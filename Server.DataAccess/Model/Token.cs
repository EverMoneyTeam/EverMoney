using System;

namespace Server.DataAccess.Model
{
    public class Token : BaseModel
    {
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        public string RefreshToken { get; set; }
        public int IsStop { get; set; }
    }
}