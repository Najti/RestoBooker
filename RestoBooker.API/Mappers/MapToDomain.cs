using Microsoft.JSInterop.Infrastructure;
using Restobooker.Domain.Model;
using RestoBooker.API.Exceptions;
using RestoBooker.API.Model.Input;
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
            catch (Exception ex) { throw new MapException("maptogemeentedomain", ex); }
        }
    }
}
