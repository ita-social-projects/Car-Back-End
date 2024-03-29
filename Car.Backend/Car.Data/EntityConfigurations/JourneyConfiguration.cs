﻿using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public class JourneyConfiguration : IEntityTypeConfiguration<Journey>
    {
        public void Configure(EntityTypeBuilder<Journey> builder)
        {
            builder.ToTable("Journey");

            builder.HasKey(journey => journey.Id);

            builder.HasOne(journey => journey.Schedule)
                .WithOne(schedule => schedule!.Journey!)
                .HasForeignKey<Schedule>(schedule => schedule.Id);

            builder.HasOne(journey => journey.Organizer)
                .WithMany(user => user!.OrganizerJourneys)
                .HasForeignKey(journey => journey.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(journey => journey.Participants)
                .WithMany(user => user.ParticipantJourneys)
                .UsingEntity<JourneyUser>(
                    juBuilder => juBuilder.HasOne(ju => ju.User!)
                        .WithMany(u => u.JourneyUsers)
                        .HasForeignKey(ju => ju.UserId),
                    juBuilder => juBuilder.HasOne(ju => ju.Journey!)
                        .WithMany(j => j.JourneyUsers)
                        .HasForeignKey(ju => ju.JourneyId),
                    juBuilder =>
                    {
                        juBuilder.ToTable("JourneyUser");
                        juBuilder.HasKey(ju => new { ju.JourneyId, ju.UserId });
                    });

            builder.HasOne(journey => journey.Chat)
                .WithMany(chat => chat!.Journeys)
                .HasForeignKey(journey => journey.ChatId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(journey => journey.Car)
                .WithMany(car => car!.Journeys)
                .HasForeignKey(journey => journey.CarId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(journey => journey.JourneyPoints)
                .WithOne(point => point.Journey!)
                .HasForeignKey(point => point.JourneyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(journey => journey.Notifications)
                .WithOne(notification => notification.Journey!)
                .HasForeignKey(notification => notification.JourneyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(journey => journey.Comments).HasMaxLength(100);
        }
    }
}
