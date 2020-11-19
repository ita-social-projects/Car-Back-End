using Car.DAL.Entities;
using Car.DAL.EntityConfigurations;
using Microsoft.EntityFrameworkCore;


namespace Car.DAL.Context
{
    public class CarContext : DbContext
    {
        DbSet<User> User { get; set; }
        public CarContext(DbContextOptions<CarContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
