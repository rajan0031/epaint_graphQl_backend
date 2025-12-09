using Microsoft.EntityFrameworkCore;
using MyGraphqlApp.Model;

namespace MyGraphqlApp.Data
{



    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

       
    }
}
