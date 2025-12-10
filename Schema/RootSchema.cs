using HotChocolate.Execution.Configuration;
using MyGraphqlApp.Query;
using MyGraphqlApp.Type;
using MyGraphqlApp.Mutation;
using MyGraphqlApp.Model;
using MyGraphqlApp.Mutation.PainterMutation;

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
            .AddTypeExtension<PainterMutation>();

        // .AddType<UserType>();
    }
}