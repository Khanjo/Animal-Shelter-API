using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalShelter.Models;

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
    }
}