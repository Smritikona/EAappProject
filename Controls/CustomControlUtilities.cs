using Microsoft.Playwright;

namespace EAappProject.Controls
{
    public static class CustomControlUtilities
    {

        //Write a method to perform Clear and fill 
        public static async Task ClearAndFillElementAsync(this ILocator element, string text)
        {
            try
            {
                await element.ClearAsync();
                await element.FillAsync(text);
            }
            catch (Exception ex)
            {
                throw new Exception("Clear and Fill Element failed due to " + ex);
            }
        }

        public static async Task SelectDropDownWithIndexAsync(this ILocator element, int index)
        {
            await element.SelectOptionAsync(new SelectOptionValue { Index = index });
        }

        public static async Task SelectDropDownWithMulipleIndexAsync(this ILocator element, IEnumerable<int> indexes)
        {
            foreach (var index in indexes)
            {
                await element.SelectOptionAsync(new SelectOptionValue { Index = index });
            }
        }

        public static async Task SelectDropDownWithTextAsync(this ILocator element, string label)
        {
            await element.SelectOptionAsync(new SelectOptionValue { Label = label });
        }

        public static async Task SelectDropDownWithValueAsync(ILocator element, string value)
        {
            await element.SelectOptionAsync(new SelectOptionValue { Value = value });
        }

        public static async Task SelectListWithMultipleElementsAsync(ILocator element, IEnumerable<string> values)
        {
            await element.SelectOptionAsync(values);
        }
    }
}
