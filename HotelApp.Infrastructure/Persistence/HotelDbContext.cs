using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Infrastructure.Persistence
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {

        }

        public DbSet<Domain.Entities.Room> Rooms { get; set; }
        public DbSet<Domain.Entities.Reservation> Reservations { get; set; }
        public DbSet<Domain.Entities.Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Room>(entity =>
            {
                entity.Property(r => r.PricePerNight).HasPrecision(18, 2);

            });

            modelBuilder.Entity<Domain.Entities.Booking>(entity =>
            {
                entity.OwnsOne(b => b.TimeFrame, tf =>
                {

                    tf.Property(t => t.StartDate).HasColumnName("StartDate");
                    tf.Property(t => t.EndDate).HasColumnName("EndDate");
                });
            });

            modelBuilder.Entity<Domain.Entities.Reservation>(entity =>
            {
                entity.HasOne(r => r.Booking)
                .WithMany(b => b.Reservations)
                .HasForeignKey(r => r.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Room)
                .WithMany(rm => rm.Reservations)
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(r => new { r.BookingId, r.RoomId })
                    .IsUnique();
            });
        }
    }
}
