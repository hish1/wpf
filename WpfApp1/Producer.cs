using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
    public class Producer
    {
        public string name { get; set; }
        public string country { get; set; }
        public int data { get; set; }
        public Producer() { }
        public Producer(string n, string c, int d)
        {
            name = n;
            country = c;
            data = d;
        }
        public string all()
        {
            return name + " " + country + " " + data;
        }

        public static bool operator ==(Producer a, Producer b)
        {
            if (a.name == b.name && a.country == b.country && a.data == b.data)
                return true;
            else return false;
        }
        public static bool operator !=(Producer a, Producer b)
        {
            if (a.name == b.name && a.country == b.country && a.data == b.data)
                return false;
            else return true;
        }

        public override bool Equals(object obj)
        {   
            if (!(obj is Producer)) return false;
            var pr = obj as Producer;
            return this == pr;
        }
    }
}
