using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Blob.Models
{
    public class Fridge
    {
        public int FridgeId { get; set; }
        public string FoodName { get; set; }
        public int HungerPercentage { get; set; }
        public int Quantity { get; set; }
        public void IncreaseHunger()
        {

        }
    }
}
