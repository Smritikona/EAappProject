using EAappProject.Controls;
using EAappProject.Model;
using Microsoft.Playwright;

namespace EAappProject.Pages
{
    public class EditProductPage(IPage page)
    {
        ILocator pageTitleTxt => page.GetByRole(AriaRole.Heading, new() { Name = "List" });
        ILocator txtName => page.GetByRole(AriaRole.Textbox, new() { Name = "Name" });
        ILocator txtDescription => page.GetByRole(AriaRole.Textbox, new() { Name = "Description" });
        ILocator txtPrice => page.Locator("#Price");
        ILocator txtProductType => page.GetByRole(AriaRole.Textbox, new() { Name = "ProductType" });
        ILocator btnSave => page.GetByRole(AriaRole.Button, new() { Name = "Save" });

        public async Task<EditProductPage> ValidateTitleAsync()
        {
            await pageTitleTxt.IsVisibleAsync();
            return this;
        }
        public async Task<ProductListPage> EditProductAsync(ProductDetails productDetails)
        {
            await txtName.ClearAndFillElementAsync(productDetails.Name);
            await txtDescription.ClearAndFillElementAsync(productDetails.Description);
            await txtPrice.ClearAndFillElementAsync(productDetails.Price);
            await txtProductType.ClearAndFillElementAsync(productDetails.ProductType);
            await btnSave.ClickAsync();
            return new ProductListPage(page);
        }
    }
}
