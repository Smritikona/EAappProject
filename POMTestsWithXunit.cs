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
    public class POMTestsWithXunit : IClassFixture<PlaywrightDriver>
    {
        private readonly IHomePage _homePage;
        private readonly IProductListPage _productListPage;
        private readonly ICreateProductPage _createProductPage;
        private readonly IDeletePage _deletePage;
        private readonly IEditPage _editPage;
        private readonly IDetailsPage _detailsPage;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Random _random = new();

        public POMTestsWithXunit(
            IHomePage homePage,
            IProductListPage productListPage,
            IDetailsPage detailsPage,
            ICreateProductPage createProductPage,
            IDeletePage deletePage,
            IEditPage editPage,
            ITestOutputHelper testOutputHelper)
        {
            _homePage = homePage;
            _productListPage = productListPage;
            _createProductPage = createProductPage;
            _deletePage = deletePage;
            _editPage = editPage;
            _detailsPage = detailsPage;
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task CreateDeleteProductAsync()
        {
            var data = JsonHelper.ReadJsonFile();

            await _homePage.ValidateTitleAsync();
            await _homePage.ClickProductListAsync();
            await _productListPage.ValidateTitleAsync();

            await _productListPage.CreateProductAsync();
            await _createProductPage.pageTitleTxt.IsVisibleAsync();
            await _createProductPage.CreateProductAsync(data);
            await _productListPage.IsProductExistAsync(data);

            _testOutputHelper.WriteLine($"Created Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");

            await _productListPage.DeleteProductAsync(data);
            await _deletePage.ValidateTitleAsync();
            await _deletePage.DeleteAsync();
            await _productListPage.ValidateProductNotExistAsync(data);

            _testOutputHelper.WriteLine($"Completed Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");
        }

        [Fact]
        public async Task CreateModifyDeleteProductAsync()
        {
            var data = JsonHelper.ReadJsonFile();

            await _homePage.ValidateTitleAsync();
            await _homePage.ClickProductListAsync();
            await _productListPage.ValidateTitleAsync();

            await _productListPage.CreateProductAsync();
            await _createProductPage.pageTitleTxt.IsVisibleAsync();
            await _createProductPage.CreateProductAsync(data);
            await _productListPage.IsProductExistAsync(data);

            _testOutputHelper.WriteLine($"Created Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");

            //Edit product
            await _productListPage.EditProductAsync(data);
            await Assertions.Expect(_editPage.pageTitleTxt).ToBeVisibleAsync();
            await Assertions.Expect(_editPage.txtName).ToHaveValueAsync(data.Name);
            await Assertions.Expect(_editPage.txtDescription).ToHaveValueAsync(data.Description);
            await Assertions.Expect(_editPage.txtPrice).ToHaveValueAsync(data.Price.ToString());
            await Assertions.Expect(_editPage.txtProductType).ToHaveValueAsync(data.ProductType.ToString());

            data.Price = 3000;
            data.ProductType = ProductType.PERIPHARALS;
            await _editPage.UpdateAsync(data);
            await _productListPage.IsProductExistAsync(data);

            await _productListPage.DeleteProductAsync(data);
            await _deletePage.ValidateTitleAsync();
            await _deletePage.DeleteAsync();
            await _productListPage.ValidateProductNotExistAsync(data);

            _testOutputHelper.WriteLine($"Completed Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");
        }

        [Fact]
        public async Task CreateDetailsDeleteProductAsync()
        {
            var data = JsonHelper.ReadJsonFile();

            await _homePage.ValidateTitleAsync();
            await _homePage.ClickProductListAsync();
            await _productListPage.ValidateTitleAsync();

            await _productListPage.CreateProductAsync();
            await _createProductPage.pageTitleTxt.IsVisibleAsync();
            await _createProductPage.CreateProductAsync(data);
            await _productListPage.IsProductExistAsync(data);

            _testOutputHelper.WriteLine($"Created Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");

            //Details product
            await _productListPage.DetailsProductAsync(data);
            await Assertions.Expect(_detailsPage.pageTitleTxt).ToBeVisibleAsync();
            await Assertions.Expect(_detailsPage.txtName).ToHaveTextAsync(data.Name);
            await Assertions.Expect(_detailsPage.txtPrice).ToHaveTextAsync(data.Price.ToString());
            await Assertions.Expect(_detailsPage.txtDescription).ToHaveTextAsync(data.Description);
            await Assertions.Expect(_detailsPage.txtProductType).ToHaveTextAsync(data.ProductType.ToString());

            await _detailsPage.BackToListAsync();

            await _productListPage.DeleteProductAsync(data);
            await _deletePage.ValidateTitleAsync();
            await _deletePage.DeleteAsync();
            await _productListPage.ValidateProductNotExistAsync(data);

            _testOutputHelper.WriteLine($"Completed Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");
        }

        [Fact]
        public async Task CreateDetailsEditDeleteProductAsync()
        {
            var data = JsonHelper.ReadJsonFile();

            await _homePage.ValidateTitleAsync();
            await _homePage.ClickProductListAsync();
            await _productListPage.ValidateTitleAsync();

            await _productListPage.CreateProductAsync();
            await _createProductPage.pageTitleTxt.IsVisibleAsync();
            await _createProductPage.CreateProductAsync(data);
            await _productListPage.IsProductExistAsync(data);

            _testOutputHelper.WriteLine($"Created Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");

            //Details product
            await _productListPage.DetailsProductAsync(data);
            await Assertions.Expect(_detailsPage.pageTitleTxt).ToBeVisibleAsync();
            await Assertions.Expect(_detailsPage.txtName).ToHaveTextAsync(data.Name);
            await Assertions.Expect(_detailsPage.txtPrice).ToHaveTextAsync(data.Price.ToString());
            await Assertions.Expect(_detailsPage.txtDescription).ToHaveTextAsync(data.Description);
            await Assertions.Expect(_detailsPage.txtProductType).ToHaveTextAsync(data.ProductType.ToString());

            await _detailsPage.GoToEditPageAsync();
            await Assertions.Expect(_editPage.pageTitleTxt).ToBeVisibleAsync();
            await Assertions.Expect(_editPage.txtName).ToHaveValueAsync(data.Name);
            await Assertions.Expect(_editPage.txtDescription).ToHaveValueAsync(data.Description);
            await Assertions.Expect(_editPage.txtPrice).ToHaveValueAsync(data.Price.ToString());
            await Assertions.Expect(_editPage.txtProductType).ToHaveValueAsync(data.ProductType.ToString());

            data.Price = 3000;
            data.ProductType = ProductType.PERIPHARALS;
            await _editPage.UpdateAsync(data);
            await _productListPage.IsProductExistAsync(data);

            await _productListPage.DeleteProductAsync(data);
            await _deletePage.ValidateTitleAsync();
            await _deletePage.DeleteAsync();
            await _productListPage.ValidateProductNotExistAsync(data);

            _testOutputHelper.WriteLine($"Completed Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");
        }
    }
}
