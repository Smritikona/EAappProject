using AutoFixture.Xunit2;
using EAappProject.Model;
using EAappProject.Pages;
using EAFramework.Utilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;

namespace EAappProject.Tests;

public class DatabaseTesting
{
    private readonly IBasePage _basePage;
    private IPage _page;

    public DatabaseTesting(IBasePage basePage)
    {
        _basePage = basePage;
    }

    [Xunit.Theory]
    [AutoData]
    public async Task CreateDeleteProductWithAutoFixtureAsync(ProductDetails data)
    {
        ProductDbContext productDbContext = new ProductDbContext();
        var productWithEF = productDbContext.Products.Where(x => x.Name == "Cabinet");
        
        //product.Name
        var productbyIdWithEF = productDbContext.Products.Where(x => x.Id == 11);

        await _basePage.HomePage.ClickProductListAsync();
        await _basePage.ProductListPage.ValidateTitleAsync();
            
        await _basePage.ProductListPage.CreateProductAsync();
        await _basePage.CreateProductPage.ValidateTitleAsync();
        await _basePage.CreateProductPage.CreateProductAsync(data);
        
        //Verify in the App DB using EF
        var result = productDbContext.Products.FirstOrDefault(x => x.Name == data.Name);

        result.Should().BeEquivalentTo(data, x=> 
            x
                .Excluding(y => y.NewPrice)
                .Excluding(y => y.ProductType));

        var product = productDbContext.Products.FirstOrDefault(x => x.Name == data.Name);
        productDbContext.Remove(product);
        productDbContext.SaveChanges();
    }
    
    
}