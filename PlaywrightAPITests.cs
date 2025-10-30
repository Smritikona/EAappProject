using System.Text.Json;
using AutoFixture.Xunit2;
using EAappProject.Driver;
using EAappProject.Model;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Playwright;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;
using Xunit.Abstractions;

namespace EAappProject
{
    public class PlaywrightAPITests : IClassFixture<PlaywrightAPIDriver>
    {

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly PlaywrightAPIDriver _playwrightAPIDriver;
        private IAPIRequestContext _apiRequest;


        public PlaywrightAPITests(ITestOutputHelper testOutputHelper, PlaywrightAPIDriver playwrightAPIDriver)
        {
            _testOutputHelper = testOutputHelper;
            _playwrightAPIDriver = playwrightAPIDriver;
            var headers = new Dictionary<string, string>
                {
                    { "Accept", "application/json" },
                    { "Content-Type", "application/json" }
                };
            _apiRequest = _playwrightAPIDriver.InitializeAsync(headers).GetAwaiter().GetResult();
        }


        [Fact]
        public async Task TestGetProductAsync()
        {
            //Call the API to get products
            
            // Call the API to get product
            var response = await _apiRequest.GetAsync("/Product/GetProductById/2");

            var jsonResponse = await response.JsonAsync();
            _testOutputHelper.WriteLine("Response JSON:\n" + jsonResponse);


            //Deserialize the response
            //var product = JsonConvert.DeserializeObject<ProductDetails>(jsonResponse.ToString());

            var product = jsonResponse?.Deserialize<ProductDetails>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            _testOutputHelper.WriteLine($"Product: {product.Name}, {product.Description}, {product.Price}");

            //Validate the response
            Xunit.Assert.Equal("Mouse", product.Name.Trim(), ignoreCase: true);

            using (new AssertionScope())
            {
                product.Name.Should().Be("Mouse");
                //product.ProductType.Should().Be(ProductType.MONITOR);
                product.ProductType.Should().Be(ProductType.EXTERNAL);
                product.UpdatedName.Should().BeNullOrEmpty();
            }

            //Better way to deserialize
        }
        
        [Fact]
        public async Task TestGetProductsAsync()
        {
            //Call the API to get products
            
            // Call the API to get product
            var response = await _apiRequest.GetAsync("/Product/GetProducts");

            var jsonResponse = await response.JsonAsync();
            _testOutputHelper.WriteLine("Response JSON:\n" + jsonResponse);

            
            //Deserialize the response
            var products = JsonConvert.DeserializeObject<List<ProductDetails>>(jsonResponse.ToString());
            foreach (var product in products)
                _testOutputHelper.WriteLine($"Product: {product.Name}, {product.Description}, {product.Price}, {product.ProductType}");


            //Fluent Assertions
            products.Should().NotBeNull();
            using (new AssertionScope())
            {
                products.Should().Contain(p => p.Name == "Monitor" && p.Price == 400);
                
                products.Should().SatisfyRespectively(
                    first =>
                    {
                        first.Name.Should().Be("Keyboard");
                        first.Price.Should().Be(150);
                    },
                    second =>
                    {
                        second.Name.Should().Be("Mouse");
                        second.Price.Should().Be(40);
                    },
                    third =>
                    {
                        third.Name.Should().Be("Monitor");
                        third.Price.Should().Be(400);
                    },
                    fourth =>
                    {
                        fourth.Name.Should().Be("Cabinet");
                        fourth.Price.Should().Be(65);
                    });
                
                products.Should().BeEquivalentTo(products);
            }
                

            }

        [Fact]
        public async Task TestPostProductAsync()
        {
            //Body
            var body = new ProductDetails
            {
                Name = "Wireless HeadSet",
                Description = "A high-precision wireless headset with ergonomic design.",
                Price = 2967,
                ProductType = ProductType.MONITOR
            };
            body.UpdatedName = null;

            //Step 1
            //Call the API to get products
            var response = await _apiRequest.PostAsync("/Product/Create", new APIRequestContextOptions
            {
                DataObject = body
            });
            response.Status.Should().Be(200);
            _testOutputHelper.WriteLine(response.StatusText);
            var jsonResponse = await response.JsonAsync();
            _testOutputHelper.WriteLine("Response of newly Created JSON:\n" + jsonResponse);

            //Deserialize the response
            var product = JsonConvert.DeserializeObject<ProductDetails>(jsonResponse.ToString());
            _testOutputHelper.WriteLine($"Product: {product.Name}, {product.Description}, {product.Price}");

            //Validate the response
            product.Should().NotBeNull();

            //Step 2
            var responseFromGet = await _apiRequest.GetAsync("/Product/GetProducts");
            var jsonGetResponse = await responseFromGet.JsonAsync();

            // Deserialize the response
            var products = JsonConvert.DeserializeObject<List<ProductDetails>>(jsonGetResponse.ToString());
            products.Should().Contain(p => p.Name == body.Name
                                           && p.Price == body.Price
                                           && p.Description == body.Description);

            products.Should().ContainEquivalentOf(body);

            //Step 3
            //Delete the product
            int id = JsonDocument
                        .Parse(jsonResponse.ToString())
                        .RootElement.GetProperty("id")
                        .GetInt32();
            _testOutputHelper.WriteLine($"id: {id}");
            await _apiRequest.DeleteAsync($"/Product/Delete?id={id}");

            //Step 4
            var responseAfterDel = await _apiRequest.GetAsync($"/Product/GetProductById/{id}");
            responseAfterDel.Status.Should().Be(204);
            _testOutputHelper.WriteLine(responseAfterDel.StatusText);
            var responseBodyAfterDel = await responseAfterDel.TextAsync();
            _testOutputHelper.WriteLine($"Response body after delete: '{responseBodyAfterDel}'");
            responseBodyAfterDel.Should().BeNullOrEmpty();
        }
        
        [Xunit.Theory]
        [AutoData]
        public async Task TestPostProductWithAutoFixtureAsync(ProductDetails body)
        {
            body.UpdatedName = null;
            
            //Step 1
            var response = await _apiRequest.PostAsync("/Product/Create", new APIRequestContextOptions
            {
                DataObject = body,
            });
            response.Status.Should().Be(200);
            _testOutputHelper.WriteLine(response.StatusText);
            var jsonResponse = await response.JsonAsync();
            _testOutputHelper.WriteLine("Response of newly Created JSON:\n" + jsonResponse);
            
            //Deserialize the response
            var product = JsonConvert.DeserializeObject<ProductDetails>(jsonResponse.ToString());
            _testOutputHelper.WriteLine($"Product: {product.Name}, {product.Description}, {product.Price}");

            //Validate the response
            product.Should().BeEquivalentTo(body);

            //Step 2
            var responseFromGet = await _apiRequest.GetAsync("/Product/GetProducts");
            var jsonGetResponse = await responseFromGet.JsonAsync();

            //Deserialize the response
            var products = JsonConvert.DeserializeObject<List<ProductDetails>>(jsonGetResponse.ToString());
            products.Should().Contain(p => p.Name == body.Name
                                           && p.Price == body.Price
                                           && p.Description == body.Description);
            products.Should().ContainEquivalentOf(body);

            //Step 3
            //Delete the product
            int id = JsonDocument
                        .Parse(jsonResponse.ToString())
                        .RootElement.GetProperty("id")
                        .GetInt32();
            _testOutputHelper.WriteLine($"id: {id}");
            await _apiRequest.DeleteAsync($"/Product/Delete?id={id}");

            //Step 4
            var responseAfterDel = await _apiRequest.GetAsync($"/Product/GetProductById/{id}");
            responseAfterDel.Status.Should().Be(204);
            _testOutputHelper.WriteLine(responseAfterDel.StatusText);
            var responseBodyAfterDel = await responseAfterDel.TextAsync();
            _testOutputHelper.WriteLine($"Response body after delete: '{responseBodyAfterDel}'");
            responseBodyAfterDel.Should().BeNullOrEmpty();
        }

        [Xunit.Theory]
        [AutoData]
        public async Task TestDeleteProductWithAutoFixtureAsync(ProductDetails productBody)
        {
            productBody.UpdatedName = null;

            //Step 1
            //Call the API to POST product
            var response = await _apiRequest.PostAsync("/Product/Create", new APIRequestContextOptions
            {
                DataObject = productBody,
            });

            response.Status.Should().Be(200);
            _testOutputHelper.WriteLine(response.StatusText);
            var jsonResponse = await response.JsonAsync();
            _testOutputHelper.WriteLine("Response JSON:\n" + jsonResponse);

            //Deserialize the response
            dynamic product = JsonConvert.DeserializeObject(jsonResponse.ToString());
            int id = product.id;
            _testOutputHelper.WriteLine($"id: {id}");

            //Step 2
            var responseFromGet = await _apiRequest.GetAsync($"/Product/GetProductById/{id}");
            var jsonResponseFromGet = await responseFromGet.JsonAsync();
            
            //Deserialize the response
            dynamic productFromGet = JsonConvert.DeserializeObject(jsonResponseFromGet.ToString());
            responseFromGet.Should().NotBeNull();
            _testOutputHelper.WriteLine($"{productFromGet.name}, {productFromGet.id}");

            //Step 3
            //API call for DELETE Product 
            await _apiRequest.DeleteAsync($"/Product/Delete?id={id}");

            //Step 4
            var responseAfterDel = await _apiRequest.GetAsync($"/Product/GetProductById/{id}");
            responseAfterDel.Status.Should().Be(204);
            _testOutputHelper.WriteLine(responseAfterDel.StatusText);
            var responseBodyAfterDel = await responseAfterDel.TextAsync();
            _testOutputHelper.WriteLine($"Response body after delete: '{responseBodyAfterDel}'");
            responseBodyAfterDel.Should().BeNullOrEmpty();

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
