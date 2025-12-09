
using MyGraphqlApp.Interface;
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
    public List<User> GetUsers()
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

// [Service] IUserService userService