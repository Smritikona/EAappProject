// using EAappProject.Model;
// using Microsoft.Playwright;
//
// namespace EAappProject.Pages;
//
// public class DetailsPage(IPage page)
// {
//     ILocator pageTitleTxt => page.Locator("h1", new() { HasText = "Details" });
//     ILocator txtName => page.Locator("#Name");
//     ILocator txtDescription => page.Locator("#Description");
//     ILocator txtPrice => page.Locator("#Price");
//     ILocator txtProductType => page.Locator("#ProductType");
//     ILocator btnBackToList => page.GetByRole(AriaRole.Link, new() { Name = "Back to List" });
//     ILocator btnEdit => page.GetByRole(AriaRole.Link, new() { Name = "Edit" });
//
//
//
//     public async Task<DetailsPage> ValidateTitleAsync()
//     {
//         await Assertions.Expect(pageTitleTxt).ToBeVisibleAsync();
//         return new DetailsPage(page);
//     }
//
//     public async Task<DetailsPage> ValidateProductDetailsPage(ProductDetails productDetails)
//     {
//         await Assertions.Expect(txtName).ToHaveTextAsync(productDetails.Name);
//         await Assertions.Expect(txtDescription).ToHaveTextAsync(productDetails.Description);
//         await Assertions.Expect(txtPrice).ToHaveTextAsync(productDetails.Price.ToString());
//         await Assertions.Expect(txtProductType).ToHaveTextAsync(productDetails.ProductType.ToString());
//
//         return new DetailsPage(page);
//     }
//
//     public async Task<EditPage> GoToEditPageAsync()
//     {
//         await btnEdit.ClickAsync();
//         return new EditPage(page);
//     }
//
//     public async Task<ProductListPage> BackToListAsync()
//     {
//         await btnBackToList.ClickAsync();
//         return new ProductListPage(page);
//     }
// }