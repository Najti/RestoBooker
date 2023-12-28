using Restobooker.Domain.Interfaces;
using Restobooker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restobooker.Domain.Services
{
    public class ReservationService
    {
        private IReservationRepository repo;
        public ReservationService(IReservationRepository repo)
        {
            this.repo = repo;
        }
        public Reservation UpdateReservation(Reservation reservation) { return repo.UpdateReservation(reservation); }
        public List<Reservation> GetReservations() { return repo.GetReservations(); }
        public List<Reservation> GetReservationsByFilter(string filter) { return repo.GetReservationsByFilter(filter); }
        public void DeleteReservation(int id) { repo.DeleteReservation(id); }
        public Reservation GetReservationById(int id) { return repo.GetReservationById(id); }
        public Reservation AddReservation(Reservation reservation) {return repo.AddReservation(reservation); }
    }
}
