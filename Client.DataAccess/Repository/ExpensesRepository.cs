using Client.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DataAccess.Repository
{
    class ExpensesRepository
    {
        public void GetAllExpenses()
        {
            using (var db = new DatabaseContext())
            {
                
            }
        }
    }
}
