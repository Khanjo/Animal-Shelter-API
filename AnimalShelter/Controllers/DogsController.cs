using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalShelter.Models;

namespace AnimalShelter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly AnimalShelterContext _db;

        public DogsController(AnimalShelterContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<List<Dog>> Get(string name, string breed, string color, string pattern, int maxAge)
        {
            IQueryable<Dog> query = _db.Dogs.AsQueryable();

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
        public async Task<ActionResult<Dog>> GetDog(int id)
        {
            Dog dog = await _db.Dogs.FindAsync(id);

            if (dog == null)
            {
                return NotFound();
            }

            return dog;
        }

        [HttpPost]
        public async Task<ActionResult<Dog>> Post(Dog dog)
        {
            _db.Dogs.Add(dog);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDog), new { id = dog.DogId }, dog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Dog dog)
        {
            if (id != dog.DogId)
            {
                return BadRequest();
            }

            _db.Dogs.Update(dog);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch
            {
                if (!DogExists(id))
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

        private bool DogExists(int id)
        {
            return _db.Dogs.Any(dog => dog.DogId == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDog(int id)
        {
            Dog dog = await _db.Dogs.FindAsync(id);
            if (dog == null)
            {
                return NotFound();
            }
            _db.Dogs.Remove(dog);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}