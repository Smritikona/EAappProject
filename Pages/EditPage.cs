using EAappProject.Controls;
using EAappProject.Driver;
using EAappProject.Model;
using Microsoft.Playwright;

namespace EAappProject.Pages;
public interface IEditPage
{
    Task UpdateAsync(ProductDetails productDetails);
    Task ValidateProductDetailsAsync(ProductDetails productDetails);
    Task ValidateTitleAsync();
}
public class EditPage : IEditPage
{
    private IPage _page;
    public EditPage(IPage page)
    {
        _page = page;
    }
    ILocator pageTitleTxt => _page.Locator("h1", new() { HasText = "Edit" });
    ILocator txtName => _page.GetByRole(AriaRole.Textbox, new() { Name = "Name" });
    ILocator txtDescription => _page.GetByRole(AriaRole.Textbox, new() { Name = "Description" });
    ILocator txtPrice => _page.Locator("#Price");
    ILocator txtProductType => _page.GetByLabel("ProductType");
    ILocator btnSave =>     _page.GetByRole(AriaRole.Button, new() { Name = "Save" });


    public async Task ValidateTitleAsync()
    {
        await Assertions.Expect(pageTitleTxt).ToBeVisibleAsync();
    }

    public async Task ValidateProductDetailsAsync(ProductDetails productDetails)
    {
        await Assertions.Expect(txtName).ToHaveValueAsync(productDetails.Name);
        await Assertions.Expect(txtDescription).ToHaveValueAsync(productDetails.Description);
        await Assertions.Expect(txtPrice).ToHaveValueAsync(productDetails.Price.ToString());
        await Assertions.Expect(txtProductType).ToHaveValueAsync(productDetails.ProductType.ToString());
    }
    public async Task UpdateAsync(ProductDetails productDetails)
    {
        await txtPrice.ClearAndFillElementAsync(productDetails.NewPrice);
        await btnSave.ClickAsync();
    }
}