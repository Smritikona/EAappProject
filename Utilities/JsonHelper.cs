using EAappProject.Model;
using Newtonsoft.Json;

namespace EAappProject.Utilities
{
    public class JsonHelper
    {


        public static ProductDetails ReadJsonFile()
        {
            var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "ProductDetails.json");
            var jsonbody = File.ReadAllText(jsonFilePath);
            var productDetail = JsonConvert.DeserializeObject<ProductDetails>(jsonbody);
            return productDetail;

        }

    }
}
