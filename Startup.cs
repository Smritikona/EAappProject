using EAappProject.Driver;
using EAappProject.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;


namespace EAappProject
{
    public class Startup
    {
        public Startup()
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPlaywrightDriver, PlaywrightDriver>();
            services.AddSingleton<IPage>(p =>
            {
                IPlaywrightDriver driver = p.GetRequiredService<IPlaywrightDriver>();
                return driver.InitializePlaywright().GetAwaiter().GetResult();
            });
            services.AddTransient<IHomePage, HomePage>();
            services.AddTransient<IProductListPage, ProductListPage>();
            services.AddTransient<ICreateProductPage, CreateProductPage>();
            services.AddTransient<IDeletePage, DeletePage>();
            services.AddTransient<IEditPage, EditPage>();
            services.AddTransient<IDetailsPage, DetailsPage>();
        }
    }
}
