using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Email> emails = CreateAllEmails();
            emails.ForEach(e => Console.WriteLine($"To: {e.Reciever}\n{e.Subject}\n{e.Text}\n\n"));

        }
        static List<Email> CreateAllEmails()
        {
            List<Email> allEmails = new List<Email>();
            List<User> list = DBHandler.CreateUserList();
            list.ForEach(x =>
            {
                Console.WriteLine($"Articles for:  {x.FirstName} {x.LastName}");
                List<Article> articles = DBHandler.GetArticlesForUser(x.UserId);
                Console.WriteLine($"articles count: {articles.Count}");
                if(articles.Count != 0)
                {
                    allEmails.Add(CreateEmail(x.EmailAddress, articles));
                }
                              
            });
            return allEmails;
        }

        static Email CreateEmail(string emailaddress, List<Article> articles)
        {
            Email email = new Email($"Newsletter from News Website {DateTime.Now}", emailaddress);
            articles.ForEach(a => 
            {
                email.Text += $"{a.Header}\nby {a.Editor}\n{a.Text}\n";
             });
            return email;
        }
    } 
}
