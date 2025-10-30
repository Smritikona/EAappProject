using EAappProject.Pages;
using Microsoft.Playwright;

namespace EAappProject.Base;

public interface IBasePage
{
    ICreateProductPage CreateProductPage { get; }
    IDeletePage DeletePage { get; }
    IDetailsPage DetailsPage { get; }
    IEditPage EditPage { get; }
    IHomePage HomePage { get; }
    IProductListPage ProductListPage { get; }
}

public class BasePage : IBasePage
{
    private Lazy<IHomePage> _homePage;
    private Lazy<IProductListPage> _productListPage;
    private Lazy<ICreateProductPage> _createProductPage;
    private Lazy<IEditPage> _editPage;
    private Lazy<IDeletePage> _deletePage;
    private Lazy<IDetailsPage> _detailsPage;

    public BasePage(IPage page)
    {
        _homePage = new Lazy<IHomePage>(GetHomePage(page));
        _productListPage = new Lazy<IProductListPage>(() => new ProductListPage(page));
        _createProductPage = new Lazy<ICreateProductPage>(() => new CreateProductPage(page));
        _editPage = new Lazy<IEditPage>(() => new EditPage(page));
        _deletePage = new Lazy<IDeletePage>(() => new DeletePage(page));
        _detailsPage = new Lazy<IDetailsPage>(() => new DetailsPage(page));

    }

    public HomePage GetHomePage(IPage page)
    {
        return new HomePage(page);
    }

    public IHomePage HomePage => _homePage.Value;
    public IProductListPage ProductListPage => _productListPage.Value;
    public ICreateProductPage CreateProductPage => _createProductPage.Value;
    public IEditPage EditPage => _editPage.Value;
    public IDeletePage DeletePage => _deletePage.Value;
    public IDetailsPage DetailsPage => _detailsPage.Value;
}