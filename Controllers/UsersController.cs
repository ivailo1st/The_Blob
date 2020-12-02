using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using The_Blob.Data;
using The_Blob.Infrastructure;
using The_Blob.Models;

namespace The_Blob.Controllers
{
    public class UsersController : Controller
    {
        private readonly BlobContext _context;

        public UsersController(BlobContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            User sessionUser = HttpContext.Session.GetJson<User>("User");
            Character sessionCharacter = HttpContext.Session.GetJson<Character>("Character");

            Combiner combined = new Combiner(sessionUser, sessionCharacter);
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(combined);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("user,character")] Combiner combiner)
        {
            if (id != combiner.user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    SqlParameter param1 = new SqlParameter("@UserEmail", combiner.user.Email);
                    SqlParameter param2 = new SqlParameter("@UserID", combiner.user.UserId);
                    Character characterData = _context.Character.FromSqlRaw("Select * from character where userid = @UserID", param2).FirstOrDefault();

                    SqlParameter param3 = new SqlParameter("@UserPassword", combiner.user.Password);
                    _context.Database.ExecuteSqlRaw("Update [user] set email = @UserEmail, password = @UserPassword where userid = @UserID ", param1,param3,param2);

                    SqlParameter param4 = new SqlParameter("@CharacterID", characterData.CharacterId);
                    SqlParameter param5 = new SqlParameter("@CharacterName", combiner.character.Name);
                    
                    _context.Database.ExecuteSqlRaw("Update character set name = @CharacterName where characterid = @CharacterID",param5,param4);
                    await _context.SaveChangesAsync();

                    User userSession = _context.User.FromSqlRaw("Select * from [user] where userid = @UserID", param2).FirstOrDefault();
                    Character characterSession = _context.Character.FromSqlRaw("Select * from character where userid = @UserID", param2).FirstOrDefault();

                    HttpContext.Session.SetJson("User", userSession);
                    HttpContext.Session.SetJson("Character", characterSession);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(combiner.user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","Main");
            }
            return View(combiner);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
    }
}
