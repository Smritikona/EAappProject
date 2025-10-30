using EAappProject.Pages;

namespace EAappProject.Base
{
    public interface IBasePageOld
    {
        IHomePage HomePage { get; }
        IProductListPage ProductListPage { get; }
        ICreateProductPage CreateProductPage { get; }
        IDeletePage DeletePage { get; }
        IDetailsPage DetailsPage { get; }
        IEditPage EditPage { get; }
    }

    public class BasePageOld : IBasePageOld
    {
        public IHomePage HomePage { get; }
        public IProductListPage ProductListPage { get; }
        public ICreateProductPage CreateProductPage { get; }
        public IEditPage EditPage { get; }
        public IDeletePage DeletePage { get; }
        public IDetailsPage DetailsPage { get; }

        public BasePageOld(
            IHomePage homePage,
            IProductListPage productListPage,
            ICreateProductPage createProductPage,
            IEditPage editPage,
            IDeletePage deletePage,
            IDetailsPage detailsPage)
        {
            HomePage = homePage;
            ProductListPage = productListPage;
            CreateProductPage = createProductPage;
            EditPage = editPage;
            DeletePage = deletePage;
            DetailsPage = detailsPage;
        }
    }

}
