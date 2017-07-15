namespace Server.DataAccess
{
    public static class Constant
    {
        // TODO for Antony: Add security here
        public static string ConnectionString =>
            $"Data Source=SERVER_NAME;Initial Catalog=DB_NAME;Integrated Security=false;User ID=LOGIN;Password=PASSWORD";
    }
}
