using Data.Tables;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			optionsBuilder.UseNpgsql("");
		}

        public DbSet<User> Users { get; set; }
        public DbSet<Sites> Sites { get; set; }


    }
}
