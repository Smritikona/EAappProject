using EAappProject.Driver;
using Microsoft.Playwright;

namespace EAappProject.Pages;
public interface IHomePage
{
    public Task ValidateTitleAsync();
    public Task ClickProductListAsync();
}
public class HomePage : IHomePage
{
    private IPage _page;

    public HomePage(IPage page)
    {
        _page = page;
    }

    ILocator pageTitleTxt => _page.GetByRole(AriaRole.Heading, new() { Name = "Welcome" });
    ILocator lnkProductList => _page.GetByRole(AriaRole.Link, new() { Name = "Product" });
    public async Task ValidateTitleAsync()
    {
        await Assertions.Expect(pageTitleTxt).ToBeVisibleAsync();
    }

    public async Task ClickProductListAsync()
    {
        await lnkProductList.ClickAsync();
    }
}