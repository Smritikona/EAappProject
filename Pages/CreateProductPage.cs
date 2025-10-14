using EAappProject.Controls;
using Microsoft.Playwright;

namespace EAappProject.Pages
{
    public class CreateProductPage(IPage page)
    {
        ILocator pageTitleTxt => page.GetByRole(AriaRole.Heading, new() { Name = "Create" });
        ILocator txtName => page.GetByRole(AriaRole.Textbox, new() { Name = "Name" });
        ILocator txtDescription => page.GetByRole(AriaRole.Textbox, new() { Name = "Description" });
        ILocator txtPrice => page.Locator("#Price");
        ILocator txtProductType => page.GetByLabel("ProductType");
        ILocator btnCreate => page.GetByRole(AriaRole.Button, new() { Name = "Create" });

        public async Task<CreateProductPage> ValidateTitleAsync()
        {
            await pageTitleTxt.IsVisibleAsync();
            return this;
        }
        public async Task<ProductListPage> CreateProductAsync(string name, string description, string price, string productType)
        {
            await txtName.ClearAndFillElementAsync(name);
            await txtDescription.ClearAndFillElementAsync(description);
            await txtPrice.ClearAndFillElementAsync(price);
            await txtProductType.SelectDropDownWithTextAsync(productType);
            await btnCreate.ClickAsync();
            return new ProductListPage(page);
        }
    }
}
