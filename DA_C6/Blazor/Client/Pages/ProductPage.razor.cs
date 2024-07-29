using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using Blazor.Shared.Model;
using System.Linq;

namespace Blazor.Client.Pages
{
    public partial class ProductPage
    {
        private List<Product> products;
        private List<Category> categories;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                products = await http.GetFromJsonAsync<List<Product>>("api/product/getproducts");
                categories = await http.GetFromJsonAsync<List<Category>>("api/category/getcategories");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
            }
            await base.OnInitializedAsync();
        }

        private void OnSortOrderChanged(string value)
        {
            try
            {
                if (value == "priceLowToHigh")
                {
                    products = products.OrderBy(p => p.Price).ToList();
                }
                else if (value == "priceHighToLow")
                {
                    products = products.OrderByDescending(p => p.Price).ToList();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        
    }
}
