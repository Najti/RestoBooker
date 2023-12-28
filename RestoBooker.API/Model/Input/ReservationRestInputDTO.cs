using Restobooker.Domain.Model;

namespace RestoBooker.API.Model.Input
{
    public class ReservationRestInputDTO
    {

        public int ReservationNumber { get; set; }
        public RestaurantRestInputDTO Restaurant { get; set; }
        public UserRestInputDTO ContactPerson { get; set; }
        public int NumberOfSeats { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int TableNumber { get; set; }
    }
}
