using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.WebApi.ViewModel
{
    public class SyncUpdateParameters
    {
        public Guid RowId { get; set; }

        public string Column { get; set; }

        public string Value { get; set; }
    }
}
