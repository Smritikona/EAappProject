// using Microsoft.Playwright;
//
// namespace EAappProject.Pages;
//
// public class DeletePage(IPage page)
// {
//     ILocator pageTitleTxt => page.Locator("h1", new() { HasText = "Delete" });
//     ILocator btnDelete => page.GetByRole(AriaRole.Button, new() { Name = "Delete" });
//
//     public async Task<DeletePage> ValidateTitleAsync()
//     {
//         await Assertions.Expect(pageTitleTxt).ToBeVisibleAsync();
//         return new DeletePage(page);
//     }
//
//     public async Task<ProductListPage> DeleteAsync()
//     {
//         await btnDelete.ClickAsync();
//         return new ProductListPage(page);
//     }
// }