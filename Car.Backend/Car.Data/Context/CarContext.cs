﻿using Car.Data.Entities;
using Car.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Car.Data.Context
{
    public class CarContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Entities.Car> Cars { get; set; }

        public DbSet<Journey> Journeys { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Schedule> Schedule { get; set; }

        public DbSet<Stop> Stops { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserJourney> UserJourney { get; set; }

        public DbSet<UserPreferences> UserPreferences { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Chat> Chats { get; set; }

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
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new ModelConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());

            modelBuilder.Seed();
        }
    }
}
