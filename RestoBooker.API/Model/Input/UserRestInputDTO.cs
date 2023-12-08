using Restobooker.Domain.Model;

namespace RestoBooker.API.Model.Input
{
    public class UserRestInputDTO
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}
