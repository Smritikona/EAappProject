using EAappProject.Controls;
using EAappProject.Model;
using Microsoft.Playwright;

namespace EAappProject.Pages;

public class EditPage(IPage page)
{
    public ILocator pageTitleTxt => page.Locator("h1", new() { HasText = "Edit" });
    public ILocator txtName => page.GetByRole(AriaRole.Textbox, new() { Name = "Name" });
    public ILocator txtDescription => page.GetByRole(AriaRole.Textbox, new() { Name = "Description" });
    public ILocator txtPrice => page.Locator("#Price");
    public ILocator txtProductType => page.GetByLabel("ProductType");
    public ILocator btnSave => page.GetByRole(AriaRole.Button, new() { Name = "Save" });

    public async Task<ProductListPage> UpdateAsync(ProductDetails productDetails)
    {
        await txtName.ClearAndFillElementAsync(productDetails.Name);
        await txtDescription.ClearAndFillElementAsync(productDetails.Description);
        await txtPrice.ClearAndFillElementAsync(productDetails.Price.ToString());
        await txtProductType.ClearAndFillElementAsync(productDetails.ProductType);
        await btnSave.ClickAsync();
        return new ProductListPage(page);
    }
}