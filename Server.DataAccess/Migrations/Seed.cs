using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Server.DataAccess.Context;
using Server.DataAccess.Model;
using System.Linq;

namespace Server.DataAccess.Migrations
{
    public static class DbInitializer
    {
        public static void EnsureSeedData(this DatabaseContext context)
        {
            if (context.Accounts.Any() && context.Users.Any())
            {
                return;
            }

            var accounts = new[] {
                new Account{ Login = "login", Password = "password" },
                new Account{ Login = "test_account", Password="test_password" }
            };

            foreach (var a in accounts)
            {
                context.Accounts.Add(a);
            }
            context.SaveChanges();

            var users = new[]
            {
                new User{Name = "Pasha", AccountId = accounts.Single(a => a.Login == "login").Id},
                new User{Name = "Ira", AccountId = accounts.Single(a => a.Login == "login").Id},
                new User{Name = "Alex", AccountId = accounts.Single(a => a.Login == "test_account").Id}
            };

            foreach (var u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
        }

        public static void EnsureUpdated(this DatabaseContext context)
        {
            if (!context.AllMigrationsApplied())
            {
                context.Database.Migrate();
            }
        }

        public static bool AllMigrationsApplied(this DatabaseContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }
    }
}
