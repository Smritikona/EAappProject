using AutoFixture;
using AutoFixture.Xunit2;
using EAappProject.Model;
using EAappProject.Pages;
using Microsoft.Playwright;
using Xunit;

namespace EAappProject
{
    public class DataDrivenTestingWithXunit
    {
        private IHomePage _homePage;
        private IProductListPage _productListPage;
        private ICreateProductPage _createProductPage;
        private IDeletePage _deletePage;
        private IEditPage _editPage;
        private IDetailsPage _detailsPage;
        private IPage _page;

        public DataDrivenTestingWithXunit(IHomePage homePage, IProductListPage productListPage, ICreateProductPage createProductPage, IDetailsPage detailsPage, IEditPage editPage, IDeletePage deletePage)
        {
            _homePage = homePage;
            _productListPage = productListPage;
            _createProductPage = createProductPage;
            _deletePage = deletePage;
            _detailsPage = detailsPage;
            _editPage = editPage;
        }

        [Xunit.Theory]
        [InlineData("TestProduct1", "This is a test product 1", 10, "MONITOR")]
        [InlineData("TestProduct2", "This is a test product 2", 30, "MONITOR")]
        [InlineData("TestProduct3", "This is a test product 3", 40, "MONITOR")]
        [InlineData("TestProduct3", "This is a test product 3", 30, "MONITOR", "NewTestProduct")]
        public async Task CreateDeleteProductAsync(string name, string description, int price, string productType, string? newProductData = null)
        {
            await _homePage.ValidateTitleAsync();

            await _homePage.ClickProductListAsync();
            await _productListPage.ValidateTitleAsync();

            await _productListPage.CreateProductAsync();
            await _createProductPage.ValidateTitleAsync();

            var data = new ProductDetails
            {
                Name = name,
                Description = description,
                Price = price,
                ProductType = ProductType.CPU
            };

            var updateData = new ProductDetails
            {
                Name = newProductData,
                Description = description,
                Price = price,
                ProductType = ProductType.PERIPHARALS
            };

            await _createProductPage.CreateProductAsync(data);
            await _productListPage.IsProductExistAsync(data);

            await _productListPage.DeleteProductAsync(data);
            await _deletePage.ValidateTitleAsync();
            await _deletePage.DeleteAsync();
            await _productListPage.ValidateProductNotExistAsync(data);
        }


        [Xunit.Theory(Skip = "I dont want to run this test now")]
        [MemberData(nameof(GetProductData))]
        public async Task CreateDeleteProductUsingMemberDataAsync(ProductDetails data)
        {
            await _homePage.ValidateTitleAsync();

            await _homePage.ClickProductListAsync();
            await _productListPage.ValidateTitleAsync();

            await _productListPage.CreateProductAsync();
            await _createProductPage.ValidateTitleAsync();

            await _createProductPage.CreateProductAsync(data);
            await _productListPage.IsProductExistAsync(data);
            await _productListPage.DeleteProductAsync(data);
            await _deletePage.ValidateTitleAsync();
            await _deletePage.DeleteAsync();
            await _productListPage.ValidateProductNotExistAsync(data);
        }




        public static IEnumerable<object[]> GetProductData()
        {
            yield return new object[] { new ProductDetails { Name = "TestProduct1", Description = "This is a test product 1", Price = 30, ProductType = ProductType.CPU } };
            yield return new object[] { new ProductDetails { Name = "TestProduct2", Description = "This is a test product 2", Price = 30, ProductType = ProductType.PERIPHARALS } };
            yield return new object[] { new ProductDetails { Name = "TestProduct3", Description = "This is a test product 3", Price = 30, ProductType = ProductType.EXTERNAL } };
        }




        [Xunit.Fact]
        public async Task CreateDeleteProductWithFixtureAsync()
        {
            var fixture = new Fixture();
            var data = fixture.Create<ProductDetails>();

            await _homePage.ValidateTitleAsync();
            await _homePage.ClickProductListAsync();
            await _productListPage.ValidateTitleAsync();

            await _productListPage.CreateProductAsync();
            await _createProductPage.ValidateTitleAsync();

            await _createProductPage.CreateProductAsync(data);
            //await productList.IsProductExistAsync(data);
            await _productListPage.DeleteProductAsync(data);
            //await deleteProduct.ValidateTitleAsync();
            await _deletePage.DeleteAsync();
            //await productList.ValidateProductNotExistAsync(data);
        }


        [Xunit.Theory]
        [AutoData]
        public async Task CreateDeleteProductWithAutoFixtureAsync(ProductDetails data)
        {
            await _homePage.ValidateTitleAsync();
            await _homePage.ClickProductListAsync();
            await _productListPage.ValidateTitleAsync();
            await _productListPage.CreateProductAsync();
            await _createProductPage.ValidateTitleAsync();

            await _createProductPage.CreateProductAsync(data);
        }

    }
}
