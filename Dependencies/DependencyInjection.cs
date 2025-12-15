

using HotChocolate.Authorization;

namespace MyGraphqlApp.Dependencies
{

    public static class DependencyInjection
    {

        public static void addDependencies(IServiceCollection services)
        {

            // injecting the IhttpContextAceesor  in utils file , just to get the user details from the token 
            services.AddHttpContextAccessor();

            // start of the adding the controller rest apis 
            services.AddControllers();
            // builder.Services.AddScoped<UsersController>();
            // end of the adding rest apis here 

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IpaintService, PainterService>();


            // adding all the Query here 
            services.AddScoped<RootQuery>();
            services.AddScoped<UserQuery>();


            // add all the mutations here 
            services.AddScoped<RootMutation>();
            services.AddScoped<UserMutation>();
            services.AddScoped<PainterMutation>();



            // middlewares and jwtutils
            services.AddScoped<JwtUtils>();
            services.AddScoped<UserValidator>();

            // adding all the graphql 



            services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddApplicationSchema()
            .AddErrorFilter<MyGraphqlApp.Exception.GraphqlException.GraphqlErrorFilter>();



        }

    }

}