using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.DataAccess.Context;
using Server.DataAccess.Model;

namespace Server.DataAccess.Repository
{
    public interface IHistoryChangeRepository
    {
        int GetLastUpdateSequenceNumber(string accountId);

        List<HistoryChange> GetChanges(string accountId, int startAt, int count);

        bool InsertDataRow(string accountId, string table, string rowId);

        bool UpdateDataRow(string accountId, string table, string rowId, string col, string value);

        bool DeleteDataRow(string accountId, string table, string rowId);
    }

    public class HistoryChangeRepository : IHistoryChangeRepository
    {
        private readonly DatabaseContext _databaseContext;

        public HistoryChangeRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public int GetLastUpdateSequenceNumber(string accountId)
        {
            return _databaseContext.HistoryChanges.Where(h => h.AccountId == accountId).Max(h => h.USN);
        }

        public List<HistoryChange> GetChanges(string accountId, int startAt, int count)
        {
            return _databaseContext.HistoryChanges.Where(h => h.AccountId == accountId && h.USN >= startAt).OrderBy(h => h.USN).Take(count).ToList();
        }

        public bool InsertDataRow(string accountId, string table, string rowId)
        {
            bool result;

            using (var dbContextTransaction = _databaseContext.Database.BeginTransaction())
            {
                _databaseContext.HistoryChanges.Add(new HistoryChange
                {
                    AccountId = accountId,
                    USN = GetLastUpdateSequenceNumber(accountId) + 1,
                    Table = table,
                    RowId = rowId,
                    Column = "Id"
                });

                result = _databaseContext.SaveChanges() > 0;

                dbContextTransaction.Commit();
            }

            return result;
        }

        public bool UpdateDataRow(string accountId, string table, string rowId, string col, string value)
        {
            bool result;

            using (var dbContextTransaction = _databaseContext.Database.BeginTransaction())
            {
                _databaseContext.HistoryChanges.Add(new HistoryChange()
            {
                AccountId = accountId,
                USN = GetLastUpdateSequenceNumber(accountId) + 1,
                Table = table,
                RowId = rowId,
                Column = col,
                Value = value
            });

                result = _databaseContext.SaveChanges() > 0;

                dbContextTransaction.Commit();
            }

            return result;
        }

        public bool DeleteDataRow(string accountId, string table, string rowId)
        {
            bool result;

            using (var dbContextTransaction = _databaseContext.Database.BeginTransaction())
            {
                    _databaseContext.HistoryChanges.Add(new HistoryChange()
            {
                AccountId = accountId,
                USN = GetLastUpdateSequenceNumber(accountId) + 1,
                Table = table,
                RowId = rowId
            });

                result = _databaseContext.SaveChanges() > 0;

                dbContextTransaction.Commit();
            }

            return result;
        }
    }
}
