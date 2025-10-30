using EAappProject.Base;
using EAappProject.Driver;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using System.Reflection.PortableExecutable;

namespace EAappProject
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPlaywrightDriver, PlaywrightDriver>();

            //Initialize the Playwright Driver and return the IPage for you.
            services.AddScoped<IPage>(p =>
            {
                var driver = p.GetRequiredService<IPlaywrightDriver>();
                return driver.InitializeAsync().GetAwaiter().GetResult();
            });
            services.AddTransient<IBasePage, BasePage>();

            //services.AddTransient<IHomePage, HomePage>();
            //services.AddTransient<IProductListPage, ProductListPage>();
            //services.AddTransient<ICreateProductPage, CreateProductPage>();
            //services.AddTransient<IEditPage, EditPage>();
            //services.AddTransient<IDeletePage, DeletePage>();
            //services.AddTransient<IDetailsPage, DetailsPage>();

            services.AddScoped<IPlaywrightAPIDriver, PlaywrightAPIDriver>();
            services.AddScoped<IAPIRequestContext>(p =>
            {
                var driver = p.GetRequiredService<IPlaywrightAPIDriver>();
                var headers = new Dictionary<string, string>
                {
                    { "Accept", "application/json" },
                    { "Content-Type", "application/json" }
                };
                return driver.InitializeAsync(headers).GetAwaiter().GetResult();
            });
        }
    }
}