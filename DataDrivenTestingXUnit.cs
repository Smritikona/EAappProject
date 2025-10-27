using AutoFixture;
using AutoFixture.Xunit2;
using EAappProject.Driver;
using EAappProject.Model;
using EAappProject.Pages;
using EAappProject.Utilities;
using Microsoft.Playwright;
using Xunit;
using Xunit.Abstractions;
using static EAappProject.Model.ProductDetails;

namespace EAappProject
{
    public class DataDrivenTestingXUnit : IClassFixture<PlaywrightDriver>
    {

        private IPage _page;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly PlaywrightDriver _playwrightDriver;
        private readonly Random _random = new Random();


        public DataDrivenTestingXUnit(ITestOutputHelper testOutputHelper, PlaywrightDriver playwrightDriver)
        {
            _testOutputHelper = testOutputHelper;
            _playwrightDriver = playwrightDriver;
            _page = _playwrightDriver.InitializeAsync().GetAwaiter().GetResult();
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
            
            HomePage homePage = new HomePage(_page);
            await homePage.ValidateTitleAsync();

            var productList = await homePage.ClickProductListAsync();
            await productList.ValidateTitleAsync();

            var createProduct = await productList.CreateProductAsync();
            await createProduct.pageTitleTxt.IsVisibleAsync();

            var data = new ProductDetails
            {
                Name = productName,
                Description = description,
                Price = price,
                ProductType = Enum.Parse<ProductType>(productType)
            };
            
            await createProduct.CreateProductAsync(data);
            await productList.IsProductExistAsync(data);

            var editProduct = await productList.EditProductAsync(data);

            data.Name = newProductName ?? productName;
            await editProduct.UpdateAsync(data);

            var deleteProduct = await productList.DeleteProductAsync(data);
            await deleteProduct.ValidateTitleAsync();
            await deleteProduct.DeleteAsync();
            await productList.ValidateProductNotExistAsync(data);

            //Output to console after test
            _testOutputHelper.WriteLine($"After test Product: {data.Name}, Description: {data.Description}, Price: {data.Price}, ProductType: {data.ProductType}");

        }

        [Xunit.Theory]
        [MemberData(nameof(GetProductData))]
        public async Task CreateDeleteProductWithMemberDataAsync(ProductDetails data)
        {
            //Output to console before test
            _testOutputHelper.WriteLine($"Before test Product: {data.Name}, Description: {data.Description}, Price: {data.Price}, ProductType: {data.ProductType}");

            HomePage homePage = new HomePage(_page);
            await homePage.ValidateTitleAsync();

            var productList = await homePage.ClickProductListAsync();
            await productList.ValidateTitleAsync();

            var createProduct = await productList.CreateProductAsync();
            await createProduct.pageTitleTxt.IsVisibleAsync();

            await createProduct.CreateProductAsync(data);
            await productList.IsProductExistAsync(data);

            var editProduct = await productList.EditProductAsync(data);

            data.Name = data.UpdatedName ?? data.Name;
            await editProduct.UpdateAsync(data);

            var deleteProduct = await productList.DeleteProductAsync(data);
            await deleteProduct.ValidateTitleAsync();
            await deleteProduct.DeleteAsync();
            await productList.ValidateProductNotExistAsync(data);

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

            HomePage homePage = new HomePage(_page);
            await homePage.ValidateTitleAsync();

            var productList = await homePage.ClickProductListAsync();
            await productList.ValidateTitleAsync();

            var createProduct = await productList.CreateProductAsync();
            await createProduct.pageTitleTxt.IsVisibleAsync();
            
            await createProduct.CreateProductAsync(data);
            await productList.IsProductExistAsync(data);

            //Output to console before test
            _testOutputHelper.WriteLine($"Before test Product: {data.Name}, Description: {data.Description}, Price: {data.Price}, ProductType: {data.ProductType}");

            var editProduct = await productList.EditProductAsync(data);

            data.Name = data.UpdatedName ?? data.Name;
            await editProduct.UpdateAsync(data);

            var deleteProduct = await productList.DeleteProductAsync(data);
            await deleteProduct.ValidateTitleAsync();
            await deleteProduct.DeleteAsync();
            await productList.ValidateProductNotExistAsync(data);

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

            HomePage homePage = new HomePage(_page);
            await homePage.ValidateTitleAsync();

            var productList = await homePage.ClickProductListAsync();
            await productList.ValidateTitleAsync();

            var createProduct = await productList.CreateProductAsync();
            await createProduct.pageTitleTxt.IsVisibleAsync();

            await createProduct.CreateProductAsync(data);
            await productList.IsProductExistAsync(data);

            //Output to console before edit
            _testOutputHelper.WriteLine($"Before test Product: {data.Name}, Description: {data.Description}, Price: {data.Price}, ProductType: {data.ProductType}");

            var editProduct = await productList.EditProductAsync(data);

            data.Name = data.UpdatedName ?? data.Name;
            data.Price += 10; 
            await editProduct.UpdateAsync(data);

            var deleteProduct = await productList.DeleteProductAsync(data);
            await deleteProduct.ValidateTitleAsync();
            await deleteProduct.DeleteAsync();
            await productList.ValidateProductNotExistAsync(data);

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
