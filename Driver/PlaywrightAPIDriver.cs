using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAappProject.Driver
{
    public class PlaywrightAPIDriver  : IDisposable
    {
        private IPlaywright? _playwright;
        public async Task<IAPIRequestContext> InitializeAsync(Dictionary<string, string> headers)
        {
            Console.WriteLine("Starting SetupPlaywright...");
            //Playwright 
            _playwright = await Playwright.CreateAsync();

            var apiRequestOptions = new APIRequestNewContextOptions
            {
                BaseURL = "https://localhost:44334",
                ExtraHTTPHeaders = headers,
                IgnoreHTTPSErrors = true
            };
            return await _playwright.APIRequest.NewContextAsync(apiRequestOptions);
        }

        public void Dispose()
        {
            _playwright?.Dispose();
        }
    }
}
