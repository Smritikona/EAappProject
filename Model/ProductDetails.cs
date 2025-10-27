using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAappProject.Model
{
    public class ProductDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ProductType ProductType { get; set; }
        public string NewPrice { get; set; }
    }


    public enum ProductType
    {
        CPU,
        MONITOR,
        PERIPHARALS,
        EXTERNAL
    }
}
