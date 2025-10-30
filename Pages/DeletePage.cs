using EAappProject.Driver;
using Microsoft.Playwright;

namespace EAappProject.Pages;

public interface IDeletePage
{
    Task DeleteAsync();
    Task ValidateTitleAsync();
}

public class DeletePage : IDeletePage
{
    public DeletePage(IPage page)
    {
        _page = page;
    }
    private IPage _page;
    ILocator pageTitleTxt => _page.Locator("h1", new() { HasText = "Delete" });
    ILocator btnDelete => _page.GetByRole(AriaRole.Button, new() { Name = "Delete" });

    public async Task ValidateTitleAsync()
    {
        await Assertions.Expect(pageTitleTxt).ToBeVisibleAsync();
    }

    public async Task DeleteAsync()
    {
        await btnDelete.ClickAsync();
    }
}