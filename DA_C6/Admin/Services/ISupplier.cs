using System.Collections.Generic;
using System.Linq;
using Admin.Model;
namespace Admin.Services
{
    public interface ISupplier
    {
        public IEnumerable<Supplier> GetSuppliers();
        public Supplier Addsuplire (Supplier supplier);
        public Supplier UpdateSuplier (Supplier supplier, int id);
        public Supplier GetSupplierByID(int id);
    }
}
