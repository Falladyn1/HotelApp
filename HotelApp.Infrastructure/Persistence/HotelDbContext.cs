using HotelApp.Domain.Entities;
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
            modelBuilder.Entity<Room>()
                .ToTable("Room");

            modelBuilder.Entity<Room>()
                .Property(r => r.PricePerNight)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Domain.Entities.Booking>(booking =>
            {
                booking.Property(b => b.GuestName).HasMaxLength(200).IsRequired();
                booking.Property(b => b.GuestEmail).HasMaxLength(320).IsRequired();
                booking.Property(b => b.GuestPhone).HasMaxLength(50);
                booking.OwnsOne(b => b.TimeFrame);
            });

            modelBuilder.Entity<Domain.Entities.Reservation>()
                .HasOne(reservation => reservation.Booking)
                .WithMany(booking => booking.Reservations)
                .HasForeignKey(reservation => reservation.BookingId);
        }
    }
}
