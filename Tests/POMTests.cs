// using EAappProject.Pages;
// using EAappProject.Utilities;
// using Microsoft.Playwright;
//
// namespace EAappProject
// {
//     public class POMTests
//     {
//
//         private IPage _page;
//
//         [SetUp]
//         public async Task SetupPlaywright()
//         {
//             //Playwright 
//             var playwright = await Playwright.CreateAsync();
//
//             //Browser Launch Settings
//             var browserSettings = new BrowserTypeLaunchOptions
//             {
//                 Headless = false,
//                 //SlowMo = 1000
//             };
//
//             //Browser
//             var browser = await playwright.Chromium.LaunchAsync(browserSettings);
//
//             //Page
//             var context = await browser.NewContextAsync();
//             _page = await context.NewPageAsync();
//
//             //URL
//             await _page.GotoAsync("http://localhost:8000/");
//         }
//
//
//         [Test]
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
//         [Test]
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
//         [Test]
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
//         [Test]
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
//         [TearDown]
//         public async Task ClosePlaywright() => await _page.CloseAsync();
//
//     }
// }
