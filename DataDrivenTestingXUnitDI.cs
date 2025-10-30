using AutoFixture.Xunit2;
using EAappProject.Model;
using EAappProject.Pages;
using Xunit.Abstractions;

namespace EAappProject
{
    public class DataDrivenTestingWithXunitWithDI
    {
        //private readonly IHomePage _homePage;
        //private readonly IProductListPage _productListPage;
        //private readonly ICreateProductPage _createProductPage;
        //private readonly IDeletePage _deletePage;
        //private readonly IEditPage _editPage;
        //private readonly IDetailsPage _detailsPage;
        private readonly IBasePage _basePage;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Random _random = new();

        public DataDrivenTestingWithXunitWithDI(
            //IHomePage homePage,
            //IProductListPage productListPage,
            //IDetailsPage detailsPage,
            //ICreateProductPage createProductPage,
            //IDeletePage deletePage,
            //IEditPage editPage,
            ITestOutputHelper testOutputHelper,
            IBasePage basePage
            )
        {
            //_homePage = homePage;
            //_productListPage = productListPage;
            //_createProductPage = createProductPage;
            //_deletePage = deletePage;
            //_editPage = editPage;
            //_detailsPage = detailsPage;
            _testOutputHelper = testOutputHelper;
            _basePage = basePage;
        }

        [Xunit.Theory]
        [AutoData]
        public async Task CreateEditDeleteProductAsync(ProductDetails data)
        {
            // Assign random product type
            var values = Enum.GetValues(typeof(ProductType));
            data.ProductType = (ProductType)values.GetValue(_random.Next(values.Length));

            // Navigate to product list
            await _basePage.HomePage.ValidateTitleAsync();
            await _basePage.HomePage.ClickProductListAsync();
            await _basePage.ProductListPage.ValidateTitleAsync();

            // Create product
            await _basePage.ProductListPage.CreateProductAsync();
            await _basePage.CreateProductPage.pageTitleTxt.IsVisibleAsync();
            await _basePage.CreateProductPage.CreateProductAsync(data);
            await _basePage.ProductListPage.IsProductExistAsync(data);

            _testOutputHelper.WriteLine($"Created Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");

            // Edit product
            await _basePage.ProductListPage.EditProductAsync(data);
            data.Name = data.UpdatedName ?? data.Name;
            data.Price += 10;
            await _basePage.EditPage.UpdateAsync(data);

            // Delete product
            await _basePage.ProductListPage.DeleteProductAsync(data);
            await _basePage.DeletePage.ValidateTitleAsync();
            await _basePage.DeletePage.DeleteAsync();
            await _basePage.ProductListPage.ValidateProductNotExistAsync(data);

            _testOutputHelper.WriteLine($"Completed Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");
        }
    }
}