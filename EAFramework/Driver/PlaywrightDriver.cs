using Microsoft.Playwright;

namespace EAFramework.Driver
{
    public interface IPlaywrightDriver
    {
        Task<IPage> InitializePlaywright();
    }

    public class PlaywrightDriver :  IDisposable, IPlaywrightDriver
    {

        private IPage _page;
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;

        public async Task<IPage> InitializePlaywright()
        {
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

            //URL
            await _page.GotoAsync("http://localhost:8000/");

            return _page;
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
