using EAappProject.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
