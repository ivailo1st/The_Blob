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
    public class CharacterFridgeAPIController : ControllerBase
    {
        private readonly BlobContext _context;

        public CharacterFridgeAPIController(BlobContext context)
        {
            _context = context;
        }

        // GET: api/CharacterFridgeAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterFridge>>> GetCharacterFridge()
        {
            return await _context.CharacterFridge.ToListAsync();
        }

        // GET: api/CharacterFridgeAPI/5
        [HttpGet("{fridgeID}")]
        public ActionResult<CharacterFridge> GetCharacterFridge(int fridgeID)
        {
            SqlParameter param1 = new SqlParameter("@FridgeID", fridgeID);
            Character sessionCharacter = HttpContext.Session.GetJson<Character>("Character");
            SqlParameter param2 = new SqlParameter("@CharID", sessionCharacter.CharacterId);
            CharacterFridge charFridge = _context.CharacterFridge.FromSqlRaw("Select * from characterfridge where fridgeid = @FridgeID and characterid = @CharID", param1, param2).FirstOrDefault();

            if (charFridge == null)
            {
                return NotFound();
            }

            return charFridge;
        }

        // PUT: api/CharacterFridgeAPI/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacterFridge(int id, CharacterFridge characterFridge)
        {
            if (id != characterFridge.CharacterFridgeId)
            {
                return BadRequest();
            }

            _context.Entry(characterFridge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterFridgeExists(id))
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

        // POST: api/CharacterFridgeAPI
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CharacterFridge>> PostCharacterFridge(CharacterFridge characterFridge)
        {
            _context.CharacterFridge.Add(characterFridge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacterFridge", new { id = characterFridge.CharacterFridgeId }, characterFridge);
        }

        // DELETE: api/CharacterFridgeAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CharacterFridge>> DeleteCharacterFridge(int id)
        {
            var characterFridge = await _context.CharacterFridge.FindAsync(id);
            if (characterFridge == null)
            {
                return NotFound();
            }

            _context.CharacterFridge.Remove(characterFridge);
            await _context.SaveChangesAsync();

            return characterFridge;
        }

        private bool CharacterFridgeExists(int id)
        {
            return _context.CharacterFridge.Any(e => e.CharacterFridgeId == id);
        }
    }
}
