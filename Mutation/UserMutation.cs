using MyGraphqlApp.dtos;
using MyGraphqlApp.InputType;
using MyGraphqlApp.Model;


namespace MyGraphqlApp.Mutation;


[ExtendObjectType(typeof(RootMutation))]
public class UserMutation
{


    private readonly IUserService userServices;

    public UserMutation(IUserService userService)
    {
        this.userServices = userService;
    }


    public async Task<UserDto.GetAllUserDto> CreateUser(CreateUserInput input)
    {
        return await userServices.CreateUserAsync(input.Name, input.UserName, input.Email, input.Password, input.PhoneNumber, input.Role);
    }

    public async Task<User?> UpdateUser(UpdateUserInput input)
    {
        Console.WriteLine("Update mutations is called here ");
        return await userServices.UpdateUserAsync(input.Id, input.Name, input.UserName, input.Email, input.PhoneNumber, input.Role);
    }

    public async Task<bool> DeleteUser(int id)
    {
        return await userServices.DeleteUserAsync(id);
    }

    public async Task<UserDto.LoginResponse> loginUser(loginInput loginInput)
    {
        var loginInputObj = new UserDto.loginDto();
        loginInputObj.email = loginInput.email;
        loginInputObj.password = loginInput.password;
        return await userServices.loginUser(loginInputObj);
    }

    // change the password for the painter , Employee for the first time 

    public string changePassword(changePasswordInput changePasswordInput)
    {
        var changePasswordObj = new UserDto.ChangePasswordDto();
        changePasswordObj.id = changePasswordInput.id;
        changePasswordObj.password = changePasswordInput.password;
        changePasswordObj.newPassword = changePasswordInput.newPassword;

        return userServices.changePassword(changePasswordObj);

    }

}