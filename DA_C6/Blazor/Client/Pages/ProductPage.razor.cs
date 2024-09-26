using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using Blazor.Shared.Model;
using System.Linq;
using Microsoft.AspNetCore.Components;

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
        private List<Product> pagedProducts;
        private int currentPage = 1;
        private int totalPages;
        private int pageSize =6; // tổng số sản phẩm trong 1 trang
        private string searchTerm = ""; // Biến để lưu từ khóa tìm kiếm

        protected override async Task OnInitializedAsync()
        {
            try
            {
                products = await httpClient.GetFromJsonAsync<List<Product>>("api/product/getproducts");
                categories = await httpClient.GetFromJsonAsync<List<Category>>("api/category/getcategories");
                UpdatePagedProducts();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void UpdatePagedProducts()
        {
            // Cập nhật sản phẩm phân trang dựa trên danh sách sản phẩm hiện có và từ khóa tìm kiếm
            var filteredProducts = products.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            totalPages = (int)Math.Ceiling((double)filteredProducts.Count / pageSize);
            pagedProducts = filteredProducts.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        private async Task ChangePage(int page)
        {
                currentPage = page;
                UpdatePagedProducts();
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
                products = (await httpClient.GetFromJsonAsync<List<Product>>("api/product/getproducts")).Where(x => selectedCategories.Contains(x.IDCategory)).ToList();
            else
            {
                products = await httpClient.GetFromJsonAsync<List<Product>>("api/product/getproducts");
            }
            UpdatePagedProducts();
        }

        private async Task OnFilterByPrice()
        {
            var filteredProducts = products.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower())).ToList();

            if (lowPrice >= 0 && highPrice >= 0)
            {
                filteredProducts = filteredProducts.Where(x => x.Price >= lowPrice && x.Price <= highPrice).ToList();
            }

            if (filteredProducts.Count == 0)
            {
                message = "Không tìm thấy sản phẩm trong khoản giá";
                await Task.Delay(3000);
                message = null;
                products = await httpClient.GetFromJsonAsync<List<Product>>("api/product/getproducts");
            }
            else
            {
                products = filteredProducts;
            }
            UpdatePagedProducts();
        }

        private async Task OnSortOrderChanged(string value)
        {
            try
            {
                var filteredProducts = products.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower())).ToList();

                if (value == "priceLowToHigh")
                {
                    filteredProducts = filteredProducts.OrderBy(p => p.Price).ToList();
                }
                else if (value == "priceHighToLow")
                {
                    filteredProducts = filteredProducts.OrderByDescending(p => p.Price).ToList();
                }

                products = filteredProducts;
                currentPage = 1;
                UpdatePagedProducts();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void OnSearchTermChanged(ChangeEventArgs e)
        {
            searchTerm = e.Value.ToString();
            UpdatePagedProducts();
        }

        private void OnSearch()
        {
            UpdatePagedProducts();
        }
    }
}
