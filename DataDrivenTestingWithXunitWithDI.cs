using AutoFixture;
using AutoFixture.Xunit2;
using EAappProject.Driver;
using EAappProject.Model;
using EAappProject.Pages;
using EAappProject.Utilities;
using Microsoft.Playwright;
using Xunit;
using Xunit.Abstractions;

namespace EAappProject
{
    public class DataDrivenTestingWithXunitWithDI
    {
        private readonly IHomePage _homePage;
        private readonly IProductListPage _productListPage;
        private IPage _page;

        public DataDrivenTestingWithXunitWithDI(
            IHomePage homePage, 
            IProductListPage productListPage)
        {
            _homePage = homePage;
            _productListPage = productListPage;
        }

        [Xunit.Theory]
        [AutoData]
        public async Task CreateDeleteProductWithAutoFixtureAsync(ProductDetails data)
        {
            await _homePage.ValidateTitleAsync();

            await _homePage.ClickProductListAsync();
            await _productListPage.ValidateTitleAsync();
            
            await _productListPage.CreateProductAsync();
            // await createProduct.ValidateTitleAsync();
            //
            // await createProduct.CreateProductAsync(data);
        }

    }
}
