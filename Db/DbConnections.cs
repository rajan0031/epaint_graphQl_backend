
using DotNetEnv;

namespace MyGraphqlApp.Db.DbConnections
{

    public static class DbConnections
    {



        public static void AddDatabase(IServiceCollection services, IConfiguration configuration)
        {
            Env.Load();
            var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
           {
               options.UseSqlServer(connectionString);
           });

        }



    }

}