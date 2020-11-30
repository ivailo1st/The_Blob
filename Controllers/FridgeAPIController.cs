﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Fridge>> GetFridge(int id)
        {
            var fridge = await _context.Fridge.FindAsync(id);

            if (fridge == null)
            {
                return NotFound();
            }

            return fridge;
        }

        // PUT: api/FridgeAPI/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFridge(int id, Fridge fridge)
        {
            if (id != fridge.FridgeId)
            {
                return BadRequest();
            }

            _context.Entry(fridge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FridgeExists(id))
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

            return CreatedAtAction("GetFridge", new { id = fridge.FridgeId }, fridge);
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
