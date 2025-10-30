using AutoFixture;
using AutoFixture.Xunit2;
using EAappProject.Driver;
using EAappProject.Model;
using EAappProject.Pages;
using EAappProject.Pages.Interfaces;
using EAappProject.Utilities;
using Microsoft.Playwright;
using Xunit;
using Xunit.Abstractions;

namespace EAappProject
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
