using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restobooker.Domain.Model
{
    public class Reservation
    {
        private static int _nextReservationId = 1;

        public int ReservationNumber { get; set; }
        public Restaurant Restaurant { get; set; }
        public User ContactPerson { get; set; }
        public int NumberOfSeats { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int TableNumber { get; set; }

        // Constructor for creating a reservation
        public Reservation(Restaurant restaurant, User contactPerson, int numberOfSeats, DateTime date, TimeSpan time, int tableNumber)
        {
            Restaurant = restaurant;
            ContactPerson = contactPerson;
            NumberOfSeats = numberOfSeats;
            Date = date;
            Time = time;
            TableNumber = tableNumber;

            ReservationNumber = _nextReservationId++;
        }
    }
}

