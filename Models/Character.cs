using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Blob.Models { 

    public class Character
    {
        public int CharacterId { get; set; }
        [Required(ErrorMessage = "You must enter a Name")]
        public string Name { get; set; }
        public int Hunger { get; set; } = 100;
        public int Sleep { get; set; } = 100;
        public bool Awake { get; set; } = true;
        public int Fun { get; set; } = 100;
        public int Currency { get; set; } = 500;
        public DateTime LogDate { get; set; }

        public int UserId { get; set; }
        public virtual User user { get; set; }
        public void CharacterCreation()
        {

        }

        public void LowerBars()
        {

        }

        public void CharacterEdit()
        {

        }
        public void LowerSleep()
        {

        }
        public void IncreaseSleep()
        {

        }
        public void IncreaseFun()
        {

        }


    }


}