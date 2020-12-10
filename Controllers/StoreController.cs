using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using The_Blob.Data;
using The_Blob.Infrastructure;
using The_Blob.Models;

namespace The_Blob.Controllers
{
    public class StoreController : Controller
    {
        private readonly BlobContext _context;

        public StoreController(BlobContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Fridge()
        {
            User sessionUser = HttpContext.Session.GetJson<User>("User");
            Character sessionCharacter = HttpContext.Session.GetJson<Character>("Character");

            SqlParameter param3 = new SqlParameter("@CharID", sessionCharacter.CharacterId);

            List<CharacterFridge> characterFridgeData = _context.CharacterFridge.FromSqlRaw("Select * from characterfridge where characterid = @CharID", param3).ToList();

            List<Fridge> fridgeData = new List<Fridge>();

            foreach (CharacterFridge id in characterFridgeData)
            {
                SqlParameter param4 = new SqlParameter("@FridgeID", id.FridgeId);
                Fridge item = _context.Fridge.FromSqlRaw("Select * from fridge where fridgeid = @FridgeID", param4).FirstOrDefault();
                fridgeData.Add(item);
            }

            List<Product> productData = _context.Product.FromSqlRaw("Select * from Product").ToList();

            Combiner combined = new Combiner(sessionUser, sessionCharacter, fridgeData, productData);

            return PartialView("_FridgePartial",combined);
        }
        public IActionResult Store()
        {
            User sessionUser = HttpContext.Session.GetJson<User>("User");
            Character sessionCharacter = HttpContext.Session.GetJson<Character>("Character");

            SqlParameter param3 = new SqlParameter("@CharID", sessionCharacter.CharacterId);

            List<CharacterFridge> characterFridgeData = _context.CharacterFridge.FromSqlRaw("Select * from characterfridge where characterid = @CharID", param3).ToList();

            List<Fridge> fridgeData = new List<Fridge>();

            foreach (CharacterFridge id in characterFridgeData)
            {
                SqlParameter param4 = new SqlParameter("@FridgeID", id.FridgeId);
                Fridge item = _context.Fridge.FromSqlRaw("Select * from fridge where fridgeid = @FridgeID", param4).FirstOrDefault();
                fridgeData.Add(item);
            }

            List<Product> productData = _context.Product.FromSqlRaw("Select * from Product").ToList();

            Combiner combined = new Combiner(sessionUser, sessionCharacter, fridgeData, productData);

            return PartialView("_StorePartial",combined);
        }
    }
}
