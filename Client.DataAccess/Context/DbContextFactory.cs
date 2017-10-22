using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;

namespace Client.DataAccess.Context
{
    public static class DbContextFactory
    {
        private static string _currentPassword = "00000000-0000-0000-0000-000000000000";

        private static string _connectionStringWithoutPassword = "Data Source=.\\EverMoney.sqlite;Version=3;";

        private static string _connectionString = "Data Source=.\\EverMoney.sqlite;Version=3;Password={0}";

        public static DatabaseContext GetDbContext()
        {
            var conn = new SQLiteConnection(string.Format(_connectionString, _currentPassword));
            return new DatabaseContext(conn);
        }

        public static void SetDefaultPassword()
        {
            SQLiteConnection conn = new SQLiteConnection(_connectionStringWithoutPassword);
            if (TryConnect(conn))
            {
                conn.Open();
                conn.ChangePassword(_currentPassword);
                conn.Close();
                conn = new SQLiteConnection(string.Format(_connectionString, _currentPassword));
                TryConnect(conn);
            }
            else
            {
                conn = new SQLiteConnection(string.Format(_connectionString, _currentPassword));
                if (!TryConnect(conn))
                    throw new DataException("Something wrong with connection");
            }
        }

        public static void SetPassword(string password)
        {
            SQLiteConnection conn = new SQLiteConnection(string.Format(_connectionString, password));
            if (TryConnect(conn))
            {
                _currentPassword = password;
                return;
            }

            conn = new SQLiteConnection(string.Format(_connectionString, _currentPassword));
            if (TryConnect(conn))
            {
                conn.Open();
                conn.ChangePassword(password);
                conn.Close();
                conn = new SQLiteConnection(string.Format(_connectionString, password));
                if (TryConnect(conn))
                {
                    _currentPassword = password;
                    return;
                }
                else
                {
                    throw new DataException("Something wrong with connection");
                }
            }

            SetDefaultPassword();
            conn = new SQLiteConnection(string.Format(_connectionString, _currentPassword));
            conn.Open();
            conn.ChangePassword(password);
            conn.Close();
            conn = new SQLiteConnection(string.Format(_connectionString, password));
            if (TryConnect(conn))
            {
                _currentPassword = password;
                return;
            }
            else
            {
                throw new DataException("Something wrong with connection");
            }

        }

        private static bool TryConnect(SQLiteConnection conn)
        {
            try
            {
                new DatabaseContext(conn).Accounts.ToList();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}