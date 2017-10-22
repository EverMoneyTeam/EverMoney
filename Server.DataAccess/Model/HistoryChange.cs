using System;
using System.Collections.Generic;
using System.Text;

namespace Server.DataAccess.Model
{
    public class HistoryChange : BaseModel
    {
        public string AccountId { get; set; }

        public Account Account { get; set; }

        public int USN { get; set; }

        public string Table { get; set; }

        public string RowId { get; set; }

        public string Column { get; set; }

        public string Value { get; set; }
    }
}
