using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using The_Blob.Models;
using The_Blob.Data;

namespace The_Blob.Controllers
{
    public class WelcomeController : Controller
    { 
        private readonly BlobContext _context;

        public WelcomeController(BlobContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("Index");
        }
        public IActionResult Singup()
        {
            return View("Singup");
        }
        // POST: Sign up- Create new user
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("UserId,Email,Password,Name")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Singup", "Welcome");
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("UserId,Email,Password")] User user)
        {
            bool result = _context.User.Where(x => x.Email == user.Email && x.Password == user.Password).Any();

            if (ModelState.IsValid && result)
            {
                return RedirectToAction("Index", "Characters");
            }
            else if (!result)
            {
                ModelState.AddModelError("Email", "Enter a valid login credential");
                ModelState.AddModelError("Password", "Enter a valid login credential");
            }
            else{ 
                return View("Index",user);
            }
            return View("Index", user);
        }
    }
}
