using System;
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
            if (context.Accounts.Any() && context.Currencies.Any())
            {
                return;
            }

            var account = new Account { Id = new Guid("00000000-0000-0000-0000-000000000000"), Login = "login", Password = BCrypt.BCryptHelper.HashPassword("password", BCrypt.BCryptHelper.GenerateSalt()) };

            context.Accounts.Add(account);

            context.SaveChanges();

            var currency = new[]
            {
                new Currency{ Id = new Guid("00000000-0000-0000-0000-000000000001"), Name = "Гривня", Code = "UAH"},
                new Currency{ Id = new Guid("00000000-0000-0000-0000-000000000002"), Name = "Dollar (US)", Code = "USD"},
                new Currency{ Id = new Guid("00000000-0000-0000-0000-000000000003"), Name = "Euro", Code = "EUR"}
            };

            foreach (var u in currency)
            {
                context.Currencies.Add(u);
            }
            context.SaveChanges();

            var category = new CashflowCategory { Name = "Parent1", AccountId = account.Id };
            context.CashflowCategories.Add(category);
            context.SaveChanges();

            var categories = new[]
            {
               
                new CashflowCategory{  Name = "Child1", ParentCashflowCategoryId = category.Id, AccountId = account.Id},
                new CashflowCategory{  Name = "Child2", ParentCashflowCategoryId = category.Id, AccountId = account.Id},
                new CashflowCategory{  Name = "Parent2", AccountId = account.Id},
            };

            foreach (var u in categories)
            {
                context.CashflowCategories.Add(u);
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
