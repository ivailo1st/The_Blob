using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Blob.Models
{
    public class CharacterFridge
    {
        public int CharacterFridgeId { get; set; }
        public int CharacterId { get; set; }

        public virtual Character character { get; set; }
        public int FridgeId { get; set; }

        public virtual Fridge fridge { get; set; }
    }
}
