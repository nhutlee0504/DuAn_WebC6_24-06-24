using System.Collections.Generic;
using System.Linq;
using Blazor.Shared.Model;
namespace Blazor.Server.Services
{
    public interface ISale
    {
        public IEnumerable<Sale> GetSale();

        public Sale AddSale(Sale sale);
        public Sale GetSaleByID(int id);
        public Sale GetSaleByName (string name);
    }
}
