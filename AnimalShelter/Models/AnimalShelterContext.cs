using Microsoft.EntityFrameworkCore;

namespace AnimalShelter.Models
{
    public class AnimalShelterContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }
        public DbSet<Dog> Dogs { get; set; }

        public AnimalShelterContext(DbContextOptions<AnimalShelterContext> options) : base(options)
        {
        }
    }
}