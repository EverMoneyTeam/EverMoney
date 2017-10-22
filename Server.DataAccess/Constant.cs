namespace Server.DataAccess
{
    public static class Constant
    {
        // Local connection string
        public static string ConnectionString =>
            "Data Source=localhost\\SQLEXPRESS;Initial Catalog=EverMoney;Integrated Security=true;";
    }

    public enum SyncTable
    {
        CashFlow,
        CashAccount,
        CashFlowCategory
    }

    public enum SyncCashFlowColumn
    {
        Id,
        Amount,
        Date,
        Description,
        CashAccountId,
        CashFlowCategoryId
    }

    public enum SyncCashFlowCategoryColumn
    {
        Id,
        Name,
        ParentCashFlowCategoryId
    }

    public enum SyncCashAccountColumn
    {
        Id,
        Amount,
        CurrencyId,
        Name
    }
}
