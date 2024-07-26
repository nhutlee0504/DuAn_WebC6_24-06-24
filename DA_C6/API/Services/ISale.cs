using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Model;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public interface ISale
    {
        public IEnumerable<Sale> GetSale();

        public Sale AddSale(Sale sale);
        public Sale GetSaleByID(int id);
        public Sale GetSaleByName (string name);
    }
}
