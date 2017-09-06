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
            var allEmails = new List<Email>();
            var list = DBHandler.CreateUserList();
            list.ForEach(u =>
            {
                var articles = DBHandler.GetArticlesForUser(u.UserId);
                Console.WriteLine($"{articles.Count} Articles for:  {u.FirstName} {u.LastName}");
                if (articles.Count != 0)
                {
                    allEmails.Add(CreateEmail(u.EmailAddress, articles));
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
            var list = DBHandler.CreateUserList();
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
                        var emails = CreateAllEmails();
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
