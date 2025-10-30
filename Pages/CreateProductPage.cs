using EAappProject.Controls;
using EAappProject.Driver;
using EAappProject.Model;
using Microsoft.Playwright;

namespace EAappProject.Pages
{
    public interface ICreateProductPage
    {
        Task CreateProductAsync(ProductDetails productDetails);
        Task ValidateTitleAsync();
    }

    public class CreateProductPage : ICreateProductPage
    {
        public CreateProductPage(IPage page)
        {
            _page = page;
        }
        private IPage _page;
        ILocator pageTitleTxt => _page.GetByRole(AriaRole.Heading, new() { Name = "Create" });
        ILocator txtName => _page.GetByRole(AriaRole.Textbox, new() { Name = "Name" });
        ILocator txtDescription => _page.GetByRole(AriaRole.Textbox, new() { Name = "Description" });
        ILocator txtPrice => _page.Locator("#Price");
        ILocator txtProductType => _page.GetByLabel("ProductType");
        ILocator btnCreate => _page.GetByRole(AriaRole.Button, new() { Name = "Create" });

        public async Task ValidateTitleAsync()
        {
            await pageTitleTxt.IsVisibleAsync();
        }
        public async Task CreateProductAsync(ProductDetails productDetails)
        {
            await txtName.ClearAndFillElementAsync(productDetails.Name);
            await txtDescription.ClearAndFillElementAsync(productDetails.Description);
            await txtPrice.ClearAndFillElementAsync(productDetails.Price.ToString());
            await txtProductType.SelectDropDownWithValueAsync(productDetails.ProductType.ToString());
            await btnCreate.ClickAsync();
        }
    }
}
