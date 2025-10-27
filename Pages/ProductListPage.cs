using EAappProject.Model;
using Microsoft.Playwright;

namespace EAappProject.Pages;

public class ProductListPage(IPage page)
{
    ILocator pageTitleTxt => page.GetByRole(AriaRole.Heading, new() { Name = "List" });
    ILocator btnCreate => page.GetByRole(AriaRole.Link, new() { Name = "Create" });
    public ILocator btnDelete(ILocator parentRow) => parentRow.GetByRole(AriaRole.Link, new() { Name = "Delete" });
    public ILocator btnEdit(ILocator parentRow) => parentRow.GetByRole(AriaRole.Link, new() { Name = "Edit" });
    public ILocator btnDetails(ILocator parentRow) => parentRow.GetByRole(AriaRole.Link, new() { Name = "Details" });



    public async Task<ProductListPage> ValidateTitleAsync()
    {
        await Assertions.Expect(pageTitleTxt).ToBeVisibleAsync();
        return new ProductListPage(page);
    }
    public async Task<CreateProductPage> CreateProductAsync()
    {
        await btnCreate.ClickAsync();
        return new CreateProductPage(page);
    }

    public ILocator GetProductRow(string name, string description, string price, ProductType productType)
    {
        return page.GetByRole(AriaRole.Row, new() { Name = name })
            .Filter(new() { HasText = description })
            .Filter(new() { HasText = price })
            .Filter(new() { HasText = productType.ToString() });
    }

    public async Task<bool> IsProductExistAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.Price.ToString(), productDetails.ProductType);

        return await productRow.IsVisibleAsync();
    }
  
    public async Task<EditPage> EditProductAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.Price.ToString(), productDetails.ProductType);

        await btnEdit(productRow).ClickAsync();
        return new EditPage(page);
    }

    public async Task<bool> isModifiedProductExistAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.NewPrice, productDetails.ProductType);
        return await productRow.IsVisibleAsync();
    }

    public async Task<DeletePage> DeleteProductAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.Price.ToString(), ProductType.CPU);

        await btnDelete(productRow).ClickAsync();
        return new DeletePage(page);
    }

    public async Task<ProductListPage> ValidateProductNotExistAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.Price.ToString(), productDetails.ProductType);
        await Assertions.Expect(productRow).Not.ToBeVisibleAsync();

        return new ProductListPage(page);
    }

    public async Task<DeletePage> DeleteModifiedProductAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.NewPrice, productDetails.ProductType);

        await btnDelete(productRow).ClickAsync();
        return new DeletePage(page);
    }

    public async Task<ProductListPage> ValidateModifiedProductNotExistAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.NewPrice, productDetails.ProductType);
        await Assertions.Expect(productRow).Not.ToBeVisibleAsync();

        return new ProductListPage(page);
    }

    public async Task<DetailsPage> DetailsProductAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.Price.ToString(), productDetails.ProductType);

        await btnDetails(productRow).ClickAsync();
        return new DetailsPage(page);
    }


}
