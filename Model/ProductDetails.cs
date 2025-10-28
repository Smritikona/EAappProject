using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NUnit.Framework.Internal.OSPlatform;

namespace EAappProject.Model
{
    public class ProductDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ProductType ProductType { get; set; }
        public string? UpdatedName { get; set; }
        public int Id { get; set; }


    }
    public enum ProductType
    {
        CPU,
        MONITOR,
        EXTERNAL,
        PERIPHARALS
    }
}
