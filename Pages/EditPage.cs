using EAappProject.Controls;
using EAappProject.Model;
using Microsoft.Playwright;

namespace EAappProject.Pages
{
    public class EditPage(IPage page)
    {
        ILocator txtName => page.GetByRole(AriaRole.Textbox, new() { Name = "Name" });
        ILocator txtDescription => page.GetByRole(AriaRole.Textbox, new() { Name = "Description" });
        ILocator txtPrice => page.Locator("#Price");
        ILocator productType => page.GetByRole(AriaRole.Textbox, new() { Name = "ProductType" });
        ILocator btnSave => page.GetByRole(AriaRole.Button, new() { Name = "Save" });

        public async Task<ProductListPage> EditProductAsync(ProductDetails product)
        {
            await txtName.ClearAndFillElementAsync(product.Name);
            await txtDescription.ClearAndFillElementAsync(product.Description);
            await txtPrice.ClearAndFillElementAsync(product.Price);
            await productType.ClearAndFillElementAsync(product.ProductType);
            await btnSave.ClickAsync();
            return new ProductListPage(page);
        }

    }
}
