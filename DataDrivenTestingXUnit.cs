using AutoFixture;
using AutoFixture.Xunit2;
using EAappProject.Base;
using EAappProject.Driver;
using EAappProject.Model;
using Xunit;
using Xunit.Abstractions;

namespace EAappProject
{
    public class DataDrivenTestingXUnit : IClassFixture<PlaywrightDriver>
    {
        private readonly IBasePage _basePage;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Random _random = new();

        public DataDrivenTestingXUnit(
            IBasePage basePage,
            ITestOutputHelper testOutputHelper)
        {
            _basePage = basePage;
            _testOutputHelper = testOutputHelper;
        }

        [Xunit.Theory]
        [InlineData("Test Product1", "This is 1st Test Product", 10, "CPU", null)]
        [InlineData("Test Product2", "This is 2nd Test Product", 20, "MONITOR", null)]
        [InlineData("Test Product3", "This is 3rd Test Product", 30, "EXTERNAL", null)]
        [InlineData("Test Product4", "This is 4th Test Product", 40, "EXTERNAL", "New Test Product4")]
        public async Task CreateDeleteProductAsync(string productName, string description, int price, string productType, string? newProductName)
        {
            //Output to console before test
            _testOutputHelper.WriteLine($"Before test Product: {productName}, Description: {description}, Price: {price}, ProductType: {productType}");

            await _basePage.HomePage.ValidateTitleAsync();
            await _basePage.HomePage.ClickProductListAsync();
            await _basePage.ProductListPage.ValidateTitleAsync();

            await _basePage.ProductListPage.CreateProductAsync();
            await _basePage.CreateProductPage.pageTitleTxt.IsVisibleAsync();

            var data = new ProductDetails
            {
                Name = productName,
                Description = description,
                Price = price,
                ProductType = Enum.Parse<ProductType>(productType)
            };

            await _basePage.CreateProductPage.CreateProductAsync(data);
            await _basePage.ProductListPage.IsProductExistAsync(data);
            await _basePage.ProductListPage.EditProductAsync(data);

            data.Name = newProductName ?? productName;
            await _basePage.EditPage.UpdateAsync(data);

            await _basePage.ProductListPage.DeleteProductAsync(data);
            await _basePage.DeletePage.ValidateTitleAsync();
            await _basePage.DeletePage.DeleteAsync();
            await _basePage.ProductListPage.ValidateProductNotExistAsync(data);

            //Output to console after test
            _testOutputHelper.WriteLine($"After test Product: {data.Name}, Description: {data.Description}, Price: {data.Price}, ProductType: {data.ProductType}");

        }

        [Xunit.Theory]
        [MemberData(nameof(GetProductData))]
        public async Task CreateDeleteProductWithMemberDataAsync(ProductDetails data)
        {
            //Output to console before test
            _testOutputHelper.WriteLine($"Before test Product: {data.Name}, Description: {data.Description}, Price: {data.Price}, ProductType: {data.ProductType}");

            await _basePage.HomePage.ValidateTitleAsync();
            await _basePage.HomePage.ClickProductListAsync();
            await _basePage.ProductListPage.ValidateTitleAsync();

            await _basePage.ProductListPage.CreateProductAsync();
            await _basePage.CreateProductPage.pageTitleTxt.IsVisibleAsync();
            await _basePage.CreateProductPage.CreateProductAsync(data);
            await _basePage.ProductListPage.IsProductExistAsync(data);
            await _basePage.ProductListPage.EditProductAsync(data);

            data.Name = data.UpdatedName ?? data.Name;
            await _basePage.EditPage.UpdateAsync(data);

            await _basePage.ProductListPage.DeleteProductAsync(data);
            await _basePage.DeletePage.ValidateTitleAsync();
            await _basePage.DeletePage.DeleteAsync();
            await _basePage.ProductListPage.ValidateProductNotExistAsync(data);

            //Output to console after test
            _testOutputHelper.WriteLine($"After test Product: {data.Name}, Description: {data.Description}, Price: {data.Price}, ProductType: {data.ProductType}");
        }

        [Xunit.Fact]
        public async Task CreateDeleteProductWithFixtureAsync()
        {
            var fixture = new Fixture();
            var data = fixture.Create<ProductDetails>();

            var values = Enum.GetValues(typeof(ProductType));
            var randomProductType = (ProductType)values.GetValue(_random.Next(values.Length));
            data.ProductType = randomProductType;

            await _basePage.HomePage.ValidateTitleAsync();
            await _basePage.HomePage.ClickProductListAsync();
            await _basePage.ProductListPage.ValidateTitleAsync();

            await _basePage.ProductListPage.CreateProductAsync();
            await _basePage.CreateProductPage.pageTitleTxt.IsVisibleAsync();
            await _basePage.CreateProductPage.CreateProductAsync(data);
            await _basePage.ProductListPage.IsProductExistAsync(data);

            //Output to console before test
            _testOutputHelper.WriteLine($"Before test Product: {data.Name}, Description: {data.Description}, Price: {data.Price}, ProductType: {data.ProductType}");

            await _basePage.ProductListPage.EditProductAsync(data);

            data.Name = data.UpdatedName ?? data.Name;
            await _basePage.EditPage.UpdateAsync(data);

            await _basePage.ProductListPage.DeleteProductAsync(data);
            await _basePage.DeletePage.ValidateTitleAsync();
            await _basePage.DeletePage.DeleteAsync();
            await _basePage.ProductListPage.ValidateProductNotExistAsync(data);

            //Output to console after edit
            _testOutputHelper.WriteLine($"After test Product: {data.Name}, Description: {data.Description}, Price: {data.Price}, ProductType: {data.ProductType}");
        }

        [Xunit.Theory]
        [AutoData]
        public async Task CreateDeleteProductWithAutoFixtureAsync(ProductDetails data)
        {
            var values = Enum.GetValues(typeof(ProductType));
            var randomProductType = (ProductType)values.GetValue(_random.Next(values.Length));
            data.ProductType = randomProductType;

            await _basePage.HomePage.ValidateTitleAsync();
            await _basePage.HomePage.ClickProductListAsync();
            await _basePage.ProductListPage.ValidateTitleAsync();

            await _basePage.ProductListPage.CreateProductAsync();
            await _basePage.CreateProductPage.pageTitleTxt.IsVisibleAsync();
            await _basePage.CreateProductPage.CreateProductAsync(data);
            await _basePage.ProductListPage.IsProductExistAsync(data);

            //Output to console before edit
            _testOutputHelper.WriteLine($"Before test Product: {data.Name}, Description: {data.Description}, Price: {data.Price}, ProductType: {data.ProductType}");

            await _basePage.ProductListPage.EditProductAsync(data);

            data.Name = data.UpdatedName ?? data.Name;
            await _basePage.EditPage.UpdateAsync(data);

            await _basePage.ProductListPage.DeleteProductAsync(data);
            await _basePage.DeletePage.ValidateTitleAsync();
            await _basePage.DeletePage.DeleteAsync();
            await _basePage.ProductListPage.ValidateProductNotExistAsync(data);

            //Output to console after edit
            _testOutputHelper.WriteLine($"After test Product: {data.Name}, Description: {data.Description}, Price: {data.Price}, ProductType: {data.ProductType}");
        }
        public static IEnumerable<object[]> GetProductData()
        {
            yield return new object[] { new ProductDetails { Name = "TestProduct1", Description = "This is Member Product1", Price = 15, ProductType = ProductType.CPU } };
            yield return new object[] { new ProductDetails { Name = "TestProduct2", Description = "This is Member Product2", Price = 25, ProductType = ProductType.MONITOR } };
            yield return new object[] { new ProductDetails { Name = "TestProduct3", Description = "This is Member Product3", Price = 35, ProductType = ProductType.PERIPHARALS } };
            yield return new object[] { new ProductDetails { Name = "TestProduct4", Description = "This is Member Product4", Price = 45, ProductType = ProductType.EXTERNAL, UpdatedName = "New TestPorduct4" } };
        }

    }
}
