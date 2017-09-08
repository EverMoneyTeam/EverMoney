using Microsoft.EntityFrameworkCore;
using Server.DataAccess.Model;

namespace Server.DataAccess.Context
{
    public class DBContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Token> Tokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constant.ConnectionString);
        }
    }
}
