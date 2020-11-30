using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using The_Blob.Models;
using The_Blob.Infrastructure;

namespace The_Blob.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            //Gets data assigned to session key and sends it to the view
            User sessionUser = HttpContext.Session.GetJson<User>("User");
            Character sessionCharacter = HttpContext.Session.GetJson<Character>("Character");
            Combiner combined = new Combiner(sessionUser, sessionCharacter);
            return View(combined);
        }
    }
}
