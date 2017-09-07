using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SubscriptionService
{
    class Send
    {
        static List<Email> CreateAllEmails(OutputType t)
        {
            var allEmails = new List<Email>();
            var list = DBHandler.CreateUserList();
            list.ForEach(u =>
            {
                var articles = DBHandler.GetArticlesForUser(u.UserId);
                Console.WriteLine($"{articles.Count} Articles for:  {u.FirstName} {u.LastName}");
                if (articles.Count != 0)
                {
                    allEmails.Add(CreateEmail(u.EmailAddress, articles, t));
                }
            });
            return allEmails;
        }
        static Email CreateEmail(string emailaddress, List<Article> articles, OutputType t)
        {
            var email = new Email($"Newsletter {DateTime.Now}", emailaddress);
            articles.ForEach(a =>
            {
                email.Text += t == OutputType.html ? $"{a.Header} by {a.Editor}<br>{a.Text}<br><br><hr>" 
                : email.Text += $"{a.Header} by {a.Editor}\n{a.Text}\n";
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
                Console.WriteLine("(G)enerate HTML-page");
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
                        PrintEmailToConsole();
                        break;
                    case "g":
                        CreateEmailHtmlPage();
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void PrintEmailToConsole()
        {
            var emails = CreateAllEmails(OutputType.terminal);
            emails.ForEach(e => {
                var sw = new StringWriter();
                e.ToString();
            });
            Console.ReadKey();
        }
        static void CreateEmailHtmlPage()
        {
            var emails = CreateAllEmails(OutputType.html);
            Console.WriteLine();
            using (var sw = new StreamWriter("../../../html/emails.html"))
            {
                using (HtmlTextWriter writer = new HtmlTextWriter(sw))
                {
                    writer.Write("<!doctype html>");
                    writer.RenderBeginTag(HtmlTextWriterTag.Html);
                    writer.RenderBeginTag(HtmlTextWriterTag.Head);
                    writer.Write("<meta charset = 'utf-8'>");
                    writer.Write("<link rel = 'stylesheet' href = 'style/style.css'>");
                    writer.RenderEndTag();
                    writer.RenderBeginTag(HtmlTextWriterTag.Body);
                    emails.ForEach(e =>
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Div);
                        writer.RenderBeginTag(HtmlTextWriterTag.H2);
                        writer.Write(e.Subject);
                        writer.RenderEndTag();
                        writer.RenderBeginTag(HtmlTextWriterTag.H3);
                        writer.Write($"mailto:  {e.Reciever}");
                        writer.RenderEndTag();
                        writer.RenderBeginTag(HtmlTextWriterTag.P);
                        writer.Write(e.Text);
                        writer.RenderEndTag();
                        writer.RenderEndTag();
                    });
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }

            }
            Console.WriteLine("Created -emails.html-");
            Console.ReadKey();
        }
    }
}
