using EAappProject.Pages.Interfaces;
using Microsoft.Playwright;

namespace EAappProject.Pages;


public class HomePage(IPage page) : IHomePage
{
    private ILocator pageTitleTxt => page.GetByRole(AriaRole.Heading, new() { Name = "Welcome" });
    
    ILocator lnkProductList => page.GetByRole(AriaRole.Link, new() { Name = "Product" });

    public async Task ValidateTitleAsync() => await Assertions.Expect(pageTitleTxt).ToBeVisibleAsync();

    public async Task ClickProductListAsync() => await lnkProductList.ClickAsync();
}