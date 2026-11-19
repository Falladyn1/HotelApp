using HotelApp.Domain.Entities;
using HotelApp.Domain.Interfaces;
using HotelApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelDbContext _dbContext;
        public ReservationRepository(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(Reservation reservation)
        {
            _dbContext.Add(reservation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Room>> GetRooms()
        {
            return await _dbContext.Rooms.ToListAsync();
        }
    }
}
