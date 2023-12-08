using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restobooker.Domain.Model
{
    public class ContactInfo
    {
        public ContactInfo()
        {
        }

        public ContactInfo(string phoneNumber, string email)
        {
            PhoneNumber = phoneNumber;
            Email = email;
        }
        public ContactInfo(int id, string phoneNumber, string email)
        {
            ContactInfoID = id;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ContactInfoID { get; set; }
    }
}
