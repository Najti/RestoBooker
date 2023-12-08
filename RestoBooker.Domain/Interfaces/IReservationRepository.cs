using Restobooker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restobooker.Domain.Interfaces
{
    public interface IReservationRepository
    {
        public Reservation UpdateReservation(Reservation reservation);
        public List<Reservation> GetReservations();
        public List<Reservation> GetReservationsByFilter(string filter);
        public void DeleteReservation(int id);
        public Reservation GetReservationById(int id);
        public void AddReservation(Reservation reservation);

    }
}
