using EAappProject.Controls;
using EAappProject.Driver;
using EAappProject.Model;
using Microsoft.Playwright;

namespace EAappProject.Pages;

public interface IEditPage
{
    ILocator btnSave { get; }
    ILocator pageTitleTxt { get; }
    ILocator txtDescription { get; }
    ILocator txtName { get; }
    ILocator txtPrice { get; }
    ILocator txtProductType { get; }
    Task UpdateAsync(ProductDetails productDetails);
}

public class EditPage(IPlaywrightDriver playwrightDriver) : IEditPage
{
    private IPage _page = playwrightDriver.InitializeAsync().Result;
    public ILocator pageTitleTxt => _page.Locator("h1", new() { HasText = "Edit" });
    public ILocator txtName => _page.GetByRole(AriaRole.Textbox, new() { Name = "Name" });
    public ILocator txtDescription => _page.GetByRole(AriaRole.Textbox, new() { Name = "Description" });
    public ILocator txtPrice => _page.Locator("#Price");
    public ILocator txtProductType => _page.GetByLabel("ProductType");
    public ILocator btnSave => _page.GetByRole(AriaRole.Button, new() { Name = "Save" });

    public async Task UpdateAsync(ProductDetails productDetails)
    {
        await txtName.ClearAndFillElementAsync(productDetails.Name);
        await txtDescription.ClearAndFillElementAsync(productDetails.Description);
        await txtPrice.ClearAndFillElementAsync(productDetails.Price.ToString());
        await txtProductType.ClearAndFillElementAsync(productDetails.ProductType.ToString());
        await btnSave.ClickAsync();
    }
}