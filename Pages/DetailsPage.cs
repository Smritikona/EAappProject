using EAappProject.Driver;
using EAappProject.Model;
using Microsoft.Playwright;

namespace EAappProject.Pages;

public interface IDetailsPage
{
    Task BackToListAsync();
    Task GoToEditPageAsync();
    Task ValidateProductDetailsPage(ProductDetails productDetails);
    Task ValidateTitleAsync();
}

public class DetailsPage : IDetailsPage
{
    private IPage _page;
    public DetailsPage(IPage page)
    {
        _page = page;
    }
    ILocator pageTitleTxt => _page.Locator("h1", new() { HasText = "Details" });
    ILocator txtName => _page.Locator("#Name");
    ILocator txtDescription => _page.Locator("#Description");
    ILocator txtPrice => _page.Locator("#Price");
    ILocator txtProductType => _page.Locator("#ProductType");
    ILocator btnBackToList => _page.GetByRole(AriaRole.Link, new() { Name = "Back to List" });
    ILocator btnEdit => _page.GetByRole(AriaRole.Link, new() { Name = "Edit" });



    public async Task ValidateTitleAsync()
    {
        await Assertions.Expect(pageTitleTxt).ToBeVisibleAsync();
    }

    public async Task ValidateProductDetailsPage(ProductDetails productDetails)
    {
        await Assertions.Expect(txtName).ToHaveTextAsync(productDetails.Name);
        await Assertions.Expect(txtDescription).ToHaveTextAsync(productDetails.Description);
        await Assertions.Expect(txtPrice).ToHaveTextAsync(productDetails.Price.ToString());
        await Assertions.Expect(txtProductType).ToHaveTextAsync(productDetails.ProductType.ToString());
    }

    public async Task GoToEditPageAsync()
    {
        await btnEdit.ClickAsync();
    }

    public async Task BackToListAsync()
    {
        await btnBackToList.ClickAsync();
    }
}