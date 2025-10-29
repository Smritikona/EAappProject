using EAappProject.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAappProject.Base
{
    public class BasePages
    {
        public IHomePage HomePage { get; }
        public IProductListPage ProductListPage { get; }
        public ICreateProductPage CreateProductPage { get; }
        public IEditPage EditPage { get; }
        public IDeletePage DeletePage { get; }
        public IDetailsPage DetailsPage { get; }

        public BasePages(
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
