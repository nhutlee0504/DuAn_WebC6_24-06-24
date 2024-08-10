﻿using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using Blazor.Shared.Model;
using System.Linq;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Components;
using static System.Net.WebRequestMethods;
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
		private int pageSize = 6; // tổng số sản phẩm trong 1 trang
		private string searchTermFromUrl;


		protected override async Task OnInitializedAsync()
		{
			try
			{
				var uri = new Uri(NavigationManager.Uri);
				var param = System.Web.HttpUtility.ParseQueryString(uri.Query);
				searchTermFromUrl = param.Get("find");
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
			totalPages = (int)Math.Ceiling((double)products.Count / pageSize);
			pagedProducts = products.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
		}

		private async Task ChangePage(int page)
		{
			if (page >= 1 && page <= totalPages)
			{
				currentPage = page;
				UpdatePagedProducts();
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
				products = (await httpClient.GetFromJsonAsync<List<Product>>("api/product/getproducts")).Where(x => selectedCategories.Contains(x.IDCategory)).ToList();
			else
			{
				products = await httpClient.GetFromJsonAsync<List<Product>>("api/product/getproducts");
			}
			UpdatePagedProducts();
		}

		private async Task OnFilterByPrice()
		{
			var filteredProducts = await httpClient.GetFromJsonAsync<List<Product>>("api/product/getproducts");

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
				if (value == "priceLowToHigh")
				{
					products = products.OrderBy(p => p.Price).ToList();
				}
				else if (value == "priceHighToLow")
				{
					products = products.OrderByDescending(p => p.Price).ToList();
				}

				currentPage = 1;
				UpdatePagedProducts();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}
	}
}