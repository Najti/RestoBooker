using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restobooker.Domain.Model
{
    public class Restaurant
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Cuisine { get; set; }
        public ContactInfo ContactDetails { get; set; }
        public int RestaurantId { get; set; }

        // Constructor
        public Restaurant(string name, Location location, string cuisine, ContactInfo contactDetails)
        {
            Name = name;
            Location = location;
            Cuisine = cuisine;
            ContactDetails = contactDetails;
        }
    }
}
