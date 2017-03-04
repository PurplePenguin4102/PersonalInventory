using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Classes
{
    public interface IContainsId
    {
        int Id { get; set; }
    }
}
