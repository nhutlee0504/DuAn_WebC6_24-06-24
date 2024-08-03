using Admin.Data;
using Admin.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin
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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
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
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
