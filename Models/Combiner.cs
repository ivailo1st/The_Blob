﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Blob.Models
{
    public class Combiner
    {
        public User user { get; set; }
        public Character character { get; set; }
        public List<Fridge> fridge { get; set; }
        public List<Product> product { get; set; }

        public Combiner()
        {

        }
        public Combiner(User User,Character Character)
        {
            user = User;
            character = Character;
        }

        public Combiner(User User, Character Character, List<Fridge> Fridges, List<Product> Products)
        {
            user = User;
            character = Character;
            fridge = Fridges;
            product = Products;
        }
    }
}
