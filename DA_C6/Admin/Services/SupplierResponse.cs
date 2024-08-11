using Microsoft.EntityFrameworkCore;
using Admin.Data;
using Admin.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Services
{
    public class SupplierResponse : ISupplier
    {
        private readonly ApplicationDbContext _context;

        public SupplierResponse(ApplicationDbContext context) => _context = context;

        public async Task<Supplier> AddSupplierAsync(Supplier supplier)
        {
            try
            {
                await _context.Suppliers.AddAsync(supplier);
                await _context.SaveChangesAsync();
                return supplier;
            }
            catch (System.Exception ex)
            {
                // Log exception (ex.Message) here
                return null;
            }
        }

        public async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<Supplier> UpdateSupplierAsync(Supplier updateSupplier, int id)
        {
            var existingSupplier = await GetSupplierByIdAsync(id);

            if (existingSupplier != null)
            {
                existingSupplier.Address = updateSupplier.Address;
                existingSupplier.Email = updateSupplier.Email;
                existingSupplier.Phone = updateSupplier.Phone;
                existingSupplier.Name = updateSupplier.Name;

                try
                {
                    _context.Suppliers.Update(existingSupplier);
                    await _context.SaveChangesAsync();
                    return existingSupplier;
                }
                catch (System.Exception ex)
                {
                    // Log exception (ex.Message) here
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
