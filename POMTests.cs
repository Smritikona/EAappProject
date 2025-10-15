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
                Headless = false,
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
            var data = JsonHelper.ReadJsonFile();

            HomePage homePage = new HomePage(_page);
            await homePage.ValidateTitleAsync();

            var productList = await homePage.ClickProductListAsync();
            await productList.ValidateTitleAsync();

            var createProduct = await productList.CreateProductAsync();
            await createProduct.ValidateTitleAsync();


            await createProduct.CreateProductAsync(data.Name,data.Description,data.Price,data.ProductType);

            await productList.IsProductExistAsync(data.Name, data.Description, data.Price, data.ProductType);

            var deleteProduct = await productList.DeleteProductAsync(data.Name, data.Description, data.Price, data.ProductType);

            await deleteProduct.ValidateTitleAsync();

            await deleteProduct.DeleteProductPage();

        }



        [TearDown]
        public async Task ClosePlaywright() => await _page.CloseAsync();

    }
}
