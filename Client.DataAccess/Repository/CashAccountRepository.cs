﻿using Client.DataAccess.Context;
using Client.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DataAccess.Repository
{
    public class CashAccountRepository
    {
        public List<CashAccount> GetAllCashAccounts()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.CashAccounts.ToList();
            }
        }

        public CashAccount GetCashAccountByName(string name)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.CashAccounts.FirstOrDefault(x => x.Name == name);
            }
        }

    }
}
