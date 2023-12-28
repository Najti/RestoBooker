using Restobooker.Domain.Model;

namespace RestoBooker.API.Model.Output
{
    public class UserRestOutputDTO
    {
        public UserRestOutputDTO(string name, Location location, ContactInfo contactInfo)
        {
            Name = name;
            Location = location;
            ContactInfo = contactInfo;
        }

        public string Name { get; set; }
        public Location Location { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}
