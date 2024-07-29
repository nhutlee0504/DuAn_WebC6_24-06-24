
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using Blazor.Shared.Model;
using System.Linq;
using static System.Net.WebRequestMethods;


namespace Blazor.Client.Pages
{
    public partial class Index
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
    }
}
