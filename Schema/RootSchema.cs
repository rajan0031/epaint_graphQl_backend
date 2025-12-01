using HotChocolate.Execution.Configuration;
using MyGraphqlApp.Query;
using MyGraphqlApp.Type;

namespace MyGraphqlApp.Schema;

public static class RootSchema
{
   
    public static IRequestExecutorBuilder AddApplicationSchema(this IRequestExecutorBuilder builder)
    {
        return builder

            .AddQueryType<RootQuery>()
            .AddTypeExtension<UserQuery>()

          
            .AddMutationType<RootMutation>()       
            .AddTypeExtension<UserMutation>()      

            .AddType<UserType>();
    }
}