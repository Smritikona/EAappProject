using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAappProject.Driver
{
    public class PlaywrightDriver  : IDisposable
    {
        private IPage _page;
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;
        public async Task<IPage> InitializeAsync()
        {
            Console.WriteLine("Starting SetupPlaywright...");
            //Playwright 
            var playwright = await Playwright.CreateAsync();

            //Browser Launch Settings
            var browserSettings = new BrowserTypeLaunchOptions
            {
                Headless = false,
                //SlowMo = 1000
            };

            //Browser
            var browser = await playwright.Chromium.LaunchAsync(browserSettings);

            //Page
            var context = await browser.NewContextAsync();
            _page = await context.NewPageAsync();

            //URL
            await _page.GotoAsync("http://localhost:8000/");
            Console.WriteLine("SetupPlaywright completed.");
            return _page;
        }

        public void Dispose()
        {
            _playwright.Dispose();
        }
    }
}
