    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using System;

    namespace Restobooker.Domain.Model
    {
        public class User
        {
            private static int _nextCustomerId = 1;

            public int CustomerId { get; set; }
            public string Name { get; set; }
            public ContactInfo ContactInfo { get; set; }
            public Location Location { get; set; }

            // Constructor for registering a new user
            public User(string name, ContactInfo contactInfo, Location location)
            {
                Name = name;
                ContactInfo = contactInfo;
                Location = location;

                CustomerId = _nextCustomerId++;
            }

        public User(int customerId, string name, ContactInfo contactInfo, Location location)
        {
            CustomerId = customerId;
            Name = name;
            ContactInfo = contactInfo;
            Location = location;
        }

        public User()
        {
        }

        // Method to perform basic email validation
        public bool IsValidEmail()
            {
                return !string.IsNullOrWhiteSpace(ContactInfo.Email) && ContactInfo.Email.Contains("@");
            }

            // Method to check if the phone number contains only digits
            public bool IsValidPhoneNumber()
            {
                return !string.IsNullOrWhiteSpace(ContactInfo.PhoneNumber) && ContactInfo.PhoneNumber.All(char.IsDigit);
            }
        }
    }


