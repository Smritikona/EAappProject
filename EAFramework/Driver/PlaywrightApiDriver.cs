using Microsoft.Playwright;

namespace EAFramework.Driver
{
    public class PlaywrightApiDriver :  IDisposable
    {
        private IPlaywright _playwright;

        public async Task<IAPIRequestContext> InitializePlaywright(Dictionary<string, string> headers)
        {
            //Playwright 
            _playwright = await Playwright.CreateAsync();

            var apiRequestContext = new APIRequestNewContextOptions
            {
                BaseURL = "http://localhost:8001/",
                ExtraHTTPHeaders = headers,
                IgnoreHTTPSErrors = true
            };

            return await _playwright.APIRequest.NewContextAsync(apiRequestContext);
        }


        public void Dispose()
        { 
            //await _page.CloseAsync();
            //await _context.CloseAsync();
            //await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}
