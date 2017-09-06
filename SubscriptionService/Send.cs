using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService
{
    class Send
    {
        static List<Email> CreateAllEmails()
        {
            List<Email> allEmails = new List<Email>();
            List<User> list = DBHandler.CreateUserList();

            list.ForEach(x =>
            {
                List<Article> articles = DBHandler.GetArticlesForUser(x.UserId);
                Console.WriteLine($"{articles.Count} Articles for:  {x.FirstName} {x.LastName}");
                if (articles.Count != 0)
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
                email.Text += $"{a.Header} by {a.Editor}\n{a.Text}" +
                $"\n------------------------------------------------\n";
            });
            return email;
        }
        static void ShowSubscriptions()
        {           
            List<User> list = DBHandler.CreateUserList();
            list.ForEach(u =>{Console.WriteLine($"{u.FirstName} {u.LastName} subscribes to: "); u.ShowSubscribtions();});
        }
        public static void Demo()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("(S)how subscriptions");
                Console.WriteLine("(V)iew all emails");
                Console.WriteLine("(Q)uit");
                Console.Write(": ");
                string choice = Console.ReadLine().ToLower();
                switch (choice)
                {
                    case "s":
                        ShowSubscriptions();
                        Console.ReadKey();
                        break;
                    case "v":
                        List<Email> emails = CreateAllEmails();
                        Console.WriteLine();
                        emails.ForEach(e => e.ToString());
                        Console.ReadKey();
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
