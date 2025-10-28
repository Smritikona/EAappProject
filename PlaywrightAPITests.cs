using System.Text.Json;
using AutoFixture.Xunit2;
using EAappProject.Driver;
using EAappProject.Model;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Playwright;
using Newtonsoft.Json;
using System.Text.Json;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions.Execution;
using AutoFixture.Xunit2;
using Newtonsoft.Json.Linq;

namespace EAappProject
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

            // Call the API to get product
            var response = await _apiRequest.GetAsync("/Product/GetProducts");

            var jsonResponse = await response.JsonAsync();

            // Deserialize the response
            var products = JsonConvert.DeserializeObject<List<ProductDetails>>(jsonResponse.ToString());

            // Validate the response
            // Xunit.Assert.Equal(product.Name, "Mouse");


            //Fluent Assertions
            //Better way to deserialize
            using (new AssertionScope())
            {
                products.Should().NotBeNull();
                products.Should().NotBeEmpty();
                products.Should().HaveCountGreaterThanOrEqualTo(1);
                products.Should().Contain(p => p.Name == "Mouse" && p.Price == 40);
                products.Should().OnlyContain(p => p.Price > 100);
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
                    }
                );
            }
        }

        [Fact]
        public async Task TestCreateProductAsync()
        {
            //body
            var payload = new ProductDetails
            {
                Name = "Mouse12",
                Description = "Wireless Mouse12",
                Price = 70,
                ProductType = ProductType.EXTERNAL
            };
            // Call the API to get product
            var response = await _apiRequest.PostAsync("/Product/Create", new APIRequestContextOptions { DataObject = payload });

            var jsonResponse = await response.JsonAsync();

            response.Status.Should().Be(200);
            _testOutputHelper.WriteLine("Response from Create Product API: " + jsonResponse.ToString());
            response.Ok.Should().BeTrue();

            var responsefromGet = await _apiRequest.GetAsync("/Product/GetProducts");
            var jsonResponseforGet = await responsefromGet.JsonAsync();
            var products = jsonResponseforGet?.Deserialize<List<ProductDetails>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            using (new AssertionScope())
            {
                products.Should().Contain(p => p.Name == payload.Name);
                products.Should().Contain(p => p.Price == payload.Price);
                products.Should().Contain(p => p.Description == payload.Description);
            }
        }


        [Xunit.Theory]
        [AutoData]
        public async Task TestCreateProductUsingAutoFixtureAsync(ProductDetails payload)
        {
            // Call the API to get product
            var response = await _apiRequest.PostAsync("/Product/Create", new APIRequestContextOptions { DataObject = payload });

            var jsonResponse = await response.JsonAsync();

            response.Status.Should().Be(200);
            _testOutputHelper.WriteLine("Response from Create Product API: " + jsonResponse.ToString());
            response.Ok.Should().BeTrue();

            var responsefromGet = await _apiRequest.GetAsync("/Product/GetProducts");
            var jsonResponseforGet = await responsefromGet.JsonAsync();
            var products = jsonResponseforGet?.Deserialize<List<ProductDetails>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            using (new AssertionScope())
            {
                products.Should().Contain(p => p.Name == payload.Name);
                products.Should().Contain(p => p.Price == payload.Price);
                products.Should().Contain(p => p.Description == payload.Description);
            }
        }

        [Xunit.Theory]
        [AutoData]
        public async Task TestDeleteProductUsingAutoFixtureAsync(ProductDetails payload)
        {
            // Call the API to get product
            var response = await _apiRequest.PostAsync("/Product/Create", new APIRequestContextOptions { DataObject = payload });

            var jsonResponse = await response.JsonAsync();

            int? id = null;
            if (jsonResponse != null && jsonResponse.Value.ValueKind == JsonValueKind.Object)
            {
                if (jsonResponse.Value.TryGetProperty("id", out var idElement))
                {
                    if (idElement.ValueKind == JsonValueKind.Number)
                    {
                        id = idElement.GetInt32();
                    }
                    else if (idElement.ValueKind == JsonValueKind.String)
                    {
                        string idString = idElement.GetString();
                        if (int.TryParse(idString, out int parsedId))
                        {
                            id = parsedId;
                        }
                    }
                }
            }

            response.Status.Should().Be(200);
            _testOutputHelper.WriteLine("Response from Create Product API: " + jsonResponse.ToString());
            _testOutputHelper.WriteLine("Id: " + id);
            response.Ok.Should().BeTrue();

            var res=await _apiRequest.DeleteAsync($"/Product/Delete/?id={id}");
            var responsefromGet = await _apiRequest.GetAsync($"/Product/GetProductById/{id}");

             responsefromGet.Status.Should().Be(204);
            _testOutputHelper.WriteLine("Response from Get Product API after Delete: " + responsefromGet.StatusText.ToString());
        }
    }
}
