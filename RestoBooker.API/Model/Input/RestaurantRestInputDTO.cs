using Restobooker.Domain.Model;

namespace RestoBooker.API.Model.Input
{
    public class RestaurantRestInputDTO
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Cuisine { get; set; }
        public ContactInfo ContactDetails { get; set; }
        public int RestaurantId { get; set; }
    }
}
