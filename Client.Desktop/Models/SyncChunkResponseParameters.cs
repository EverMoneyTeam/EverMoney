using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Desktop.Models
{
    public class SyncChunkResponseParameters
    {
        public int USN { get; set; }

        public string Table { get; set; }

        public string Column { get; set; }

        public string RowId { get; set; }

        public string Value { get; set; }
    }
}
