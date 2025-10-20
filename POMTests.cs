using EAappProject.Model;
using EAappProject.Pages;
using EAappProject.Utilities;
using Microsoft.Playwright;
using NUnit;

namespace EAappProject
{
    public class POMTests
    {

        [Test]
        public async Task CreateDeleteProductAsync()
        {
            var data = JsonHelper.ReadJsonFile();

            HomePage homePage = new HomePage(_page);
            await homePage.ValidateTitleAsync();

            var productList = await homePage.ClickProductListAsync();
            await productList.ValidateTitleAsync();

            var createProduct = await productList.CreateProductAsync();
            await createProduct.pageTitleTxt.IsVisibleAsync();
            await createProduct.CreateProductAsync(data);
            await productList.IsProductExistAsync(data);

            var deleteProduct = await productList.DeleteProductAsync(data);
            await deleteProduct.ValidateTitleAsync();
            await deleteProduct.DeleteAsync();
            await productList.ValidateProductNotExistAsync(data);
        }

        [Test]
        public async Task CreateModifyDeleteProductAsync()
        {
            var data = JsonHelper.ReadJsonFile();

            HomePage homePage = new HomePage(_page);
            await homePage.ValidateTitleAsync();

            var productList = await homePage.ClickProductListAsync();
            await productList.ValidateTitleAsync();

            var createProduct = await productList.CreateProductAsync();
            await createProduct.pageTitleTxt.IsVisibleAsync();

            await createProduct.CreateProductAsync(data);
            await productList.IsProductExistAsync(data);

            
            var editProduct = await productList.EditProductAsync(data);

            await Assertions.Expect(editProduct.pageTitleTxt).ToBeVisibleAsync();

            await Assertions.Expect(editProduct.txtName).ToHaveValueAsync(data.Name);
            await Assertions.Expect(editProduct.txtDescription).ToHaveValueAsync(data.Description);
            await Assertions.Expect(editProduct.txtPrice).ToHaveValueAsync(data.Price.ToString());
            await Assertions.Expect(editProduct.txtProductType).ToHaveValueAsync(data.ProductType);

            data.Price = 3000;
            data.Description = "This is a modified description";

            await editProduct.UpdateAsync(data);
            await productList.IsProductExistAsync(data);

            var deleteProduct = await productList.DeleteProductAsync(data);
            await deleteProduct.ValidateTitleAsync();
            await deleteProduct.DeleteAsync();
            await productList.ValidateProductNotExistAsync(data);
        }

        [Test]
        public async Task CreateDetailsDeleteProductAsync()
        {
            var data = JsonHelper.ReadJsonFile();

            HomePage homePage = new HomePage(_page);
            await homePage.ValidateTitleAsync();

            var productList = await homePage.ClickProductListAsync();
            await productList.ValidateTitleAsync();

            var createProduct = await productList.CreateProductAsync();
            await createProduct.pageTitleTxt.IsVisibleAsync();

            await createProduct.CreateProductAsync(data);
            await productList.IsProductExistAsync(data);

            var detailsProduct = await productList.DetailsProductAsync(data);
            await Assertions.Expect(detailsProduct.pageTitleTxt).ToBeVisibleAsync();

            await Assertions.Expect(detailsProduct.txtName).ToHaveTextAsync(data.Name);
            await Assertions.Expect(detailsProduct.txtPrice).ToHaveTextAsync(data.Price.ToString());
            await Assertions.Expect(detailsProduct.txtDescription).ToHaveTextAsync(data.Description);
            await Assertions.Expect(detailsProduct.txtProductType).ToHaveTextAsync(data.ProductType);

            await detailsProduct.BackToListAsync();

            var deleteProduct = await productList.DeleteProductAsync(data);
            await deleteProduct.ValidateTitleAsync();
            await deleteProduct.DeleteAsync();
            await productList.ValidateProductNotExistAsync(data);
        }

        [Test]
        public async Task CreateDetailsEditDeleteProductAsync()
        {
            var data = JsonHelper.ReadJsonFile();

            HomePage homePage = new HomePage(_page);
            await homePage.ValidateTitleAsync();

            var productList = await homePage.ClickProductListAsync();
            await productList.ValidateTitleAsync();

            var createProduct = await productList.CreateProductAsync();
            await createProduct.pageTitleTxt.IsVisibleAsync();

            await createProduct.CreateProductAsync(data);
            await productList.IsProductExistAsync(data);

            var detailsProduct = await productList.DetailsProductAsync(data);
            await Assertions.Expect(detailsProduct.pageTitleTxt).ToBeVisibleAsync();

            await Assertions.Expect(detailsProduct.txtName).ToHaveTextAsync(data.Name);
            await Assertions.Expect(detailsProduct.txtPrice).ToHaveTextAsync(data.Price.ToString());
            await Assertions.Expect(detailsProduct.txtDescription).ToHaveTextAsync(data.Description);
            await Assertions.Expect(detailsProduct.txtProductType).ToHaveTextAsync(data.ProductType);

            var editProduct = await detailsProduct.GoToEditPageAsync();
            await Assertions.Expect(editProduct.pageTitleTxt).ToBeVisibleAsync();

            await Assertions.Expect(editProduct.txtName).ToHaveValueAsync(data.Name);
            await Assertions.Expect(editProduct.txtDescription).ToHaveValueAsync(data.Description);
            await Assertions.Expect(editProduct.txtPrice).ToHaveValueAsync(data.Price.ToString());
            await Assertions.Expect(editProduct.txtProductType).ToHaveValueAsync(data.ProductType);

            await editProduct.UpdateAsync(data);
            await productList.IsProductExistAsync(data);

            var deleteProduct = await productList.DeleteProductAsync(data);
            await deleteProduct.ValidateTitleAsync();
            await deleteProduct.DeleteAsync();
            await productList.ValidateProductNotExistAsync(data);
        }
    }
}
