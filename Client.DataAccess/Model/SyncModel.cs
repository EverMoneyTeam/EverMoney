using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DataAccess.Model
{
    public class SyncModel : BaseModel
    {
        public int USN { get; set; }

        public bool DirtyFlag { get; set; }
    }
}
