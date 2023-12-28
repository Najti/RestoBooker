using Restobooker.Domain.Model;
using RestoBooker.API.Exceptions;
using RestoBooker.API.Model.Output;

namespace RestoBooker.API.Mappers
{
    public static class MapFromDomain
    {
        public static UserRestOutputDTO MapFromUserDomain(User user)
        {
            try
            {
                return new UserRestOutputDTO(user.Name, user.Location, user.ContactInfo);
            }
            catch (Exception ex) { throw new MapException("Map From User", ex); }
        }
        public static RestaurantRestOutputDTO MapToRestaurantDomain(Restaurant restaurant)
        {
            try
            {
                return new RestaurantRestOutputDTO(restaurant.Name, restaurant.Location, restaurant.Cuisine, restaurant.ContactDetails);
            }
            catch (Exception ex) { throw new MapException("Map From Restaurant", ex); }
        }
        public static ReservationRestOutputDTO MapToReservationDomain(Reservation reservation)
        {
            try
            {
                return new ReservationRestOutputDTO(reservation.ReservationNumber, MapToRestaurantDomain(reservation.Restaurant), MapFromUserDomain(reservation.ContactPerson), reservation.NumberOfSeats, reservation.Date, reservation.Time, reservation.TableNumber);
            }
            catch (Exception ex) { throw new MapException("Map From Reservation", ex); }
        }
    }
}
