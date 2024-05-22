using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranBingo_Generator
{
    public class Header
    {     
        public string Title { get; }
        public string Org { get; }
        public string Adress { get; }
        public string Descrip { get; }

        private float cost;
        private DateTime time;

        public Header(string title, string org, string adress, DateTime date, float price, string descrip)
        {
            Title = title;
            Org = org;
            Adress = adress;
            time = date;
            cost = price;
            Descrip = descrip;
        }

        public string Cost()
        {
            if (cost < 0)
                cost = 0.10f;

            return cost.ToString("C");
        }

        public string DateHour()
        {
            return time.ToLongDateString().ToUpper() + " - " + string.Format("{0:hh:mm:ss tt}", time).ToUpper();
        }
    }
}
