using EAappProject.Model;
using Microsoft.Playwright;

namespace EAappProject.Pages;

public class DetailsPage(IPage page)
{
    public ILocator pageTitleTxt => page.Locator("h1", new() { HasText = "Details" });
    public ILocator txtName => page.Locator("#Name");
    public ILocator txtDescription => page.Locator("#Description");
    public ILocator txtPrice => page.Locator("#Price");
    public ILocator txtProductType => page.Locator("#ProductType");
    public ILocator btnBackToList => page.GetByRole(AriaRole.Link, new() { Name = "Back to List" });
    public ILocator btnEdit => page.GetByRole(AriaRole.Link, new() { Name = "Edit" });

    public async Task<EditPage> GoToEditPageAsync()
    {
        await btnEdit.ClickAsync();
        return new EditPage(page);
    }

    public async Task<ProductListPage> BackToListAsync()
    {
        await btnBackToList.ClickAsync();
        return new ProductListPage(page);
    }
}