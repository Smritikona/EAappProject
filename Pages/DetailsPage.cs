using Microsoft.Playwright;

namespace EAappProject.Pages;

public interface IDetailsPage
{
    ILocator btnBackToList { get; }
    ILocator btnEdit { get; }
    ILocator pageTitleTxt { get; }
    ILocator txtDescription { get; }
    ILocator txtName { get; }
    ILocator txtPrice { get; }
    ILocator txtProductType { get; }
    Task BackToListAsync();
    Task GoToEditPageAsync();
}

public class DetailsPage(IPage page) : IDetailsPage
{
    private IPage _page = page;
    public ILocator pageTitleTxt => _page.Locator("h1", new() { HasText = "Details" });
    public ILocator txtName => _page.Locator("#Name");
    public ILocator txtDescription => _page.Locator("#Description");
    public ILocator txtPrice => _page.Locator("#Price");
    public ILocator txtProductType => _page.Locator("#ProductType");
    public ILocator btnBackToList => _page.GetByRole(AriaRole.Link, new() { Name = "Back to List" });
    public ILocator btnEdit => _page.GetByRole(AriaRole.Link, new() { Name = "Edit" });

    public async Task GoToEditPageAsync() => await btnEdit.ClickAsync();

    public async Task BackToListAsync() => await btnBackToList.ClickAsync();
}