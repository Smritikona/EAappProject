// using EAappProject.Pages;
// using EAappProject.Utilities;
// using Microsoft.Playwright;
// using Xunit;
// using System.Threading.Tasks;
//
// namespace EAappProject
// {
//
//     public class POMTestWithXunit : IAsyncLifetime
//     {
//         private IPage _page;
//         private IPlaywright _playwright;
//         private IBrowser _browser;
//         private IBrowserContext _context;
//
//         public async Task InitializeAsync()
//         {
//             //Playwright 
//             _playwright = await Playwright.CreateAsync();
//
//             //Browser Launch Settings
//             var browserSettings = new BrowserTypeLaunchOptions
//             {
//                 Headless = false,
//                 //SlowMo = 1000
//             };
//
//             //Browser
//             _browser = await _playwright.Chromium.LaunchAsync(browserSettings);
//
//             //Page
//             _context = await _browser.NewContextAsync();
//             _page = await _context.NewPageAsync();
//
//             //URL
//             await _page.GotoAsync("http://localhost:8000/");
//         }
//
//         [Fact]
//         public async Task CreateDeleteProductAsync()
//         {
//             var data = JsonHelper.ReadJsonFile();
//
//             HomePage homePage = new HomePage(_page);
//             await homePage.ValidateTitleAsync();
//
//             var productList = await homePage.ClickProductListAsync();
//             await productList.ValidateTitleAsync();
//
//             var createProduct = await productList.CreateProductAsync();
//             await createProduct.ValidateTitleAsync();
//             await createProduct.CreateProductAsync(data);
//             await productList.IsProductExistAsync(data);
//
//             var deleteProduct = await productList.DeleteProductAsync(data);
//             await deleteProduct.ValidateTitleAsync();
//             await deleteProduct.DeleteAsync();
//             await productList.ValidateProductNotExistAsync(data);
//         }
//
//         [Fact]
//         public async Task CreateModifyDeleteProductAsync()
//         {
//             var data = JsonHelper.ReadJsonFile();
//
//             HomePage homePage = new HomePage(_page);
//             await homePage.ValidateTitleAsync();
//
//             var productList = await homePage.ClickProductListAsync();
//             await productList.ValidateTitleAsync();
//
//             var createProduct = await productList.CreateProductAsync();
//             await createProduct.ValidateTitleAsync();
//             await createProduct.CreateProductAsync(data);
//             await productList.IsProductExistAsync(data);
//
//             var editProduct = await productList.EditProductAsync(data);
//             await editProduct.ValidateTitleAsync();
//             await editProduct.ValidateProductDetailsAsync(data);
//             await editProduct.UpdateAsync(data);
//             await productList.isModifiedProductExistAsync(data);
//
//             var deleteProduct = await productList.DeleteModifiedProductAsync(data);
//             await deleteProduct.ValidateTitleAsync();
//             await deleteProduct.DeleteAsync();
//             await productList.ValidateModifiedProductNotExistAsync(data);
//         }
//
//         [Fact]
//         public async Task CreateDetailsDeleteProductAsync()
//         {
//             var data = JsonHelper.ReadJsonFile();
//
//             HomePage homePage = new HomePage(_page);
//             await homePage.ValidateTitleAsync();
//
//             var productList = await homePage.ClickProductListAsync();
//             await productList.ValidateTitleAsync();
//
//             var createProduct = await productList.CreateProductAsync();
//             await createProduct.ValidateTitleAsync();
//             await createProduct.CreateProductAsync(data);
//             await productList.IsProductExistAsync(data);
//
//             var detailsProduct = await productList.DetailsProductAsync(data);
//             await detailsProduct.ValidateTitleAsync();
//             await detailsProduct.ValidateProductDetailsPage(data);
//             await detailsProduct.BackToListAsync();
//
//             var deleteProduct = await productList.DeleteProductAsync(data);
//             await deleteProduct.ValidateTitleAsync();
//             await deleteProduct.DeleteAsync();
//             await productList.ValidateProductNotExistAsync(data);
//         }
//
//         [Fact]
//         public async Task CreateDetailsEditDeleteProductAsync()
//         {
//             var data = JsonHelper.ReadJsonFile();
//
//             HomePage homePage = new HomePage(_page);
//             await homePage.ValidateTitleAsync();
//
//             var productList = await homePage.ClickProductListAsync();
//             await productList.ValidateTitleAsync();
//
//             var createProduct = await productList.CreateProductAsync();
//             await createProduct.ValidateTitleAsync();
//             await createProduct.CreateProductAsync(data);
//             await productList.IsProductExistAsync(data);
//
//             var detailsProduct = await productList.DetailsProductAsync(data);
//             await detailsProduct.ValidateTitleAsync();
//             await detailsProduct.ValidateProductDetailsPage(data);
//
//             var editProduct = await detailsProduct.GoToEditPageAsync();
//             await editProduct.ValidateTitleAsync();
//             await editProduct.ValidateProductDetailsAsync(data);
//             await editProduct.UpdateAsync(data);
//             await productList.isModifiedProductExistAsync(data);
//
//             var deleteProduct = await productList.DeleteModifiedProductAsync(data);
//             await deleteProduct.ValidateTitleAsync();
//             await deleteProduct.DeleteAsync();
//             await productList.ValidateModifiedProductNotExistAsync(data);
//         }
//
//         public async Task DisposeAsync()
//         {
//             await _page.CloseAsync();
//             await _context.CloseAsync();
//             await _browser.CloseAsync();
//             _playwright.Dispose();
//         }
//     }
// }