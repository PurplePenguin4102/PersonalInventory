using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DataModel.Repositories;

namespace Inventory.DataModel
{
    public class InventoryDatabase : IDisposable
    {
        public OwnerRepository Owners { get; private set; }
        public PossessionRepository Possessions { get; private set; }

        public InventoryDatabase()
        {
            Owners = new OwnerRepository(DB);
            Possessions = new PossessionRepository(DB);
        }

        public void Dispose()
        {
            Owners.Dispose();
            Possessions.Dispose();
        }

        private InventoryContext _DB;
        private InventoryContext DB
        {
            get
            {
                if (_DB == null)
                {
                    _DB = new InventoryContext();
                    return _DB;
                }
                else
                    return _DB;
            }
        }
    }
}
