using Car.DAL.Entities;
using Car.DAL.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Car.DAL.Context
{
    public class CarContext : DbContext
    {
        DbSet<User> User { get; set; }

        DbSet<Journey> Journeys { get; set; }

        DbSet<UserJourney> UserJourney { get; set; }

        DbSet<Address> Addresses { get; set; }

        DbSet<Entities.Car> Cars { get; set; }

        DbSet<Message> Messages { get; set; }

        DbSet<Notification> Notifications { get; set; }

        DbSet<UserPreferences> UserPreferences { get; set; }

        DbSet<Stop> Stops { get; set; }

        DbSet<Schedule> Schedule { get; set; }

        public CarContext(DbContextOptions<CarContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new JourneyConfiguration());
            modelBuilder.ApplyConfiguration(new UserJourneyConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new UserPreferencesConfiguration());
            modelBuilder.ApplyConfiguration(new StopConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
        }
    }
}
