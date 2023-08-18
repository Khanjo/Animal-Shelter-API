using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalShelter.Models;
using Microsoft.AspNetCore.Cors;

namespace AnimalShelter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private readonly AnimalShelterContext _db;

        public CatsController(AnimalShelterContext db)
        {
            _db = db;
        }

        [EnableCors("policy1")]
        [HttpGet]
        public async Task<List<Cat>> Get(string name, string breed, string color, string pattern, int maxAge)
        {
            IQueryable<Cat> query = _db.Cats.AsQueryable();

            if (name != null)
            {
                query = query.Where(entry => entry.Name == name);
            }

            if (breed != null)
            {
                query = query.Where(entry => entry.Breed == breed);
            }

            if (color != null)
            {
                query = query.Where(entry => entry.Color == color);
            }

            if (pattern != null)
            {
                query = query.Where(entry => entry.Pattern == pattern);
            }

            if (maxAge > 0)
            {
                query = query.Where(entry => entry.Age <= maxAge);
            }

            return await query.ToListAsync();
        }

        [EnableCors("policy1")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Cat>> GetCat(int id)
        {
            Cat cat = await _db.Cats.FindAsync(id);

            if (cat == null)
            {
                return NotFound();
            }

            return cat;
        }

        [HttpPost]
        public async Task<ActionResult<Cat>> Post(Cat cat)
        {
            _db.Cats.Add(cat);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCat), new { id = cat.CatId }, cat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Cat cat)
        {
            if (id != cat.CatId)
            {
                return BadRequest();
            }

            _db.Cats.Update(cat);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatExists(id))
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

        private bool CatExists(int id)
        {
            return _db.Cats.Any(cat => cat.CatId == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCat(int id)
        {
            Cat cat = await _db.Cats.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }
            _db.Cats.Remove(cat);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}