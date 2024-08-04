using System.Collections.Generic;
using System.Linq;
using Admin.Model;
namespace Admin.Services
{
    public interface ISale
    {
        public IEnumerable<Sale> GetSale();

        public Sale AddSale(Sale sale);
        public Sale GetSaleByID(int id);
        public Sale GetSaleByName (string name);
    }
}
