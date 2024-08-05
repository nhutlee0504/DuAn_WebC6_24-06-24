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
        private List<int> selectedCategories = new List<int>();
        private int lowPrice;
        private int highPrice;
        private string message = null;
        private Dictionary<int, string> qrCodeUrls = new Dictionary<int, string>();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                products = await http.GetFromJsonAsync<List<Product>>("api/product/getproducts");
                categories = await http.GetFromJsonAsync<List<Category>>("api/category/getcategories");
                await LoadQRCodeUrls();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            await base.OnInitializedAsync();
        }

        private async Task LoadQRCodeUrls()
        {
            foreach (var product in products)
            {
                var qrCodeData = $"Sản phẩm: {product.Name} - Giá: {product.Price.ToString("N0")} đồng";
                var qrCodeUrl = await GetQRCodeAsync(qrCodeData);
                qrCodeUrls[product.IDProduct] = qrCodeUrl;
            }
        }

        private async Task<string> GetQRCodeAsync(string data)
        {
            try
            {
                var response = await http.GetStringAsync($"api/qrcode/generate?data={Uri.EscapeDataString(data)}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching QR code: {ex.Message}");
                return null;
            }
        }

        private async Task OnCategoryChanged(int IDCategory, bool isChecked)
        {
            if (isChecked)
            {
                selectedCategories.Add(IDCategory);
            }
            else
            {
                selectedCategories.Remove(IDCategory);
            }

            if (selectedCategories.Count > 0)
            {
                products = (await http.GetFromJsonAsync<List<Product>>("api/product/getproducts")).Where(x => selectedCategories.Contains(x.IDCategory)).ToList();
                await LoadQRCodeUrls(); 
            }
            else
            {
                products = await http.GetFromJsonAsync<List<Product>>("api/product/getproducts");
                await LoadQRCodeUrls(); 
            }
        }

        private async Task OnFilterByPrice()
        {
            if (lowPrice >= 0 && highPrice >= 0)
            {
                products = (await http.GetFromJsonAsync<List<Product>>("api/product/getproducts")).Where(x => x.Price >= lowPrice && x.Price <= highPrice).ToList();
                if (products.Count == 0)
                {
                    message = "Không tìm thấy sản phẩm trong khoản giá";
                    await Task.Delay(3000);
                    message = null;
                    products = await http.GetFromJsonAsync<List<Product>>("api/product/getproducts");
                }
                await LoadQRCodeUrls(); 
            }
            else
            {
                products = await http.GetFromJsonAsync<List<Product>>("api/product/getproducts");
                await LoadQRCodeUrls(); 
            }
        }

        private async Task OnSortOrderChanged(string value)
        {
            try
            {
                if (value == "priceLowToHigh")
                {
                    products = products.OrderBy(p => p.Price).ToList();
                }
                else
                {
                    products = products.OrderByDescending(p => p.Price).ToList();
                }
                await LoadQRCodeUrls(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
