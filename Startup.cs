using EAappProject.Pages;
using EAappProject.Pages.Interfaces;
using EAFramework.Driver;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

namespace EAappProject;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IPlaywrightDriver, PlaywrightDriver>();
        //Initialize the Playwright Driver and return the IPage for you.
        services.AddSingleton<IPage>(p =>
        {
            var driver = p.GetRequiredService<IPlaywrightDriver>();
            return driver.InitializePlaywright().Result;
        });
        services.AddTransient<IBasePage, BasePage>();
    }
}