using Restobooker.Domain.Interfaces;
using Restobooker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoBooker.Data.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private string connectionString;
        public ReservationRepository(string connectionstring)
        {
            this.connectionString = connectionstring;
        }

        public void AddReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public void DeleteReservation(int id)
        {
            throw new NotImplementedException();
        }

        public Reservation GetReservationById(int id)
        {

            throw new NotImplementedException();
        }

        public List<Reservation> GetReservations()
        {
            throw new NotImplementedException();
        }

        public List<Reservation> GetReservationsByFilter(string filter)
        {
            throw new NotImplementedException();
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}
