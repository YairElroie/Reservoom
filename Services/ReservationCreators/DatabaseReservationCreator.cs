using Reservoom.DbContexts;
using Reservoom.DTOs;
using Reservoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Services.ReservationCreators
{
    public class DatabaseReservationCreator : IReservationCreator
    {
        private readonly ReservoomDbContextFactory _dbContextFactory;

        public DatabaseReservationCreator(ReservoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateReservation(Reservation reservation)
        {
            using(ReservoomDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO resservationDto = ToReservationDto(reservation);

                context.Reservations.Add(resservationDto);

                context.SaveChangesAsync();
            }
        }

        private ReservationDTO ToReservationDto(Reservation reservation)
        {
            return new ReservationDTO()
            {
                FloorNumber = reservation.RoomId.FloorNumber,
                RoomNumber = reservation.RoomId.RoomNumber,
                Username = reservation.Username,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime
            };
        }
    }
}
