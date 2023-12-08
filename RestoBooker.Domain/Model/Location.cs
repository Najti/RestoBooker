using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restobooker.Domain.Model
{
    public class Location
    {
        public int LocationID { get; set; }
        public int Postcode { get; set; } // 4 digits

        private string _municipalityName;
        public string MunicipalityName
        {
            get { return _municipalityName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Municipality name cannot be empty.");
                }
                _municipalityName = value;
            }
        }

        public string StreetName { get; set; } // Optional

        public string HouseNumberLabel { get; set; } // Optional, may also include apartment number

        // Constructor
        public Location(int postcode, string municipalityName, string streetName = null, string houseNumberLabel = null)
        {
            Postcode = postcode;
            MunicipalityName = municipalityName;
            StreetName = streetName;
            HouseNumberLabel = houseNumberLabel;
        }
        public Location(int id, int postcode, string municipalityName, string streetName = null, string houseNumberLabel = null)
        {
            LocationID = id;
            Postcode = postcode;
            MunicipalityName = municipalityName;
            StreetName = streetName;
            HouseNumberLabel = houseNumberLabel;
        }
    }
}
