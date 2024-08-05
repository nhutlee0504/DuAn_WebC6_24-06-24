using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using System.Linq;
using Blazor.Shared.Model;
using Microsoft.AspNetCore.Components;
using System.Net.Http;

namespace Blazor.Client.Pages
{
	public partial class ProductPage
	{
		private string searchTerm;
		private List<Product> products = new List<Product>();
		private List<Category> categories = new List<Category>();
		private List<int> selectedCategories = new List<int>();
		private int lowPrice;
		private int highPrice;
		private string message = null;



		protected override async Task OnInitializedAsync()
		{
			try
			{
				// Load initial data
				products = await http.GetFromJsonAsync<List<Product>>("api/product/getproducts");
				categories = await http.GetFromJsonAsync<List<Category>>("api/category/getcategories");
				var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
				var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
				searchTerm = query["find"];
				if (!string.IsNullOrEmpty(searchTerm))
				{
					products = await HttpClient.GetFromJsonAsync<List<Product>>($"/api/product/search?name={searchTerm}");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

		private async Task SearchProducts()
		{
			if (!string.IsNullOrEmpty(searchTerm))
			{
				products = (await http.GetFromJsonAsync<List<Product>>("api/product/getproducts"))
					.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
					.ToList();
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

			await FilterProducts();
		}

		private async Task OnFilterByPrice()
		{
			if (lowPrice >= 0 && highPrice >= 0)
			{
				products = (await http.GetFromJsonAsync<List<Product>>("api/product/getproducts"))
					.Where(x => x.Price >= lowPrice && x.Price <= highPrice)
					.ToList();

				if (products.Count == 0)
				{
					message = "Không tìm thấy sản phẩm trong khoản giá";
					StateHasChanged();
					await Task.Delay(3000);
					message = null;
				}
			}
			else
			{
				products = await http.GetFromJsonAsync<List<Product>>("api/product/getproducts");
			}

			StateHasChanged();
		}

		private async Task OnSortOrderChanged(string value)
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

			StateHasChanged();
		}

		private async Task FilterProducts()
		{
			var filteredProducts = (await http.GetFromJsonAsync<List<Product>>("api/product/getproducts"))
				.Where(p => (selectedCategories.Count == 0 || selectedCategories.Contains(p.IDCategory)) &&
							(p.Price >= lowPrice && p.Price <= highPrice))
				.ToList();

			products = filteredProducts;

			if (!products.Any())
			{
				message = "Không tìm thấy sản phẩm phù hợp.";
				StateHasChanged();
				await Task.Delay(3000);
				message = null;
			}

			StateHasChanged();
		}
	}
}
