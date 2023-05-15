using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
    public class Song 
    {
        public string name { get; set; }
        public string author { get; set; }
        public string vocaloid { get; set; }
        public Song(string n, string a, string v)
        {
            name = n;
            author = a;
            vocaloid = v;
        }
        public Song()
        { }
        public string all()
        {
            return name + " " + author + " " + vocaloid;
        }

        public static bool operator ==(Song a, Song b)
        {
            if (a.name == b.name && a.author == b.author && a.vocaloid == b.vocaloid)
                return true;
            else return false;
        }
        public static bool operator !=(Song a, Song b)
        {
            if (a.name == b.name && a.author == b.author && a.vocaloid == b.vocaloid)
                return false;
            else return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Song)) return false;
            var song = obj as Song;
            return this == song;
        }
    }
}
