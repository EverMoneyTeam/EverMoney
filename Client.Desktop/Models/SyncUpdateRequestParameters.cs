using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Desktop.Models
{
    public class SyncUpdateRequestParameters
    {
        public Guid RowId { get; set; }

        public string Column { get; set; }

        public string Value { get; set; }
    }
}
