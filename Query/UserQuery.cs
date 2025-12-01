using HotChocolate;
using HotChocolate.Types;
using MyGraphqlApp.Interface;
using MyGraphqlApp.Model;

namespace MyGraphqlApp.Query;

[ExtendObjectType(typeof(RootQuery))]
public class UserQuery
{
    public IQueryable<User> GetUsers([Service] IUserService userService)
    {
        return userService.GetAllUsers();
    }
}