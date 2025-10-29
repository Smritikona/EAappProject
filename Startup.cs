using EAappProject.Driver;
using EAappProject.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

namespace EAappProject;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //If you call IHomePage anywhere in the code moving forward, you are going to essentially call 
        //the concrete class HomePage
        services.AddSingleton<IPlaywrightDriver, PlaywrightDriver>();
        services.AddTransient<IHomePage, HomePage>();
        services.AddTransient<IProductListPage, ProductListPage>();
        
    }
}