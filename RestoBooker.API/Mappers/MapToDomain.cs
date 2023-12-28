using Microsoft.JSInterop.Infrastructure;
using Restobooker.Domain.Model;
using RestoBooker.API.Exceptions;
using RestoBooker.API.Model.Input;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;

namespace RestoBooker.API.Mappers
{
    public static class MapToDomain
    {
        public static User MapToUserDomain(UserRestInputDTO dto)
        {
            try
            {
                return new User(dto.Name,dto.ContactInfo, dto.Location);
            }
            catch (Exception ex) { throw new MapException("Map To User", ex); }
        }
        public static Restaurant MapToRestaurantDomain(RestaurantRestInputDTO dto)
        {
            try
            {
                return new Restaurant(dto.Name, dto.Location, dto.Cuisine, dto.ContactDetails);
            }
            catch(Exception ex) { throw new MapException("Map to Restaurant", ex); }
        }
        public static Reservation MapToReservationDomain(ReservationRestInputDTO dto)
        {
            try
            {
                return new Reservation(MapToRestaurantDomain(dto.Restaurant), MapToUserDomain(dto.ContactPerson), dto.NumberOfSeats, dto.Date, dto.Time, dto.TableNumber);
            }catch(Exception ex) { throw new MapException("Map to Reservation", ex); }
        }
    }
}
