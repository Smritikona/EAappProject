using EAappProject.Driver;
using EAappProject.Model;
using Microsoft.Playwright;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

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

            // Deserialize the response
            var product = JsonConvert.DeserializeObject<ProductDetails>(jsonResponse.ToString());

            // Validate the response
            Xunit.Assert.Equal(product.Name, "Mouse");


            //Fluent Assertions
            //Better way to deserialize
        }

    }
}
