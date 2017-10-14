using Client.DataAccess.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Client.DataAccess.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=DatabaseContext")
        {
        }

        public DbSet<CashAccount> CashAccounts { get; set; }

        public DbSet<Cashflow> Cashflows { get; set; }

        public DbSet<CashflowCategory> CashflowCategories { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DatabaseContext>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
