using MyGraphqlApp.InputType;
using MyGraphqlApp.Interface;
using MyGraphqlApp.Model;
// using HotChocolate.Types;

namespace MyGraphqlApp.Schema;


[ExtendObjectType(typeof(RootMutation))]
public class UserMutation
{
    public async Task<User> CreateUser(
        [Service] IUserService userService, 
        CreateUserInput input)
    {
        return await userService.CreateUserAsync(input.Name, input.Email, input.Role);
    }

    public async Task<User?> UpdateUser(
        [Service] IUserService userService, 
        UpdateUserInput input)
    {
        return await userService.UpdateUserAsync(input.Id, input.Name, input.Email, input.Role);
    }

    public async Task<bool> DeleteUser(
        [Service] IUserService userService, 
        int id)
    {
        return await userService.DeleteUserAsync(id);
    }
}