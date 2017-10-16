using Client.DataAccess.Context;
using Client.DataAccess.Model;

namespace Client.DataAccess.Repository
{
    internal class HistoryChangesRepository
    {
        internal void AddCashFlow(DatabaseContext context, CashFlow CashFlow)
        {
            context.HistoryChanges.Add(
                new HistoryChange
                {
                    //AccountId = 
                });
        }
    }
}