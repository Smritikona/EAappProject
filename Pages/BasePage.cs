using EAappProject.Pages.Interfaces;
using Microsoft.Playwright;

namespace EAappProject.Pages;

public interface IBasePage
{
    IHomePage HomePage { get; }
    IProductListPage ProductListPage { get; }
    ICreateProductPage CreateProductPage { get; }
}

public class BasePage : IBasePage
{
    private Lazy<IHomePage> _homePage;
    private Lazy<IProductListPage> _productListPage;
    private Lazy<ICreateProductPage> _createProductPage { get; }

    public BasePage(IPage page)
    {
        _homePage = new Lazy<IHomePage>(GetHomePage(page));
        _productListPage = new Lazy<IProductListPage>(() => new ProductListPage(page));
        _createProductPage = new Lazy<ICreateProductPage>(() => new CreateProductPage(page));
    }
    
    public HomePage GetHomePage(IPage page)
    {
        return new HomePage(page);
    }

    public IHomePage HomePage => _homePage.Value;
    public IProductListPage ProductListPage => _productListPage.Value;
    public ICreateProductPage CreateProductPage => _createProductPage.Value;
}