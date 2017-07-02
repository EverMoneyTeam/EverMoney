using System;

namespace Server.DataAccess
{
    public static class Constant
    {
        // TODO for Antony: Add security here
        public static string ConnectionString =>
            $"Data Source={Environment.MachineName};Initial Catalog=EverMoney;Integrated Security=True";
    }
}
