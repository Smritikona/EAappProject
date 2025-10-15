using EAappProject.Model;
using Newtonsoft.Json;
using NUnit.Framework.Internal.Execution;
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
            var jsonData = File.ReadAllText(jsonFilePath);
            var productDetails = JsonConvert.DeserializeObject<ProductDetails>(jsonData);

            return productDetails;
        }

    }
}