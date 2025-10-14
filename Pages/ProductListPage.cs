using Microsoft.Playwright;

namespace EAappProject.Pages;

public class ProductListPage(IPage page)
{
    ILocator pageTitleTxt => page.GetByRole(AriaRole.Heading, new() { Name = "List" });
    ILocator btnCreate => page.GetByRole(AriaRole.Link, new() { Name = "Create" });
    public ILocator btnDelete(ILocator parentRow) => parentRow.GetByRole(AriaRole.Link, new() { Name = "Delete" });


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

    public async Task<bool> IsProductExistAsync(string name, string description, string price, string productType)
    {
        var productRow = page.GetByRole(AriaRole.Row, new() { Name = name })
            .Filter(new() { HasText = description})
            .Filter(new() { HasText = price})
            .Filter(new() { HasText = productType});

        return await productRow.IsVisibleAsync();
    }
    public async Task<DeletePage> DeleteProductAsync(string name, string description, string price, string productType)
    {
        var productRow = page.GetByRole(AriaRole.Row, new() { Name = name })
            .Filter(new() { HasText = description })
            .Filter(new() { HasText = price })
            .Filter(new() { HasText = productType });

        await btnDelete(productRow).ClickAsync();
        return new DeletePage(page);
    }


}
