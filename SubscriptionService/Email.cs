using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService
{
    class Email
    {
        public string Subject { get; set; }
        public string Reciever { get; set; }
        public string Text { get; set; }

        public Email(string subject, string reciever)
        {
            Subject = subject;
            Reciever = reciever;
        }
    }
}
