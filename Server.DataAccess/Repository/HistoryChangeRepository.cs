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

        int InsertDataRow(string accountId, string table, string rowId);

        int UpdateDataRow(string accountId, string table, string rowId, string col, string value);

        int DeleteDataRow(string accountId, string table, string rowId);
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
            var list = _databaseContext.HistoryChanges.Where(h => h.AccountId == accountId).ToList();
            if (list.Any())
                return list.Max(h => h.USN);
            else
                return 0;
        }

        public List<HistoryChange> GetChanges(string accountId, int startAt, int count)
        {
            return _databaseContext.HistoryChanges.Where(h => h.AccountId == accountId && h.USN > startAt).OrderBy(h => h.USN).Take(count).ToList();
        }

        public int InsertDataRow(string accountId, string table, string rowId)
        {
            bool success;
            int usn;

            using (var dbContextTransaction = _databaseContext.Database.BeginTransaction())
            {
                usn = GetLastUpdateSequenceNumber(accountId) + 1;

                _databaseContext.HistoryChanges.Add(new HistoryChange
                {
                    AccountId = accountId,
                    USN = usn,
                    Table = table,
                    RowId = rowId,
                    Column = "Id"
                });

                success = _databaseContext.SaveChanges() > 0;

                dbContextTransaction.Commit();
            }

            return success ? usn : 0;
        }

        public int UpdateDataRow(string accountId, string table, string rowId, string col, string value)
        {
            bool success;
            int usn;

            using (var dbContextTransaction = _databaseContext.Database.BeginTransaction())
            {
                usn = GetLastUpdateSequenceNumber(accountId) + 1;

                _databaseContext.HistoryChanges.Add(new HistoryChange()
                {
                    AccountId = accountId,
                    USN = usn,
                    Table = table,
                    RowId = rowId,
                    Column = col,
                    Value = value
                });

                success = _databaseContext.SaveChanges() > 0;

                dbContextTransaction.Commit();
            }

            return success ? usn : 0;
        }

        public int DeleteDataRow(string accountId, string table, string rowId)
        {
            bool success;
            int usn;

            using (var dbContextTransaction = _databaseContext.Database.BeginTransaction())
            {
                usn = GetLastUpdateSequenceNumber(accountId) + 1;

                _databaseContext.HistoryChanges.Add(new HistoryChange()
                {
                    AccountId = accountId,
                    USN = usn,
                    Table = table,
                    RowId = rowId
                });

                success = _databaseContext.SaveChanges() > 0;

                dbContextTransaction.Commit();
            }

            return success ? usn : 0;
        }
    }
}
