using EAappProject.Pages;
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
            HomePage homePage = new HomePage(_page);
            await homePage.ValidateTitleAsync();

            var productList = await homePage.ClickProductListAsync();
            await productList.ValidateTitleAsync();

            var createProduct = await productList.CreateProductAsync();
            await createProduct.ValidateTitleAsync();

            await createProduct.CreateProductAsync("Gaming Mouse", "RGB feature", "200", "PERIPHARALS");
            await productList.IsProductExistAsync("Gaming Mouse", "RGB feature", "200", "PERIPHARALS");
            var deleteProduct = await productList.DeleteProductAsync("Gaming Mouse", "RGB feature", "200", "PERIPHARALS");
            await deleteProduct.ValidateTitleAsync();
            await deleteProduct.DeleteProductPage();
        }



        [TearDown]
        public async Task ClosePlaywright() => await _page.CloseAsync();

    }
}
