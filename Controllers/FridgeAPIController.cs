using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using The_Blob.Data;
using The_Blob.Models;

namespace The_Blob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeAPIController : ControllerBase
    {
        private readonly BlobContext _context;

        public FridgeAPIController(BlobContext context)
        {
            _context = context;
        }

        // GET: api/FridgeAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fridge>>> GetFridge()
        {
            return await _context.Fridge.ToListAsync();
        }

        // GET: api/FridgeAPI/5
        [HttpGet("{foodName}")]
        public ActionResult<Fridge> GetFridge(string foodName)
        {
            SqlParameter param1 = new SqlParameter("@FoodName", foodName);
            Fridge fridge = _context.Fridge.FromSqlRaw("Select * from fridge where foodname = @FoodName", param1).FirstOrDefault();

            if (fridge == null)
            {
                return NotFound();
            }

            return fridge;
        }

        [HttpGet("fridge/{fridgeId}")]
        public ActionResult<Fridge> GetFridge(int fridgeId)
        {
            SqlParameter param1 = new SqlParameter("@FridgeID", fridgeId);
            Fridge fridge = _context.Fridge.FromSqlRaw("Select * from fridge where fridgeId = @FridgeID", param1).FirstOrDefault();

            if (fridge == null)
            {
                return NotFound();
            }

            return fridge;
        }

        // PUT: api/FridgeAPI/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutFridge(Fridge fridge)
        {
            SqlParameter param1 = new SqlParameter("@FoodName", fridge.FoodName);
            Fridge fridgeData = _context.Fridge.FromSqlRaw("Select * from fridge where foodname = @FoodName", param1).FirstOrDefault();
            SqlParameter param2 = new SqlParameter("@Quantity", (fridgeData.Quantity + 1));
            _context.Database.ExecuteSqlRaw("Update fridge set quantity=@Quantity where foodname = @FoodName", param2, param1);

            _context.Entry(fridge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FridgeExists(fridge.FridgeId))
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
        [HttpPatch("{fridgeId}")]
        public async Task<IActionResult> PatchFridge(int fridgeId)
        {
            SqlParameter param1 = new SqlParameter("@FridgeId", fridgeId);
            Fridge fridgeData = _context.Fridge.FromSqlRaw("Select * from fridge where fridgeId = @FridgeId", param1).FirstOrDefault();
            SqlParameter param2 = new SqlParameter("@Quantity", (fridgeData.Quantity - 1));
            _context.Database.ExecuteSqlRaw("Update fridge set quantity=@Quantity where fridgeId = @FridgeId", param2, param1);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FridgeExists(fridgeId))
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

        // POST: api/FridgeAPI
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Fridge>> PostFridge(Fridge fridge)
        {
            _context.Fridge.Add(fridge);
            await _context.SaveChangesAsync();

            return fridge;
        }

        // DELETE: api/FridgeAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fridge>> DeleteFridge(int id)
        {
            var fridge = await _context.Fridge.FindAsync(id);
            if (fridge == null)
            {
                return NotFound();
            }

            _context.Fridge.Remove(fridge);
            await _context.SaveChangesAsync();

            return fridge;
        }

        private bool FridgeExists(int id)
        {
            return _context.Fridge.Any(e => e.FridgeId == id);
        }
    }
}
