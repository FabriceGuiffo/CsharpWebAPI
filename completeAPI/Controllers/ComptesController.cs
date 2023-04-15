using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using completeAPI.DAL;
using completeAPI.Model;

namespace completeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComptesController : ControllerBase
    {
        private readonly GeneralContext _context;

        public ComptesController(GeneralContext context)
        {
            _context = context;
        }

        // GET: Comptes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compte>>> GetComptes()
        {
          if (_context.Comptes == null)
          {
              return NotFound();
          }
            return await _context.Comptes.ToListAsync();
        }

        // GET: Comptes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Compte>> GetCompte(int id)
        {
          if (_context.Comptes == null)
          {
              return NotFound();
          }
            var compte = await _context.Comptes.FindAsync(id);

            if (compte == null)
            {
                return NotFound();
            }

            return compte;
        }

        // PUT: Comptes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompte(int id, Compte compte)
        {
            if (id != compte.CompteID)
            {
                return BadRequest();
            }

            _context.Entry(compte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompteExists(id))
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

        // POST: api/Comptes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Compte>> PostCompte(Compte compte)
        {
          if (_context.Comptes == null)
          {
              return Problem("Entity set 'GeneralContext.Comptes'  is null.");
          }
            _context.Comptes.Add(compte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompte", new { id = compte.CompteID }, compte);
            
        }   

        // DELETE: api/Comptes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompte(int id)
        {
            if (_context.Comptes == null)
            {
                return NotFound();
            }
            var compte = await _context.Comptes.FindAsync(id);
            if (compte == null)
            {
                return NotFound();
            }

            _context.Comptes.Remove(compte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompteExists(int id)
        {
            return (_context.Comptes?.Any(e => e.CompteID == id)).GetValueOrDefault();
        }
    }
}
