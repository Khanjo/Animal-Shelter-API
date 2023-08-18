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
    }
}