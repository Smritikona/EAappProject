using EAappProject.Controls;
using EAappProject.Driver;
using EAappProject.Model;
using Microsoft.Playwright;

namespace EAappProject.Pages
{
    public interface ICreateProductPage
    {
        ILocator btnCreate { get; }
        ILocator pageTitleTxt { get; }
        ILocator txtDescription { get; }
        ILocator txtName { get; }
        ILocator txtPrice { get; }
        ILocator txtProductType { get; }
        Task CreateProductAsync(ProductDetails productDetails);
    }

    public class CreateProductPage(IPlaywrightDriver playwrightDriver) : ICreateProductPage

    {
        private IPage _page = playwrightDriver.InitializeAsync().Result;
        public ILocator pageTitleTxt => _page.GetByRole(AriaRole.Heading, new() { Name = "Create" });
        public ILocator txtName => _page.GetByRole(AriaRole.Textbox, new() { Name = "Name" });
        public ILocator txtDescription => _page.GetByRole(AriaRole.Textbox, new() { Name = "Description" });
        public ILocator txtPrice => _page.Locator("#Price");
        public ILocator txtProductType => _page.GetByLabel("ProductType");
        public ILocator btnCreate => _page.GetByRole(AriaRole.Button, new() { Name = "Create" });

        public async Task CreateProductAsync(ProductDetails productDetails)
        {
            await txtName.ClearAndFillElementAsync(productDetails.Name);
            await txtDescription.ClearAndFillElementAsync(productDetails.Description);
            await txtPrice.ClearAndFillElementAsync(productDetails.Price.ToString());
            //await txtProductType.SelectDropDownWithIndexAsync(1);
            await txtProductType.SelectDropDownWithTextAsync(productDetails.ProductType.ToString());
            await btnCreate.ClickAsync();
        }
    }
}
