using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.PCL.Classes
{
    public class History
    {
        public int Id { get; set; }
        public virtual Owner OwnerInvolved { get; set; }
        public virtual Task TaskInvolved { get; set; }
        public DateTime EventCompleted { get; set; }

    }
}
