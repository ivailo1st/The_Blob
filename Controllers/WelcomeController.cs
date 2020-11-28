﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using The_Blob.Models;
using The_Blob.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

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
        public async Task<IActionResult> Signup([Bind("user,character")] Combiner combiner)
        {
            bool result = _context.User.Where(x => x.Email == combiner.user.Email).Any();
            if (ModelState.IsValid && !result)
            {
                SqlParameter param1 = new SqlParameter("@UserEmail", combiner.user.Email);
                SqlParameter param2 = new SqlParameter("@UserPassword", combiner.user.Password);
                SqlParameter param3 = new SqlParameter("@UserConfirmPassword", combiner.user.ConfirmPassword);
                SqlParameter param4 = new SqlParameter("@CharacterName", combiner.character.Name);

                _context.Database.ExecuteSqlRaw("createNewUser @UserEmail, @UserPassword, @UserConfirmPassword, @CharacterName", param1,param2,param3,param4);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Welcome");
            }
            else if (result)
            {
                ModelState.AddModelError("user.Email", "Login already exists");
            }
            return View("Singup", combiner);
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
            return View("Index", user);
        }
    }
}
