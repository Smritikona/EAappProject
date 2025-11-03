using AutoFixture.Xunit2;
using EAappProject.Model;
using EAappProject.Pages;
using Microsoft.Playwright;

namespace EAappProject.Tests
{
    public class DataDrivenTestingWithXunitWithDI
    {
        private readonly IBasePage _basePage;
        private IPage _page;

        public DataDrivenTestingWithXunitWithDI(IBasePage basePage)
        {
            _basePage = basePage;
        }

        [Xunit.Theory]
        [AutoData]
        public async Task CreateDeleteProductWithAutoFixtureAsync(ProductDetails data)
        {
            await _basePage.HomePage.ValidateTitleAsync();

            await _basePage.HomePage.ClickProductListAsync();
            await _basePage.ProductListPage.ValidateTitleAsync();
            
            await _basePage.ProductListPage.CreateProductAsync();
            await _basePage.CreateProductPage.ValidateTitleAsync();
        }
        
        //Convert the code as a framework
        //Introduce Configuration

    }
}
