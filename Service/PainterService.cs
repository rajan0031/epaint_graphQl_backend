using MyGraphqlApp.Interface.IpaintService;
using MyGraphqlApp.Model;
using MyGraphqlApp.Data;
using BCrypt.Net;
using MyGraphqlApp.Validators.UserValidator;
using MyGraphqlApp.Exception.UserException;

namespace MyGraphqlApp.Service.PainterService
{

    public class PainterService : IpaintService
    {

        private readonly AppDbContext _context;
        private readonly UserValidator userValidator;

        public PainterService(AppDbContext context, UserValidator userValidator)
        {
            _context = context;
            this.userValidator = userValidator;
        }

        public async Task<Painter> RegisterPainter(Painter painter)
        {

            // start of the validations logic 

            if (!userValidator.userNameCheck(painter.UserName))
            {
                throw new UserException("User name is not valid only acccedpted as a-z , A-Z , 0-9 , _", System.Net.HttpStatusCode.BadRequest);
            }
            var painterCheck = _context.Painters.FirstOrDefault(u => u.UserName == painter.UserName);
            if (painterCheck != null)
            {
                throw new UserException("Painter Already exists , either login or try anoter painter details ", System.Net.HttpStatusCode.Conflict);
            }
            if (!userValidator.emailCheck(painter.Email))
            {
                throw new UserException("Email is incorrect , give a correct format", System.Net.HttpStatusCode.BadRequest);
            }
            var emailCheck = _context.Painters.FirstOrDefault(u => u.Email == painter.Email);
            if (emailCheck != null)
            {
                throw new UserException("Email Already exists , either login or try anoter Email", System.Net.HttpStatusCode.Conflict);
            }
            if (painter.Password?.Length < 8) // making sure circuit break 
            {
                throw new UserException("Password must be at least 8 size ", System.Net.HttpStatusCode.BadRequest);
            }
            if (!userValidator.passwordCheck(painter.Password))
            {
                throw new UserException("password is not valid , use a-z , A-Z , 0-9 , !,@,#,$,%", System.Net.HttpStatusCode.BadRequest);
            }

            if (!userValidator.phoneNuberCheck(painter.PhoneNumber))
            {
                throw new UserException("Phone no is not correct...", System.Net.HttpStatusCode.BadRequest);
            }

            // end of the validations logic 

            // painters table -- i am storning all the details in painters table 
            painter.Password = BCrypt.Net.BCrypt.HashPassword(painter.Password);
            var result1 = await _context.Painters.AddAsync(painter);
            await _context.SaveChangesAsync();

            // storing some specific details in this table for the auth porpose 
            var userObj = new User();
            userObj.Name = painter.Name;
            userObj.UserName = painter.UserName;
            userObj.Email = painter.Email;
            userObj.Password = BCrypt.Net.BCrypt.HashPassword(painter.Password);
            userObj.PhoneNumber = painter.PhoneNumber;
            userObj.Role = 2;
            userObj.LoginFlag = 0;
            var result2 = await _context.Users.AddAsync(userObj);
            await _context.SaveChangesAsync();
            return painter;
        }
    }

}