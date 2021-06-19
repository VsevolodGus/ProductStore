using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Store;
using Store.Memory;
using Store.Contract;
using Store.Messages;
using Store.SberKassa;
using ProductStore.Web.Contract;

namespace StoreProduct.Web
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
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
            });

            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IMakerRepository, MakerRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<INotificationService, DebugNotificationService>();
            services.AddSingleton<IDeliveryService, DeliveryLocations>();
            services.AddSingleton<IPaymentService, CashPaymentService>();
            services.AddSingleton<IPaymentService, SberKassaPaymentService>();
            services.AddSingleton<IWebContractorService, SberKassaPaymentService>();
            services.AddSingleton<ProductService>();
            

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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
