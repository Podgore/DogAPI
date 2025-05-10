using DogAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogAPI.DAL.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Dog> Dogs { get; set; } = null!;

        public virtual DbSet<AnimalShelter> AnimalShelters { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
