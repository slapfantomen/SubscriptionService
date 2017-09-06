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
                        Console.WriteLine("Nothing right now");
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
            var sql = "execute GetAllUsers";
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
            var sql = "execute GetUserArticles "+userId;
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
