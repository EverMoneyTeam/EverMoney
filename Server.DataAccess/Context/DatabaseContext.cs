using Microsoft.EntityFrameworkCore;
using Server.DataAccess.Model;

namespace Server.DataAccess.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Token> Tokens { get; set; }

        public DbSet<CashAccount> CashAccounts { get; set; }

        public DbSet<Cashflow> Cashflows { get; set; }

        public DbSet<CashflowCategory> CashflowCategories { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        private readonly bool _forLocalMigration = true;

        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
            _forLocalMigration = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_forLocalMigration)
                optionsBuilder.UseSqlServer(Constant.ConnectionString);
        }
    }
}
