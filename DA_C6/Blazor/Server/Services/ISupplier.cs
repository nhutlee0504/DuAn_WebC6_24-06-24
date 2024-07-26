using Microsoft.AspNetCore.Mvc;
using Blazor.Data;
using Blazor.Model;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Services
{
    public interface ISupplier
    {
        public IEnumerable<Supplier> GetSuppliers();
        public Supplier Addsuplire (Supplier supplier);
        public Supplier UpdateSuplier (Supplier supplier, int id);
        public Supplier GetSupplierByID(int id);
    }
}
