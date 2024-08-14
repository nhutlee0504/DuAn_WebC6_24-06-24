using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Model;

namespace Admin.Services
{
    public interface ISupplier
    {
        Task<IEnumerable<Supplier>> GetSuppliersAsync();
        Task<Supplier> AddSupplierAsync(Supplier supplier);
        Task<Supplier> UpdateSupplierAsync(Supplier supplier, int id);
        Task<Supplier> GetSupplierByIdAsync(int id);
    }
}
