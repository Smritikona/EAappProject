using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAappProject.Driver
{
    public interface IPlaywrightDriver
    {
        Task<IPage> InitializeAsync();
        void Dispose();
    }

    public class PlaywrightDriver : IDisposable, IPlaywrightDriver
    {
        private IPage? _page;
        private IBrowser? _browser;
        private IBrowserContext? _context;
        private IPlaywright? _playwright;
        public async Task<IPage> InitializeAsync()
        {
            if (_page == null)
            {
                Console.WriteLine("Starting SetupPlaywright...");
                //Playwright 
                _playwright = await Playwright.CreateAsync();

                //Browser Launch Settings
                var browserSettings = new BrowserTypeLaunchOptions
                {
                    Headless = false,
                    //SlowMo = 1000
                };

                //Browser
                _browser = await _playwright.Chromium.LaunchAsync(browserSettings);
            //Page
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
            }

            //URL
            await _page.GotoAsync("http://localhost:8000/");
            Console.WriteLine("SetupPlaywright completed.");
            return _page;
        }

        public void Dispose()
        {
            _browser?.CloseAsync().GetAwaiter().GetResult();
            _playwright?.Dispose();
        }
    }
}
