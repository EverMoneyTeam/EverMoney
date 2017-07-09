using Microsoft.EntityFrameworkCore;
using Server.DataAccess.Model;

namespace Server.DataAccess.Context
{
    public class SecurityContext : DbContext
    {
        public SecurityContext(DbContextOptions<SecurityContext> options)
            : base(options)
        { }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constant.ConnectionString);
        }
    }
}
