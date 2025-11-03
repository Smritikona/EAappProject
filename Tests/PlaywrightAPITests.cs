using System.Text.Json;
using AutoFixture.Xunit2;
using EAappProject.Model;
using EAFramework.Driver;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Playwright;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace EAappProject.Tests
{
    public class PlaywrightAPITests : IClassFixture<PlaywrightApiDriver>
    {

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly PlaywrightApiDriver _playwrightDriver;
        private IAPIRequestContext _apiRequest;

        public PlaywrightAPITests(ITestOutputHelper testOutputHelper, PlaywrightApiDriver playwrightDriver)
        {
            _testOutputHelper = testOutputHelper;
            _playwrightDriver = playwrightDriver;

            var headers = new Dictionary<string, string> { { "Authorization", "Bearer 234r324234233" } };

            _apiRequest = _playwrightDriver.InitializePlaywright(headers).GetAwaiter().GetResult();
        }

        [Fact]
        public async Task TestGetProductAsync()
        {
            
            // Call the API to get product
            var response = await _apiRequest.GetAsync("/Product/GetProductById/2");

            var jsonResponse = await response.JsonAsync();

            var product = jsonResponse?.Deserialize<ProductDetails>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            // // Deserialize the response
            // var product = JsonConvert.DeserializeObject<ProductDetails>(jsonResponse.ToString());

            using (new AssertionScope())
            {
                product.Name.Should().Be("Mouse");
                product.ProductType.Should().Be(ProductType.MONITOR);
                product.ProductType.Should().Be(ProductType.PERIPHARALS);
                product.NewPrice.Should().BeEmpty();
            }

            //Better way to deserialize
        }
        
        [Fact]
        public async Task TestGetProductsAsync()
        {
            
            // "id": 1,
            // "name": "Keyboard",
            // "description": "Gaming Keyboard with lights",
            // "price": 150,
            // "productType": 2
            
            // Call the API to get product
            var response = await _apiRequest.GetAsync("/Product/GetProducts");

            var jsonResponse = await response.JsonAsync();
            
            // Deserialize the response
            var products = JsonConvert.DeserializeObject<List<ProductDetails>>(jsonResponse.ToString());

            using (new AssertionScope())
            {
                products.Should().Contain(p => p.Name == "Monitor" && p.Price == 400);
                
                
                
                // products.Should().SatisfyRespectively(
                //     first =>
                //     {
                //         first.Name.Should().Be("Keyboard");
                //         first.Price.Should().Be(150);
                //     },
                //     second =>
                //     {
                //         second.Name.Should().Be("Mouse");
                //         second.Price.Should().Be(40);
                //     });
                //
                // products.Should().BeEquivalentTo(products);
            }

            //Better way to deserialize
        }
        
        [Xunit.Theory]
        [AutoData]
        public async Task TestPostProductsAsync(ProductDetails productBody)
        {
            
            //Create A PRODUCT using POST
            //Validate the PRODUCT created - GET operation of the product
            
            //Step 1
            
            // Call the API to POST product
            var response = await _apiRequest.PostAsync("/Product/Create", new APIRequestContextOptions
            {
                DataObject = productBody,
            });

            response.Status.Should().Be(200);
            _testOutputHelper.WriteLine(response.StatusText);


            //Step 2
            var responseFromGet = await _apiRequest.GetAsync("/Product/GetProducts");

            var jsonResponse = await responseFromGet.JsonAsync();
            
            // Deserialize the response
            var products = JsonConvert.DeserializeObject<List<ProductDetails>>(jsonResponse.ToString());
            
            // products.Should().Contain(p => p.Name == productBody.Name 
            //                                && p.Price == productBody.Price 
            //                                && p.Description == productBody.Description);

            //products.Should().Contain(productBody);


        }
        
        // [Xunit.Theory]
        // [AutoData]
        // public async Task TestDeleteProductsAsync(ProductDetails productBody)
        // {
        //     
        //     //Create A PRODUCT using POST
        //     //Delete the Product created
        //     //Validate the PRODUCT if its deleted
        //     
        //     //Step 1
        //     // Call the API to POST product
        //     var response = await _apiRequest.PostAsync("/Product/Create", new APIRequestContextOptions
        //     {
        //         DataObject = productBody,
        //     });
        //
        //     response.Status.Should().Be(200);
        //     _testOutputHelper.WriteLine(response.StatusText);
        //     
        //     var id = response.id //ToDo ???
        //
        //     //Step 2
        //     await _apiRequest.DeleteAsync($"/Product/Delete?id={id}");
        //     
        //     
        //     //Step 2
        //     var responseFromGet = await _apiRequest.GetAsync($"/Product/GetProductById/{id}");
        //
        //     var jsonResponse = await responseFromGet.JsonAsync();
        //     
        //     // Deserialize the response
        //     var products = JsonConvert.DeserializeObject<ProductDetails>(jsonResponse.ToString());
        //
        //     products.Name.Should().BeNullOrEmpty();
        //
        // }

    }
}
