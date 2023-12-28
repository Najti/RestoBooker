using Restobooker.Domain.Model;

namespace RestoBooker.API.Model.Output
{
    public class RestaurantRestOutputDTO
    {
        public RestaurantRestOutputDTO(string name, Location location, string cuisine, ContactInfo contactDetails)
        {
            Name = name;
            Location = location;
            Cuisine = cuisine;
            ContactDetails = contactDetails;
        }

        public string Name { get; set; }
        public Location Location { get; set; }
        public string Cuisine { get; set; }
        public ContactInfo ContactDetails { get; set; }
    }
}
