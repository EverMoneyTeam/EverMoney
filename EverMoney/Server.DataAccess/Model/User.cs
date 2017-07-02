namespace Server.DataAccess.Model
{
    public class User
    {
        public int UserId { get; set; }

        public int? AccountId { get; set; }

        public virtual Account Account { get; set; }

        public string Name { get; set; }
    }
}
