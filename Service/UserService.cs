
using MyGraphqlApp.Model;
using MyGraphqlApp.dtos;
using MyGraphqlApp.Exception.UserException;






namespace MyGraphqlApp.Service
{

    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        private readonly JwtUtils _jwtUtils;

        private readonly UserValidator userValidator;

        private ILogger<UserService> _logger;

        private readonly EmailVerification _emailVerification;




        public UserService(AppDbContext context, JwtUtils jwtUtils, UserValidator userValidator, ILogger<UserService> logger, EmailVerification emailVerification)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            this.userValidator = userValidator;
            _logger = logger;
            _emailVerification = emailVerification;
        }


        public List<UserDto.GetAllUserDto> GetAllUsers()
        {
            var allUser = _context.Users.Select(u => new UserDto.GetAllUserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Role = u.Role
            }).ToList();

            return allUser;
        }

        public async Task<UserDto.GetAllUserDto> CreateUserAsync(string name, string userName, string email, string Password, string PhoneNumber, int role)
        {


            Console.WriteLine($"Password received: '{Password}' Length: {Password.Length}");

            var user = new User { Name = name, UserName = userName, Email = email, Password = Password, PhoneNumber = PhoneNumber, Role = role };



            if (!userValidator.userNameCheck(userName))
            {
                throw new UserException("User name is not valid only acccedpted as a-z , A-Z , 0-9 , _", System.Net.HttpStatusCode.BadRequest);
            }
            var userCheck = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (userCheck != null)
            {
                throw new UserException("user Already exists , either login or try anoter userName", System.Net.HttpStatusCode.Conflict);
            }
            if (!userValidator.emailCheck(email))
            {
                throw new UserException("Email is incorrect , give a correct format", System.Net.HttpStatusCode.BadRequest);
            }
            var emailCheck = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (emailCheck != null)
            {
                throw new UserException("Email Already exists , either login or try anoter Email", System.Net.HttpStatusCode.Conflict);
            }
            if (Password.Length < 8)
            {
                throw new UserException("Password must be at least 8 size ", System.Net.HttpStatusCode.BadRequest);
            }
            if (!userValidator.passwordCheck(Password))
            {
                throw new UserException("password is not valid , use a-z , A-Z , 0-9 , !,@,#,$,%", System.Net.HttpStatusCode.BadRequest);
            }

            if (!userValidator.phoneNuberCheck(PhoneNumber))
            {
                throw new UserException("Phone no is not correct...", System.Net.HttpStatusCode.BadRequest);

            }





            var emailOtp = _emailVerification.GetOtp();

            _emailVerification.SendOtpEmail(email, emailOtp);

            Console.WriteLine("the otp is send successfully" + email + emailOtp);






            user.Password = BCrypt.Net.BCrypt.HashPassword(Password);
            user.LoginFlag = 1;
            user.EmailOtp = emailOtp;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var responseObj = new UserDto.GetAllUserDto();
            responseObj.Id = user.Id;
            responseObj.Name = user.Name;
            responseObj.UserName = user.UserName;
            responseObj.Email = user.Email;
            responseObj.PhoneNumber = user.PhoneNumber;
            responseObj.Role = user.Role;

            return responseObj;
        }

        public async Task<User?> UpdateUserAsync(
         int id,
         string? name,
         string? userName,
         string? email,
         string? phoneNumber,
         int role
        )
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            if (!string.IsNullOrEmpty(name))
                user.Name = name;

            if (!string.IsNullOrEmpty(userName))
                user.UserName = userName;

            if (!string.IsNullOrEmpty(email))
                user.Email = email;

            if (!string.IsNullOrEmpty(phoneNumber))
                user.PhoneNumber = phoneNumber;

            if (role > 0)
                user.Role = role;

            await _context.SaveChangesAsync();
            return user;
        }


        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> getUserById(int id)
        {


            var result = await _context.Users.FindAsync(id);


            if (result == null)
            {
                throw new UserException("User details is not found for this id");
            }
            return result!;
        }

        public async Task<UserDto.LoginResponse> loginUser(UserDto.loginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.email);
            if (user == null)
            {
                throw new UserException("user is not found", System.Net.HttpStatusCode.BadRequest);
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.password, user.Password);
            _logger.LogInformation("the isPasswordValid" + isPasswordValid);
            if (!isPasswordValid)
            {
                throw new UserException("Password is incorrect, give the correct password", System.Net.HttpStatusCode.BadRequest);
            }
            if (user.LoginFlag == 0)
            {
                var tempRes = new UserDto.LoginResponse();
                tempRes.User = null;
                tempRes.Token = null;
                tempRes.Message = "login successfull , now kindly change your password";
                return tempRes;
            }
            // var result = _context.Users.FirstOrDefault(u => u.Email == loginDto.email && isPasswordValid);

            // await _context.SaveChangesAsync();
            var token = _jwtUtils.GenerateToken(user);

            var responseObj = new UserDto.LoginResponse();
            responseObj.User = user;
            responseObj.Token = token;
            responseObj.Message = "login successfully";
            return responseObj;


        }

        // change password api goes here 

        public string changePassword(UserDto.ChangePasswordDto changePasswordDto)
        {

            if (changePasswordDto.password == changePasswordDto.newPassword)
            {
                return "Please use a different new password , not the previous one";
            }
            if (!userValidator.passwordCheck(changePasswordDto.newPassword!))
            {
                throw new UserException("password must be valid atleast 8 length , and must have @ , a-z , A-Z and @ # $ %");
            }
            var result = _context.Users.Find(changePasswordDto.id);
            _logger.LogInformation("the user from the database is " + result);
            if (result == null)
            {
                return "User is not found ";
            }
            var userHasedPassword = result.Password;
            _logger.LogInformation(userHasedPassword);
            _logger.LogInformation(" the password is " + changePasswordDto.password);
            _logger.LogInformation(" the new  password is " + changePasswordDto.newPassword);

            bool passwordChek = BCrypt.Net.BCrypt.Verify(changePasswordDto.password, userHasedPassword);
            _logger.LogInformation("the password check is " + passwordChek);
            if (!passwordChek)
            {
                return "Give your correct password";
            }
            result.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.newPassword);
            result.LoginFlag = 1;
            _context.SaveChanges();
            return "the password is changed successfully";
        }


    }

}