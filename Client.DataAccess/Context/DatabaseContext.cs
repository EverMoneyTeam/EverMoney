using System.Data.Common;
using Client.DataAccess.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Client.DataAccess.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbConnection connection) : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<CashAccount> CashAccounts { get; set; }

        public DbSet<CashFlow> CashFlows { get; set; }

        public DbSet<CashFlowCategory> CashFlowCategories { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<HistoryChange> HistoryChanges { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DatabaseContext>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
