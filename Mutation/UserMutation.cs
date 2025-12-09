using MyGraphqlApp.InputType;
using MyGraphqlApp.Interface;
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


    public async Task<User> CreateUser(CreateUserInput input)
    {
        return await userServices.CreateUserAsync(input.Name, input.UserName, input.Email, input.PhoneNumber, input.Password, input.Role);
    }

    public async Task<User?> UpdateUser(UpdateUserInput input)
    {
        Console.WriteLine("Update mutations is called here ");
        return await userServices.UpdateUserAsync(input.Id, input.Name, input.UserName, input.Email, input.PhoneNumber,input.Role);
    }

    public async Task<bool> DeleteUser(int id)
    {
        return await userServices.DeleteUserAsync(id);
    }
}