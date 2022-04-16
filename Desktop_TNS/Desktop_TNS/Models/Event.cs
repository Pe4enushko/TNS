using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Desktop_TNS.Models
{
    public class Event
    {
        string date = null;
        string time = null;
        string content = null;

        public string Date { get => date ?? "Когда-нибудь"; set => date = value; }
        public string Time { get => time ?? "В течение дня"; set => time = value; }
        public string Content { get => content ?? "Ничего"; set => content = value; }
        public Event(string notFormattedString)
        {
            Date = new Regex("[\\d]? [\\W]*").Match(notFormattedString).Value;
            Time = new Regex("[\\d]{2}.[\\d]{2}").Match(notFormattedString).Value;
            Content = notFormattedString.Substring(Time.Length + Content.Length);
        }
        public Event(string date, string time, string content)
        {
            Date = date;
            Time = time;
            Content = content;
        }
    }
}
