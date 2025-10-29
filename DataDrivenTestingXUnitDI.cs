using AutoFixture.Xunit2;
using EAappProject.Model;
using EAappProject.Pages;
using Microsoft.Playwright;
using System;
using Xunit.Abstractions;

namespace EAappProject
{
    public class DataDrivenTestingWithXunitWithDI
    {
        private readonly IHomePage _homePage;
        private readonly IProductListPage _productListPage;
        private readonly ICreateProductPage _createProductPage;
        private readonly IDeletePage _deletePage;
        private readonly IEditPage _editPage;
        private readonly IDetailsPage _detailsPage;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Random _random = new();

        public DataDrivenTestingWithXunitWithDI(
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
        
        [Xunit.Theory]
        [AutoData]
        public async Task CreateEditDeleteProductAsync(ProductDetails data)
        {
            // Assign random product type
            var values = Enum.GetValues(typeof(ProductType));
            data.ProductType = (ProductType)values.GetValue(_random.Next(values.Length));

            // Navigate to product list
            await _homePage.ValidateTitleAsync();
            await _homePage.ClickProductListAsync();
            await _productListPage.ValidateTitleAsync();

            // Create product
            await _productListPage.CreateProductAsync();
            await _createProductPage.pageTitleTxt.IsVisibleAsync();
            await _createProductPage.CreateProductAsync(data);
            await _productListPage.IsProductExistAsync(data);

            _testOutputHelper.WriteLine($"Created Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");

            // Edit product
            await _productListPage.EditProductAsync(data);
            data.Name = data.UpdatedName ?? data.Name;
            data.Price += 10;
            await _editPage.UpdateAsync(data);

            // Delete product
            await _productListPage.DeleteProductAsync(data);
            await _deletePage.ValidateTitleAsync();
            await _deletePage.DeleteAsync();
            await _productListPage.ValidateProductNotExistAsync(data);

            _testOutputHelper.WriteLine($"Completed Product: {data.Name}, {data.Description}, {data.Price}, {data.ProductType}");
        }
    }
}