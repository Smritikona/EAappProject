using EAappProject.Model;
using Microsoft.Playwright;

namespace EAappProject.Pages;

public class ProductListPage(IPage page)
{
    ILocator pageTitleTxt => page.GetByRole(AriaRole.Heading, new() { Name = "List" });
    ILocator btnCreate => page.GetByRole(AriaRole.Link, new() { Name = "Create" });
    public ILocator btnDelete(ILocator parentRow) => parentRow.GetByRole(AriaRole.Link, new() { Name = "Delete" });
    public ILocator btnEdit(ILocator parentRow) => parentRow.GetByRole(AriaRole.Link, new() { Name = "Edit" });


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

    public async Task<bool> IsProductExistAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.Price, productDetails.ProductType);

        return await productRow.IsVisibleAsync();
    }
    public async Task<DeletePage> DeleteProductAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.Price, productDetails.ProductType);

        await btnDelete(productRow).ClickAsync();
        return new DeletePage(page);
    }

    public async Task<EditProductPage> ClickEditLinkAsync(ProductDetails productDetails)
    {
        var productRow = GetProductRow(productDetails.Name, productDetails.Description, productDetails.Price, productDetails.ProductType);

        await btnEdit(productRow).ClickAsync();
        return new EditProductPage(page);
    }


}
