using HotelApp.Domain.Entities;
using HotelApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Infrastructure.Seeders
{
    public class HotelRoomSeeder
    {
        private readonly HotelDbContext _dbContext;
        public HotelRoomSeeder(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private List<Room> RoomsCreator(int numersOfRooms)
        {
            var rooms = new List<Room>();
            for (int i = 0; i < numersOfRooms; i++)
            {
                var room = new Domain.Entities.Room() { Name = $"Domek {i+1}", Capacity = 4, PricePerNight = 220, Description = "Domek 4 osobowy"};
                rooms.Add(room);
            }
            return rooms;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (! await _dbContext.Rooms.AnyAsync())
                {

                    _dbContext.Rooms.AddRange(RoomsCreator(10));
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
