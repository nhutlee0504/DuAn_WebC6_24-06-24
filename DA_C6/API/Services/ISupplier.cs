using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Model;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public interface ISupplier
    {
        public IEnumerable<Supplier> GetSuppliers();
        public Supplier Addsuplire (Supplier supplier);
        public Supplier UpdateSuplier (Supplier supplier, int id);
        public Supplier GetSupplierByID(int id);
    }
}
