using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using The_Blob.Models;
using The_Blob.Infrastructure;
using The_Blob.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace The_Blob.Controllers
{
    public class MainController : Controller
    {
        private readonly BlobContext _context;

        public MainController(BlobContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //Gets data assigned to session key and sends it to the view
            User sessionUser = HttpContext.Session.GetJson<User>("User");
            Character sessionCharacter = HttpContext.Session.GetJson<Character>("Character");

            SqlParameter param3 = new SqlParameter("@CharID", sessionCharacter.CharacterId);

            List<CharacterFridge>  characterFridgeData = _context.CharacterFridge.FromSqlRaw("Select * from characterfridge where characterid = @CharID", param3).ToList();

            List<Fridge> fridgeData = new List<Fridge>();

            foreach(CharacterFridge id in characterFridgeData) {
                SqlParameter param4 = new SqlParameter("@FridgeID", id.FridgeId);
                Fridge item = _context.Fridge.FromSqlRaw("Select * from fridge where fridgeid = @FridgeID", param4).FirstOrDefault();
                fridgeData.Add(item);
            }

            List<Product> productData = _context.Product.FromSqlRaw("Select * from Product").ToList();

            Combiner combined = new Combiner(sessionUser, sessionCharacter, fridgeData, productData);
            return View(combined);
        }
    }
}
