﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using The_Blob.Models;
using The_Blob.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using The_Blob.Infrastructure;

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
        public IActionResult Logout()
        {
            HttpContext.Session.Logout();
            return View("Index");
        }

        // POST: Sign up- Create new user
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("user,character")] Combiner combiner)
        {
            //Variable that checks whether the email provided already exists in the database
            //This is done by using a lambda function that goes through each user email in the database
            //and compares it with the given one.

            bool result = _context.User.Where(x => x.Email == combiner.user.Email).Any();
            if (ModelState.IsValid && !result)
            {
                //Local Variables that serve as a way to assign specific values to the raw sql query after them
                //Each one represents a different given value
                SqlParameter param1 = new SqlParameter("@UserEmail", combiner.user.Email);
                SqlParameter param2 = new SqlParameter("@UserPassword", combiner.user.Password);
                SqlParameter param3 = new SqlParameter("@CharacterName", combiner.character.Name);

                //A dbcontext function that is cable of executing raw sql code in mvc
                //Here it executes a stored procedure using the variables from before
                _context.Database.ExecuteSqlRaw("createNewUser @UserEmail, @UserPassword, @CharacterName", param1,param2,param3);
                await _context.SaveChangesAsync();
                return View("Index");
            }
            else if (result)
            {
                ModelState.AddModelError("user.Email", "Login already exists");
            }
            return View("Singup", combiner);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserId,Email,Password")] User user)
        {
            bool result = _context.User.Where(x => (x.Email == user.Email) && (x.Password == user.Password)).Any();
            if (ModelState.IsValid && result)
            {
                //Local Variable that serve as a way to assign specific values to the raw sql query after them
                SqlParameter param1 = new SqlParameter("@UserEmail", user.Email);

                //A dbcontext function that is cable of executing raw sql code in mvc
                //Here it extracts all data of the user from the database
                User userData = _context.User.FromSqlRaw("Select * from [user] where email = @UserEmail", param1).FirstOrDefault();

                SqlParameter param2 = new SqlParameter("@UserID", userData.UserId);

                Character characterData = _context.Character.FromSqlRaw("Select * from character where userid = @UserID", param2).FirstOrDefault();


                //Variable used to check whether there is an already assigned variable to the session key
                User sessionUser = HttpContext.Session.GetJson<User>("User");
                Character sessionCharacter = HttpContext.Session.GetJson<Character>("Character");
                Fridge sessionFridge = HttpContext.Session.GetJson<Fridge>("Fridge");

                if (sessionUser == null || sessionCharacter == null)
                {
                    //If there is not it assigns it the data we got from the sql query
                    HttpContext.Session.SetJson("User", userData);
                    HttpContext.Session.SetJson("Character", characterData);

                }
                
                return RedirectToAction("Index", "Main");
            }
            else
            {
                ModelState.AddModelError("Email", "Enter a valid login credential");
                ModelState.AddModelError("Password", "Enter a valid login credential");
                return View("Index");
            }
        }
    }
}
