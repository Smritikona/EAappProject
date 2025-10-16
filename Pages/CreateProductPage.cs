using EAappProject.Controls;
using EAappProject.Model;
using Microsoft.Playwright;

namespace EAappProject.Pages
{
    public class CreateProductPage(IPage page)
    {
        public ILocator pageTitleTxt => page.GetByRole(AriaRole.Heading, new() { Name = "Create" });
        public ILocator txtName => page.GetByRole(AriaRole.Textbox, new() { Name = "Name" });
        public ILocator txtDescription => page.GetByRole(AriaRole.Textbox, new() { Name = "Description" });
        public ILocator txtPrice => page.Locator("#Price");
        public ILocator txtProductType => page.GetByLabel("ProductType");
        public ILocator btnCreate => page.GetByRole(AriaRole.Button, new() { Name = "Create" });

        public async Task<ProductListPage> CreateProductAsync(ProductDetails productDetails)
        {
            await txtName.ClearAndFillElementAsync(productDetails.Name);
            await txtDescription.ClearAndFillElementAsync(productDetails.Description);
            await txtPrice.ClearAndFillElementAsync(productDetails.Price);
            await txtProductType.SelectDropDownWithTextAsync(productDetails.ProductType);
            await btnCreate.ClickAsync();
            return new ProductListPage(page);
        }
    }
}
