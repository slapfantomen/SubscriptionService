using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService
{
    public static class DBHandler
    {
        static string connstr = "Server = (localdb)\\mssqllocaldb; Database = News website";

        public static void QueryDb(string sql)
        {
            using (var con = new SqlConnection(connstr))
            {
                using (var com = new SqlCommand(sql, con))
                {
                    con.Open();
                    var reader = com.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No results found in the database");
                    }

                    while (reader.Read())
                    {
                        int count = reader.FieldCount;
                        for (int i = 0; i < count; i++)
                        {
                            Console.Write(reader.GetValue(i) + " ");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
        public static List<User> CreateUserList()
        {
            List<User> list = new List<User>();
            var sql = @"select u.user_id, u.first_name, u.last_name, e.email_address"+
                        " from email e"+
                        " join users u on u.email_id = e.email_id ";
            using (var con = new SqlConnection(connstr))
            {
                using (var com = new SqlCommand(sql, con))
                {
                    con.Open();
                    var reader = com.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No users found in the database");
                    }

                    while (reader.Read())
                    {
                        int userId = int.Parse(reader.GetValue(0).ToString());
                        string firstName = reader.GetValue(1).ToString();
                        string lastName = reader.GetValue(2).ToString();
                        string email = reader.GetValue(3).ToString();
                        list.Add(new User(userId, firstName, lastName, email));
                    }
                }
            }
            return list;
        }

        public static List<Article> GetArticlesForUser(int userId)
        {
            List<Article> list = new List<Article>();
            var sql = @"select article_header, article.article_text, editors.first_name+' '+editors.last_name as Editor" +
                        " from users" +
                        " join subscription_list on subscription_list.user_id=users.user_id" +
                        " join subscribe_details on subscribe_details.subscribe_list_id=subscription_list.list_id" +
                        " join subcategory on subcategory.subcategory_id=subscribe_details.subcategory_id" +
                        " join article on article.subcategory_id=subcategory.subcategory_id" +
                        " join editors on editors.editor_id=article.editor_id" +
                        " where users.user_id=" +userId;
            using (var con = new SqlConnection(connstr))
            {
                using (var com = new SqlCommand(sql, con))
                {
                    con.Open();
                    var reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string header = reader.GetValue(0).ToString();
                            string text = reader.GetValue(1).ToString();
                            string editor = reader.GetValue(2).ToString();
                            list.Add(new Article(header, text, editor));
                        }
                    }
                }
            }
            return list;
        } 
    }
}
