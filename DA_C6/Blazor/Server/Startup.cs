﻿using Blazor.Server.Data;
using Blazor.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Blazor.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Thêm dịch vụ InMemoryCache
            services.AddDistributedMemoryCache(); // Cung cấp dịch vụ IDistributedCache
            services.AddDataProtection();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAccount, AccountResponse>();
            services.AddScoped<IProduct, ProductResponse>();
            services.AddScoped<IProductDetail, ProductDetailResponse>();
            services.AddScoped<IImage, ImageResponse>();
            services.AddScoped<ICategory, CategoryResponse>();
            services.AddScoped<ISize, SizeResponse>();
            services.AddScoped<IColor, ColorResponse>();
            services.AddScoped<ISupplier, SupplierResponse>();
            services.AddScoped<IEvaluate, EvaluateResponse>();
            services.AddScoped<ICategory, CategoryResponse>();
            services.AddScoped<IBill, BillResponse>();
            services.AddScoped<IBillDetail, BillDetailResponse>();
            services.AddScoped<ISale, SaleResponse>();
            services.AddScoped<ICart, CartResponse>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseSession();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}