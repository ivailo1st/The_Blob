using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Blob.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductPercentage { get; set; }
        public string ImageURL { get; set; }

        public void AddItem()
        {

        }
    }
}
