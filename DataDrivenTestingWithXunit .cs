// using AutoFixture;
// using AutoFixture.Xunit2;
// using EAappProject.Driver;
// using EAappProject.Model;
// using EAappProject.Pages;
// using EAappProject.Utilities;
// using Microsoft.Playwright;
// using Xunit;
// using Xunit.Abstractions;
//
// namespace EAappProject
// {
//     public class DataDrivenTestingWithXunit : IClassFixture<PlaywrightDriver>
//     {
//
//         private readonly ITestOutputHelper _testOutputHelper;
//         private readonly PlaywrightDriver _playwrightDriver;
//         private IPage _page;
//
//         public DataDrivenTestingWithXunit(ITestOutputHelper testOutputHelper, PlaywrightDriver playwrightDriver)
//         {
//             _testOutputHelper = testOutputHelper;
//             _playwrightDriver = playwrightDriver;
//             _page = _playwrightDriver.InitializePlaywright().GetAwaiter().GetResult();
//         }
//
//         [Xunit.Theory]
//         [InlineData("TestProduct1", "This is a test product 1", 10, "MONITOR")]
//         [InlineData("TestProduct2", "This is a test product 2", 30, "MONITOR")]
//         [InlineData("TestProduct3", "This is a test product 3", 40, "MONITOR")]
//         [InlineData("TestProduct3", "This is a test product 3", 30, "MONITOR", "NewTestProduct")]
//         public async Task CreateDeleteProductAsync(string name, string description, int price, string productType, string? newProductData = null)
//         {
//             //Concert type creation
//             HomePage homePage = new HomePage(_page);
//             await homePage.ValidateTitleAsync();
//
//             var productList = await homePage.ClickProductListAsync();
//             await productList.ValidateTitleAsync();
//
//             var createProduct = await productList.CreateProductAsync();
//             await createProduct.ValidateTitleAsync();
//
//             var data = new ProductDetails
//             {
//                 Name = name,
//                 Description = description,
//                 Price = price,
//                 ProductType = ProductType.CPU
//             };
//
//             var updateData = new ProductDetails
//             {
//                 Name = newProductData,
//                 Description = description,
//                 Price = price,
//                 ProductType = ProductType.PERIPHARALS
//             };
//
//             await createProduct.CreateProductAsync(data);
//             await productList.IsProductExistAsync(data);
//
//             var deleteProduct = await productList.DeleteProductAsync(data);
//             await deleteProduct.ValidateTitleAsync();
//             await deleteProduct.DeleteAsync();
//             await productList.ValidateProductNotExistAsync(data);
//         }
//
//
//         [Xunit.Theory(Skip = "I dont want to run this test now")]
//         [MemberData(nameof(GetProductData))]
//         public async Task CreateDeleteProductUsingMemberDataAsync(ProductDetails data)
//         {
//             //Output the current test data to the console
//             _testOutputHelper.WriteLine($"Running the test for Product: {data.Name}, Description: {data.Description}, Price: {data.Price}, ProductType: {data.ProductType}");
//
//             HomePage homePage = new HomePage(_page);
//             await homePage.ValidateTitleAsync();
//
//             var productList = await homePage.ClickProductListAsync();
//             await productList.ValidateTitleAsync();
//
//             var createProduct = await productList.CreateProductAsync();
//             await createProduct.ValidateTitleAsync();
//
//             await createProduct.CreateProductAsync(data);
//             await productList.IsProductExistAsync(data);
//
//             var deleteProduct = await productList.DeleteProductAsync(data);
//             await deleteProduct.ValidateTitleAsync();
//             await deleteProduct.DeleteAsync();
//             await productList.ValidateProductNotExistAsync(data);
//         }
//
//        
//
//
//         public static IEnumerable<object[]> GetProductData()
//         {
//             yield return new object[] { new ProductDetails { Name = "TestProduct1", Description = "This is a test product 1", Price = 30, ProductType = ProductType.CPU } };
//             yield return new object[] { new ProductDetails { Name = "TestProduct2", Description = "This is a test product 2", Price = 30, ProductType = ProductType.PERIPHARALS } };
//             yield return new object[] { new ProductDetails { Name = "TestProduct3", Description = "This is a test product 3", Price = 30, ProductType = ProductType.EXTERNAL } };
//         }
//
//
//
//
//         [Xunit.Fact]
//         public async Task CreateDeleteProductWithFixtureAsync()
//         {
//             var fixture = new Fixture();
//             var data = fixture.Create<ProductDetails>();
//
//             HomePage homePage = new HomePage(_page);
//             await homePage.ValidateTitleAsync();
//
//             var productList = await homePage.ClickProductListAsync();
//             await productList.ValidateTitleAsync();
//
//             var createProduct = await productList.CreateProductAsync();
//             await createProduct.ValidateTitleAsync();
//
//             await createProduct.CreateProductAsync(data);
//             //await productList.IsProductExistAsync(data);
//
//             var deleteProduct = await productList.DeleteProductAsync(data);
//             //await deleteProduct.ValidateTitleAsync();
//             await deleteProduct.DeleteAsync();
//             //await productList.ValidateProductNotExistAsync(data);
//         }
//
//
//         [Xunit.Theory]
//         [AutoData]
//         public async Task CreateDeleteProductWithAutoFixtureAsync(ProductDetails data)
//         {
//             HomePage homePage = new HomePage(_page);
//             await homePage.ValidateTitleAsync();
//
//             var productList = await homePage.ClickProductListAsync();
//             await productList.ValidateTitleAsync();
//
//             var createProduct = await productList.CreateProductAsync();
//             await createProduct.ValidateTitleAsync();
//
//             await createProduct.CreateProductAsync(data);
//         }
//
//     }
// }
