using EAappProject.Base;
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
    public class POMTestsWithXunit
    {
        private readonly IBasePage _basePage;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Random _random = new();

        public POMTestsWithXunit(
            IBasePage basePage,
            ITestOutputHelper testOutputHelper)
        {
            _basePage = basePage;
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task CreateDeleteProductAsync()
        {
            var data = JsonHelper.ReadJsonFile();

            await _basePage.HomePage.ValidateTitleAsync();
            await _basePage.HomePage.ClickProductListAsync();
            await _basePage.ProductListPage.ValidateTitleAsync();

            await _basePage.ProductListPage.CreateProductAsync();
            await _basePage.CreateProductPage.pageTitleTxt.IsVisibleAsync();
            await _basePage.CreateProductPage.CreateProductAsync(data);
            await _basePage.ProductListPage.IsProductExistAsync(data);

            _testOutputHelper.WriteLine($"Created Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");

            await _basePage.ProductListPage.DeleteProductAsync(data);
            await _basePage.DeletePage.ValidateTitleAsync();
            await _basePage.DeletePage.DeleteAsync();
            await _basePage.ProductListPage.ValidateProductNotExistAsync(data);

            _testOutputHelper.WriteLine($"Completed Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");
        }

        [Fact]
        public async Task CreateModifyDeleteProductAsync()
        {
            var data = JsonHelper.ReadJsonFile();

            await _basePage.HomePage.ValidateTitleAsync();
            await _basePage.HomePage.ClickProductListAsync();
            await _basePage.ProductListPage.ValidateTitleAsync();

            await _basePage.ProductListPage.CreateProductAsync();
            await _basePage.CreateProductPage.pageTitleTxt.IsVisibleAsync();
            await _basePage.CreateProductPage.CreateProductAsync(data);
            await _basePage.ProductListPage.IsProductExistAsync(data);

            _testOutputHelper.WriteLine($"Created Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");

            //Edit product
            await _basePage.ProductListPage.EditProductAsync(data);
            await Assertions.Expect(_basePage.EditPage.pageTitleTxt).ToBeVisibleAsync();
            await Assertions.Expect(_basePage.EditPage.txtName).ToHaveValueAsync(data.Name);
            await Assertions.Expect(_basePage.EditPage.txtDescription).ToHaveValueAsync(data.Description);
            await Assertions.Expect(_basePage.EditPage.txtPrice).ToHaveValueAsync(data.Price.ToString());
            await Assertions.Expect(_basePage.EditPage.txtProductType).ToHaveValueAsync(data.ProductType.ToString());

            data.Price = 3000;
            data.ProductType = ProductType.PERIPHARALS;
            await _basePage.EditPage.UpdateAsync(data);
            await _basePage.ProductListPage.IsProductExistAsync(data);

            await _basePage.ProductListPage.DeleteProductAsync(data);
            await _basePage.DeletePage.ValidateTitleAsync();
            await _basePage.DeletePage.DeleteAsync();
            await _basePage.ProductListPage.ValidateProductNotExistAsync(data);

            _testOutputHelper.WriteLine($"Completed Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");
        }

        [Fact]
        public async Task CreateDetailsDeleteProductAsync()
        {
            var data = JsonHelper.ReadJsonFile();

            await _basePage.HomePage.ValidateTitleAsync();
            await _basePage.HomePage.ClickProductListAsync();
            await _basePage.ProductListPage.ValidateTitleAsync();

            await _basePage.ProductListPage.CreateProductAsync();
            await _basePage.CreateProductPage.pageTitleTxt.IsVisibleAsync();
            await _basePage.CreateProductPage.CreateProductAsync(data);
            await _basePage.ProductListPage.IsProductExistAsync(data);

            _testOutputHelper.WriteLine($"Created Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");

            //Details product
            await _basePage.ProductListPage.DetailsProductAsync(data);
            await Assertions.Expect(_basePage.DetailsPage.pageTitleTxt).ToBeVisibleAsync();
            await Assertions.Expect(_basePage.DetailsPage.txtName).ToHaveTextAsync(data.Name);
            await Assertions.Expect(_basePage.DetailsPage.txtPrice).ToHaveTextAsync(data.Price.ToString());
            await Assertions.Expect(_basePage.DetailsPage.txtDescription).ToHaveTextAsync(data.Description);
            await Assertions.Expect(_basePage.DetailsPage.txtProductType).ToHaveTextAsync(data.ProductType.ToString());

            await _basePage.DetailsPage.BackToListAsync();

            await _basePage.ProductListPage.DeleteProductAsync(data);
            await _basePage.DeletePage.ValidateTitleAsync();
            await _basePage.DeletePage.DeleteAsync();
            await _basePage.ProductListPage.ValidateProductNotExistAsync(data);

            _testOutputHelper.WriteLine($"Completed Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");
        }

        [Fact]
        public async Task CreateDetailsEditDeleteProductAsync()
        {
            var data = JsonHelper.ReadJsonFile();

            await _basePage.HomePage.ValidateTitleAsync();
            await _basePage.HomePage.ClickProductListAsync();
            await _basePage.ProductListPage.ValidateTitleAsync();

            await _basePage.ProductListPage.CreateProductAsync();
            await _basePage.CreateProductPage.pageTitleTxt.IsVisibleAsync();
            await _basePage.CreateProductPage.CreateProductAsync(data);
            await _basePage.ProductListPage.IsProductExistAsync(data);

            _testOutputHelper.WriteLine($"Created Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");

            //Details product
            await _basePage.ProductListPage.DetailsProductAsync(data);
            await Assertions.Expect(_basePage.DetailsPage.pageTitleTxt).ToBeVisibleAsync();
            await Assertions.Expect(_basePage.DetailsPage.txtName).ToHaveTextAsync(data.Name);
            await Assertions.Expect(_basePage.DetailsPage.txtPrice).ToHaveTextAsync(data.Price.ToString());
            await Assertions.Expect(_basePage.DetailsPage.txtDescription).ToHaveTextAsync(data.Description);
            await Assertions.Expect(_basePage.DetailsPage.txtProductType).ToHaveTextAsync(data.ProductType.ToString());

            await _basePage.DetailsPage.GoToEditPageAsync();
            await Assertions.Expect(_basePage.EditPage.pageTitleTxt).ToBeVisibleAsync();
            await Assertions.Expect(_basePage.EditPage.txtName).ToHaveValueAsync(data.Name);
            await Assertions.Expect(_basePage.EditPage.txtDescription).ToHaveValueAsync(data.Description);
            await Assertions.Expect(_basePage.EditPage.txtPrice).ToHaveValueAsync(data.Price.ToString());
            await Assertions.Expect(_basePage.EditPage.txtProductType).ToHaveValueAsync(data.ProductType.ToString());

            data.Price = 3000;
            data.ProductType = ProductType.PERIPHARALS;
            await _basePage.EditPage.UpdateAsync(data);
            await _basePage.ProductListPage.IsProductExistAsync(data);

            await _basePage.ProductListPage.DeleteProductAsync(data);
            await _basePage.DeletePage.ValidateTitleAsync();
            await _basePage.DeletePage.DeleteAsync();
            await _basePage.ProductListPage.ValidateProductNotExistAsync(data);

            _testOutputHelper.WriteLine($"Completed Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");
        }
    }
}
