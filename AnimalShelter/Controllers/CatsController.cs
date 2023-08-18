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
    }
}