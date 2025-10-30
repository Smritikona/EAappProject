using EAappProject.Base;
using EAappProject.Driver;
using EAappProject.Pages;
using EAappProject.Pages.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

namespace EAappProject
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPlaywrightDriver, PlaywrightDriver>();
            //Initialize the Playwright Driver and return the IPage for you.
            services.AddSingleton<IPage>(p =>
            {
                var driver = p.GetRequiredService<IPlaywrightDriver>();
                return driver.InitializeAsync().Result;
            });
            services.AddTransient<IBasePage, BasePage>();
            //services.AddTransient<IHomePage, HomePage>();
            //services.AddTransient<IProductListPage, ProductListPage>();
            //services.AddTransient<ICreateProductPage, CreateProductPage>();
            //services.AddTransient<IEditPage, EditPage>();
            //services.AddTransient<IDeletePage, DeletePage>();
            //services.AddTransient<IDetailsPage, DetailsPage>();
        }
    }
}