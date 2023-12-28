using RestoBooker.API.Model.Input;

namespace RestoBooker.API.Model.Output
{
    public class ReservationRestOutputDTO
    {
        public ReservationRestOutputDTO(int reservationNumber, RestaurantRestOutputDTO restaurant, UserRestOutputDTO contactPerson, int numberOfSeats, DateTime date, TimeSpan time, int tableNumber)
        {
            ReservationNumber = reservationNumber;
            Restaurant = restaurant;
            ContactPerson = contactPerson;
            NumberOfSeats = numberOfSeats;
            Date = date;
            Time = time;
            TableNumber = tableNumber;
        }

        public int ReservationNumber { get; set; }
        public RestaurantRestOutputDTO Restaurant { get; set; }
        public UserRestOutputDTO ContactPerson { get; set; }
        public int NumberOfSeats { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int TableNumber { get; set; }
    }
}
