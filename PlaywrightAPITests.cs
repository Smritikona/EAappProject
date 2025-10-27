using EAappProject.Driver;
using EAappProject.Model;
using Microsoft.Playwright;
using Newtonsoft.Json;
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
             
            var response = await _apiRequest.GetAsync("/Product/GetProductById/2");
            
            var jsonResponse = await response.JsonAsync();
            _testOutputHelper.WriteLine("Response JSON:\n" + jsonResponse);


            //Deserialize the response
            var product = JsonConvert.DeserializeObject<ProductDetails>(jsonResponse.ToString());
            _testOutputHelper.WriteLine($"Product: {product.Name}, {product.Description}, {product.Price}");

            //Validate the response
            Xunit.Assert.Equal(product.Name.Trim(), "Mouse",ignoreCase: true);
        }


    }
}
