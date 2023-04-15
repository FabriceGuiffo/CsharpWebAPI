using completeAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace completeAPI.DAL
{
    public class GeneralContext:DbContext,IContext
    {
        private readonly IConfiguration _configuration;

        public GeneralContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("webapidatabase"));
        }

        public DbSet<Compte> Comptes { get; set; }
    }
}
