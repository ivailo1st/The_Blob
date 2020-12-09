using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using The_Blob.Data;
using The_Blob.Infrastructure;
using The_Blob.Models;

namespace The_Blob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterAPIController : ControllerBase
    {
        private readonly BlobContext _context;

        public CharacterAPIController(BlobContext context)
        {
            _context = context;
        }

        // GET: api/CharacterAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharacter()
        {
            return await _context.Character.ToListAsync();
        }

        // GET: api/CharacterAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacter(int id)
        {
            var character = await _context.Character.FindAsync(id);

            if (character == null)
            {
                return NotFound();
            }

            return character;
        }

        // PUT: api/CharacterAPI/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, Character character)
        {
            if (id != character.CharacterId)
            {
                return BadRequest();
            }

            _context.Entry(character).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPatch("Bars/{awake}")]
        public async Task<IActionResult> CharacterAwake(bool awake)
        {
            Character sessionCharacter = HttpContext.Session.GetJson<Character>("Character");

            SqlParameter param1 = new SqlParameter("@CharID", sessionCharacter.CharacterId);
            SqlParameter param2 = new SqlParameter("@Awake", awake);

            _context.Database.ExecuteSqlRaw("Update character set awake = @Awake where characterid = @CharID ", param2, param1);

            try
            {
                await _context.SaveChangesAsync();

                Character characterData = _context.Character.FromSqlRaw("Select * from character where characterid = @CharID", param1).FirstOrDefault();
                HttpContext.Session.SetJson("Character", characterData);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(sessionCharacter.CharacterId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPatch("{currency}")]
        public async Task<IActionResult> PatchCurrency(int currency)
        {
            Character sessionCharacter = HttpContext.Session.GetJson<Character>("Character");
            SqlParameter param1 = new SqlParameter("@CharID",sessionCharacter.CharacterId);
            SqlParameter param2 = new SqlParameter("@CharCurrency", currency);

            _context.Database.ExecuteSqlRaw("Update character set currency = @CharCurrency where characterid = @CharID ",param2,param1);

            try
            {
                await _context.SaveChangesAsync();

                Character characterData = _context.Character.FromSqlRaw("Select * from character where characterid = @CharID", param1).FirstOrDefault();
                HttpContext.Session.SetJson("Character", characterData);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(sessionCharacter.CharacterId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPatch]
        public async Task<IActionResult> PatchCharacter(long[] Bars)
        {
            Character sessionCharacter = HttpContext.Session.GetJson<Character>("Character");
            SqlParameter param1 = new SqlParameter("@CharID", sessionCharacter.CharacterId);
            if (Bars.Length != 1) {                 
                SqlParameter param2 = new SqlParameter("@Hunger", Bars[0]);
                SqlParameter param3 = new SqlParameter("@Sleep", Bars[1]);
                SqlParameter param4 = new SqlParameter("@Fun", Bars[2]);
                SqlParameter param5 = new SqlParameter("@LogDate", Bars[3]);
                _context.Database.ExecuteSqlRaw("Update character set hunger = @Hunger, sleep = @Sleep, fun = @Fun, logDate = @LogDate where characterid = @CharID ", param2, param3, param4, param5, param1);
            }
            else
            {          
                SqlParameter param2 = new SqlParameter("@Hunger", Bars[0]);
                _context.Database.ExecuteSqlRaw("Update character set hunger = @Hunger where characterid = @CharID ", param2, param1);
            }
            

            try
            {
                await _context.SaveChangesAsync();

                Character characterData = _context.Character.FromSqlRaw("Select * from character where characterid = @CharID", param1).FirstOrDefault();
                HttpContext.Session.SetJson("Character", characterData);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(sessionCharacter.CharacterId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CharacterAPI
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(Character character)
        {
            _context.Character.Add(character);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacter", new { id = character.CharacterId }, character);
        }

        // DELETE: api/CharacterAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Character>> DeleteCharacter(int id)
        {
            var character = await _context.Character.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            _context.Character.Remove(character);
            await _context.SaveChangesAsync();

            return character;
        }

        private bool CharacterExists(int id)
        {
            return _context.Character.Any(e => e.CharacterId == id);
        }
    }
}
