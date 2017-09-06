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
            Demo();
        }

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
            string sql = $"select category.category_name+' -- '+subcategory.subcategory_name" +
                            " from users" +
                                " join subscription_list on subscription_list.user_id = users.user_id" +
                                " join category on category.category_id = subscription_list.category_id" +
                                " join subscribe_details on subscribe_details.subscribe_list_id = subscription_list.list_id" +
                                " join subcategory on subcategory.subcategory_id = subscribe_details.subcategory_id" +
                                " where users.user_id =";
            List<User> list = DBHandler.CreateUserList();
            list.ForEach(x =>
            {
                Console.WriteLine($"{x.FirstName} {x.LastName} subscribes to: ");
                DBHandler.QueryDb($"{sql}{x.UserId}");
                Console.WriteLine();
            });
        }
        private static void Demo()
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
