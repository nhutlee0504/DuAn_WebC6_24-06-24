using Admin.Data;
using Admin.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            // Register the DbContext with dependency injection
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register application services
            services.AddScoped<Services.CategoryResponse>();
            services.AddScoped<Services.SizeResponse>();
            services.AddScoped<Services.ColorResponse>();
            services.AddScoped<Services.SupplierResponse>();
            services.AddScoped<Services.AccountResponse>();
            services.AddScoped<Services.ProductResponse>();
            services.AddScoped<Services.ProductDetailResponse>();
            services.AddScoped<IImage, ImageResponse>();
            services.AddScoped<IEvaluate, EvaluateResponse>();
            services.AddScoped<IBill, BillResponse>();
            services.AddScoped<Services.BillResponse>();
            services.AddScoped<Services.BillDetailResponse>();
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
