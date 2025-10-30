using EAappProject.Base;
using EAappProject.Driver;
using EAappProject.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

namespace EAappProject
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPlaywrightDriver, PlaywrightDriver>();
            services.AddSingleton<IPage>(p =>
            {
                var driver = p.GetRequiredService<IPlaywrightDriver>();
                return driver.InitializeAsync().Result;
            });
            services.AddTransient<IBasePage, BasePage>();
            services.AddTransient<IHomePage, HomePage>();
            services.AddTransient<IProductListPage, ProductListPage>();
            services.AddTransient<ICreateProductPage, CreateProductPage>();
            services.AddTransient<IEditPage, EditPage>();
            services.AddTransient<IDeletePage, DeletePage>();
            services.AddTransient<IDetailsPage, DetailsPage>();
        }
    }
}