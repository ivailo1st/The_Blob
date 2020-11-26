using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Blob.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public int CharacterId { get; set; }

        public virtual Character character { get; set; }
        public void CreateUser()
        {

        }
        public void EditUser()
        {

        }
        public void RemoveUser()
        {

        }
    }
}
