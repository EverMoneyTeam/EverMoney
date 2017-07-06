namespace Server.DataAccess.Model
{
    public class CashflowCategory
    {
        public int CashflowCategoryId { get; set; }

        public string Name { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }
    }
}