using System.Collections.Generic;
using System.Linq;
using Blazor.Shared.Model;
namespace Blazor.Server.Services
{
    public interface ISupplier
    {
        public IEnumerable<Supplier> GetSuppliers();
        public Supplier Addsuplire (Supplier supplier);
        public Supplier UpdateSuplier (Supplier supplier, int id);
        public Supplier GetSupplierByID(int id);
    }
}
