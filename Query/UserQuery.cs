using HotChocolate.Authorization;
using MyGraphqlApp.dtos;
using MyGraphqlApp.Model;


namespace MyGraphqlApp.Query;

[ExtendObjectType(typeof(RootQuery))]
public class UserQuery
{

    private IUserService userService;

    public UserQuery(IUserService userService)
    {
        this.userService = userService;
    }

    [GraphQLName("getAllUser")]
    [Authorize]
    public List<UserDto.GetAllUserDto> GetUsers()
    {
        return userService.GetAllUsers();
    }

    // writing the second query 
    [GraphQLName("getUserById")]
    public async Task<User> GetUserById(int id)
    {
        return await userService.getUserById(id);
    }
}

