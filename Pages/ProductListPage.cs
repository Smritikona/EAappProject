using EAappProject.Model;
using Microsoft.Playwright;

namespace EAappProject.Pages;

public class ProductListPage(IPage page)
{
    ILocator pageTitleTxt => page.GetByRole(AriaRole.Heading, new() { Name = "List" });
    ILocator btnCreate => page.GetByRole(AriaRole.Link, new() { Name = "Create" });
    public ILocator btnDelete(ILocator parentRow) => parentRow.GetByRole(AriaRole.Link, new() { Name = "Delete" });
    ILocator btnDetails(string row) => page.GetByRole(AriaRole.Row, new() { Name = row }).GetByRole(AriaRole.Link, new() { Name = "Details" });

    ILocator btnEdit(string row) => page.GetByRole(AriaRole.Row, new() { Name = row }).GetByRole(AriaRole.Link, new() { Name = "Edit" });


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

    public ILocator GetProductRow(string name, string description, string price, string productType)
    {
        return page.GetByRole(AriaRole.Row, new() { Name = name })
            .Filter(new() { HasText = description })
            .Filter(new() { HasText = price })
            .Filter(new() { HasText = productType });
    }

    public async Task<bool> IsProductExistAsync(ProductDetails product)
    {
        var productRow = GetProductRow(product.Name, product.Description, product.Description, product.ProductType);

        return await productRow.IsVisibleAsync();
    }
    public async Task<DeletePage> DeleteProductAsync(ProductDetails product)
    {
        var productRow = GetProductRow(product.Name, product.Description, product.Description, product.ProductType);

        await btnDelete(productRow).ClickAsync();
        return new DeletePage(page);
    }
    public async Task<EditPage> ClickOnEditProductAsync(ProductDetails product)
    {
        await btnEdit(product.Name).ClickAsync();
        return new EditPage(page);
    }

}
