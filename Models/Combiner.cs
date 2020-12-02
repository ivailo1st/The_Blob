using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Blob.Models
{
    public class Combiner
    {
        public User user { get; set; }
        public Character character { get; set; }
        public Fridge fridge { get; set; }

        public Combiner(User User,Character Character)
        {
            user = User;
            character = Character;
        }

        public Combiner(User User, Character Character, Fridge Fridge)
        {
            user = User;
            character = Character;
            fridge = Fridge;
        }
    }
}
