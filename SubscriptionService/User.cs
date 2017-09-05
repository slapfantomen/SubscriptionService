using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public User(int userId, string firstName, string lastName, string emailAddress)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }
    }
}
