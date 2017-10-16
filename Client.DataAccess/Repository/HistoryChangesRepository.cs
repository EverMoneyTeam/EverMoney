using Client.DataAccess.Context;
using Client.DataAccess.Model;

namespace Client.DataAccess.Repository
{
    internal class HistoryChangesRepository
    {
        internal void AddCashflow(DatabaseContext context, Cashflow cashflow)
        {
            context.HistoryChanges.Add(
                new HistoryChange
                {
                    //AccountId = 
                });
        }
    }
}