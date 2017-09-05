using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService
{
    public class Article
    {
        public string Header { get; set; }
        public string Text { get; set; }
        public string Editor { get; set; }

        public Article(string header, string text, string editor)
        {
            Header = header;
            Text = text;
            Editor = editor;
        }
    }
}
