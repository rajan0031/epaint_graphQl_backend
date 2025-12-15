
using Microsoft.EntityFrameworkCore;
using MyGraphqlApp.Data;

namespace MyGraphqlApp.Db.DbConnections
{

    public static class DbConnections
    {

        public static void AddDatabase(IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
           {
               options.UseSqlServer(connectionString);
           });

        }



    }

}