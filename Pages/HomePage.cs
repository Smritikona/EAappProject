using EAappProject.Driver;
using Microsoft.Playwright;
using static System.Net.Mime.MediaTypeNames;

namespace EAappProject.Pages;

public interface IHomePage
{
    Task ClickProductListAsync();
    Task ValidateTitleAsync();
}

public class HomePage(IPlaywrightDriver playwrightDriver) : IHomePage
{
    private IPage _page = playwrightDriver.InitializeAsync().Result;
    ILocator pageTitleTxt => _page.GetByRole(AriaRole.Heading, new() { Name = "Welcome" });
    ILocator lnkProductList => _page.GetByRole(AriaRole.Link, new() { Name = "Product" });
    
    public async Task ValidateTitleAsync() => await Assertions.Expect(pageTitleTxt).ToBeVisibleAsync();
    
    public async Task ClickProductListAsync() => await lnkProductList.ClickAsync();
}