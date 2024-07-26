using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Data;
using Blazor.Shared.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Services
{
    public class SupplierResponse:ISupplier
    {
        private readonly ApplicationDbContext context;
        public SupplierResponse(ApplicationDbContext ct) => context = ct;

        public Supplier Addsuplire(Supplier supplier)
        {
            try
            {
                context.Suppliers.Add(supplier);
                context.SaveChanges();
                return supplier;
            }
            catch (System.Exception)
            {

                return null;
            }
        }

        public Supplier GetSupplierByID(int id)
        {
            return context.Suppliers.FirstOrDefault(c => c.IDSupplier == id);
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
           return context.Suppliers;
        }

        public Supplier UpdateSuplier(Supplier updateSupplier, int id)
        {
            var existingSuplier = GetSupplierByID(id);

            if (existingSuplier != null)
            {
                existingSuplier.Address = updateSupplier.Address;
                existingSuplier.Email = updateSupplier.Email;
                existingSuplier.Phone = updateSupplier.Phone;
                existingSuplier.Name = updateSupplier.Name;
                context.SaveChanges();
                return existingSuplier;
            }
            else
            {
                return null;
            }      
        }
    }
}
