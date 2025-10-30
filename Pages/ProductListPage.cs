using EAappProject.Driver;
using EAappProject.Model;
using Microsoft.Playwright;

namespace EAappProject.Pages;

public interface IProductListPage
{
    ILocator btnDelete(ILocator parentRow);
    ILocator btnDetails(ILocator parentRow);
    ILocator btnEdit(ILocator parentRow);
    Task CreateProductAsync();
    Task DeleteModifiedProductAsync(ProductDetails productDetails);
    Task DeleteProductAsync(ProductDetails productDetails);
    Task DetailsProductAsync(ProductDetails productDetails);
    Task EditProductAsync(ProductDetails productDetails);
    ILocator GetProductRow(string name, string description, string price, ProductType productType);
    Task<bool> isModifiedProductExistAsync(ProductDetails productDetails);
    Task<bool> IsProductExistAsync(ProductDetails productDetails);
    Task ValidateModifiedProductNotExistAsync(ProductDetails productDetails);
    Task ValidateProductNotExistAsync(ProductDetails productDetails);
    Task ValidateTitleAsync();
}

public class ProductListPage : IProductListPage
{
    private IPage _page;

    public ProductListPage(IPage page)
    {
        _page = page;
    }
    ILocator pageTitleTxt => _page.GetByRole(AriaRole.Heading, new() { Name = "List" });
    ILocator btnCreate => _page.GetByRole(AriaRole.Link, new() { Name = "Create" });
    public ILocator btnDelete(ILocator parentRow) => parentRow.GetByRole(AriaRole.Link, new() { Name = "Delete" });
    public ILocator btnEdit(ILocator parentRow) => parentRow.GetByRole(AriaRole.Link, new() { Name = "Edit" });
    public ILocator btnDetails(ILocator parentRow) => parentRow.GetByRole(AriaRole.Link, new() { Name = "Details" });



    public async Task ValidateTitleAsync()
    {
        await Assertions.Expect(pageTitleTxt).ToBeVisibleAsync();
    }
    public async Task CreateProductAsync()
    {
        await btnCreate.ClickAsync();
    }

    public ILocator GetProductRow(string name, string description, string price, ProductType productType)
    {
        return _page.GetByRole(AriaRole.Row, new() { Name = name })
            .Filter(new() { HasText = description })
            .Filter(new() { HasText = price })
            .Filter(new() { HasText = productType.ToString() });
    }

    public async Task<bool> IsProductExistAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.Price.ToString(), productDetails.ProductType);

        return await productRow.IsVisibleAsync();
    }

    public async Task EditProductAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.Price.ToString(), productDetails.ProductType);

        await btnEdit(productRow).ClickAsync();
    }

    public async Task<bool> isModifiedProductExistAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.NewPrice, productDetails.ProductType);
        return await productRow.IsVisibleAsync();
    }

    public async Task DeleteProductAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.Price.ToString(), ProductType.CPU);

        await btnDelete(productRow).ClickAsync();
    }

    public async Task ValidateProductNotExistAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.Price.ToString(), productDetails.ProductType);
        await Assertions.Expect(productRow).Not.ToBeVisibleAsync();
    }

    public async Task DeleteModifiedProductAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.NewPrice, productDetails.ProductType);

        await btnDelete(productRow).ClickAsync();
    }

    public async Task ValidateModifiedProductNotExistAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.NewPrice, productDetails.ProductType);
        await Assertions.Expect(productRow).Not.ToBeVisibleAsync();
    }

    public async Task DetailsProductAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.Price.ToString(), productDetails.ProductType);

        await btnDetails(productRow).ClickAsync();
    }


}
