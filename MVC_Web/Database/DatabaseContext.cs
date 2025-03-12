using Microsoft.EntityFrameworkCore;

namespace MVC_Web.Database
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
