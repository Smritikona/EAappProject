using Microsoft.Playwright;

namespace EAappProject.Pages;

public class HomePage(IPage page)
{
    ILocator pageTitleTxt => page.GetByRole(AriaRole.Heading, new() { Name = "Welcome" });
    ILocator lnkProductList => page.GetByRole(AriaRole.Link, new() { Name = "Product" });

    public async Task<HomePage> ValidateTitleAsync()
    {
        await Assertions.Expect(pageTitleTxt).ToBeVisibleAsync();
        return new HomePage(page);
    }

    public async Task<ProductListPage> ClickProductListAsync()
    {
        await lnkProductList.ClickAsync();
        return new ProductListPage(page);
    }
}