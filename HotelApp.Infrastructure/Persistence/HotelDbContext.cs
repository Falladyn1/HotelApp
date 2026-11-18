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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Reservation>()
                .OwnsOne(r => r.TimeFrame);

            modelBuilder.Entity<Room>()
                .ToTable("Room");

            modelBuilder.Entity<Room>()
                .Property(r => r.PricePerNight)
                .HasPrecision(18, 2);
        }
    }
}
