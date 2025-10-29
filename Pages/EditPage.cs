// using EAappProject.Controls;
// using EAappProject.Model;
// using Microsoft.Playwright;
//
// namespace EAappProject.Pages;
//
// public class EditPage(IPage page)
// {
//     ILocator pageTitleTxt => page.Locator("h1", new() { HasText = "Edit" });
//     ILocator txtName => page.GetByRole(AriaRole.Textbox, new() { Name = "Name" });
//     ILocator txtDescription => page.GetByRole(AriaRole.Textbox, new() { Name = "Description" });
//     ILocator txtPrice => page.Locator("#Price");
//     ILocator txtProductType => page.GetByLabel("ProductType");
//     ILocator btnSave => page.GetByRole(AriaRole.Button, new() { Name = "Save" });
//
//
//     public async Task<EditPage> ValidateTitleAsync()
//     {
//         await Assertions.Expect(pageTitleTxt).ToBeVisibleAsync();
//         return new EditPage(page);
//     }
//
//     public async Task<EditPage> ValidateProductDetailsAsync(ProductDetails productDetails)
//     {
//         await Assertions.Expect(txtName).ToHaveValueAsync(productDetails.Name);
//         await Assertions.Expect(txtDescription).ToHaveValueAsync(productDetails.Description);
//         await Assertions.Expect(txtPrice).ToHaveValueAsync(productDetails.Price.ToString());
//         await Assertions.Expect(txtProductType).ToHaveValueAsync(productDetails.ProductType.ToString());
//
//         return new EditPage(page);
//     }
//     public async Task<ProductListPage> UpdateAsync(ProductDetails productDetails)
//     {
//         await txtPrice.ClearAndFillElementAsync(productDetails.NewPrice);
//         await btnSave.ClickAsync();
//         return new ProductListPage(page);
//     }
// }