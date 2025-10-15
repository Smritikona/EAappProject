using EAappProject.Model;
using EAappProject.Pages;
using EAappProject.Utilities;
using Microsoft.Playwright;

namespace EAappProject
{
    public class POMTests
    {

        private IPage _page;


        [SetUp]
        public async Task SetupPlaywright()
        {

            //Playwright 
            var playwright = await Playwright.CreateAsync();

            //Browser Launch Settings
            var browserSettings = new BrowserTypeLaunchOptions
            {
                Headless = false
            };

            //Browser
            var browser = await playwright.Chromium.LaunchAsync(browserSettings);

            //Page
            var context = await browser.NewContextAsync();

            _page = await context.NewPageAsync();

            //URL
            await _page.GotoAsync("http://localhost:8000/");

        }


        [Test]
        public async Task CreateNewEmployeeWithPageNavigationAsync()
        {
            var product=JsonHelper.ReadJsonFile();
            HomePage homePage = new HomePage(_page);
            await homePage.ValidateTitleAsync();

            var productList = await homePage.ClickProductListAsync();
            await productList.ValidateTitleAsync();

            var createProduct = await productList.CreateProductAsync();
            await createProduct.ValidateTitleAsync();

            await createProduct.CreateProductAsync(product);
            await productList.IsProductExistAsync(product);
            var deleteProduct = await productList.DeleteProductAsync(product);
            await deleteProduct.ValidateTitleAsync();
            await deleteProduct.DeleteProductPage();
        }

        [Test]
        public async Task EditProductWithPageNavigationAsync()
        {
            var product = JsonHelper.ReadJsonFile();
            HomePage homePage = new HomePage(_page);
            await homePage.ValidateTitleAsync();

            var productList = await homePage.ClickProductListAsync();
            await productList.ValidateTitleAsync();
            var createProduct = await productList.CreateProductAsync();
            await createProduct.CreateProductAsync(product);
         
            if (productList.IsProductExistAsync(product).Result)
            {
                var editProduct = await productList.ClickOnEditProductAsync(product);
                var editProductDetails = new ProductDetails
                {
                    Name = "Working Laptop",
                    Price = "12799",
                    Description = "A high-performance laptop designed for working with high configuration",
                    ProductType = "PERIPHARALS"
                };
                await editProduct.EditProductAsync(editProductDetails);
                await productList.IsProductExistAsync(editProductDetails);
                var deleteProduct = await productList.DeleteProductAsync(editProductDetails);
                await deleteProduct.ValidateTitleAsync();
                await deleteProduct.DeleteProductPage();
            }
            else
            {
                Assert.Fail("Product does not exist to edit");
            }

        }

        [TearDown]
        public async Task ClosePlaywright() => await _page.CloseAsync();

    }
}
