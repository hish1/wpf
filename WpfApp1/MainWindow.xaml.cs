using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        static Hashtable ht;
        static Tr2 tr2;
        static Tr1 tr1;
        static List<Song> s;
        static List<Producer> pr;
        static string str1 = "a.txt";
        static string str2 = "b.txt";
        static int k = 0;
        static int t = 0;
        public MainWindow()
        {
            InitializeComponent();

            s = rSongFile(str1);
            pr = rProdFile(str2);
            ht = new Hashtable(pr);
            tr1 = new Tr1(s);
            tr2 = new Tr2(s);

            songGrid.ItemsSource = s;
            prodGrid.ItemsSource = pr;
        }

        // файлы
        static List<Song> rSongFile(string str)
        {
            using (StreamReader a = new StreamReader(str, System.Text.Encoding.UTF8))
            {
                var f = new FileInfo(str);
                if (f.Length > 1)
                {
                    int size = int.Parse(a.ReadLine());

                    List<Song> lines = new List<Song>();

                    int i = 0;
                    while (i < size)
                    {
                        string l = a.ReadLine();
                        string[] b = l.Split("  ");
                        lines.Add(new Song(b[0], b[1], b[2]));
                        i++;
                    }
                    return lines;
                }
                return null;
            }
        }
        static List<Producer> rProdFile(string str)
        {
            using (StreamReader b = new StreamReader(str, System.Text.Encoding.UTF8))
            {
                var f = new FileInfo(str);
                if (f.Length > 1)
                {
                    int size = int.Parse(b.ReadLine());

                    List<Producer> lines = new List<Producer>();

                    int i = 0;
                    while (i < size)
                    {
                        string l = b.ReadLine();
                        string[] a = l.Split("  ");
                        lines.Add(new Producer(a[0], a[1], int.Parse(a[2])));
                        i++;
                    }
                    return lines;
                }
                return null;
            }
        }
        static void resFile(List<Song> l)
        {
            using (StreamWriter a = new StreamWriter("c.txt", false))
            {
                for (int i = 0; i < l.Count; i++)
                    a.WriteLine(l[i].name + "  " + l[i].author + "  " + l[i].vocaloid);
            }
        }
        static void addSongFile(Song so)
        {
            using (StreamWriter a = new StreamWriter(str1, false))
            {
                s.Add(so);
                a.WriteLine(s.Count);
                for (int i = 0; i < s.Count; i++)
                    a.WriteLine(s[i].name + "  " + s[i].author + "  " + s[i].vocaloid);
            }
        }
        static void addProdFile(Producer p)
        {
            using (StreamWriter a = new StreamWriter(str2, false))
            {
                pr.Add(p);
                a.WriteLine(pr.Count);
                for (int i = 0; i < pr.Count; i++)
                    a.WriteLine(pr[i].name + "  " + pr[i].country + "  " + pr[i].data);
            }
        }
        static void delSongFile(Song so)
        {
            using (StreamWriter a = new StreamWriter(str1, false))
            {
                s.Remove(so);
                a.WriteLine(s.Count);
                for (int i = 0; i < s.Count; i++)
                    a.WriteLine(s[i].name + "  " + s[i].author + "  " + s[i].vocaloid);
            }
        }
        static void delProdFile(Producer p)
        {
            using (StreamWriter a = new StreamWriter(str2, false))
            {
                pr.Remove(p);
                a.WriteLine(pr.Count);
                for (int i = 0; i < pr.Count; i++)
                    a.WriteLine(pr[i].name + "  " + pr[i].country + "  " + pr[i].data);
            }
        }
        private void ofS_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog w = new Microsoft.Win32.OpenFileDialog();
           
            w.DefaultExt = ".txt";
            w.Filter = "Text documents (.txt)|*.txt";
            Nullable<bool> result = w.ShowDialog();

            if (result == true)
            {
                str1 = w.FileName;
                s = rSongFile(str1);
                songGrid.ItemsSource = s;
                if (s != null)
                    tr1 = new Tr1(s);
                else
                {
                    tr1 = new Tr1();
                    s = new List<Song>();
                }
            }
        }
        private void ofP_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog w = new Microsoft.Win32.OpenFileDialog();

            w.DefaultExt = ".txt";
            w.Filter = "Text documents (.txt)|*.txt";
            Nullable<bool> result = w.ShowDialog();

            if (result == true)
            {
                str2 = w.FileName;
                pr = rProdFile(str2);
                prodGrid.ItemsSource = pr;
                if (pr != null)
                {
                    ht = new Hashtable(pr);
                    str1 = "empty.txt";
                    File.Create(str1);
                    s = new List<Song>();
                    tr1 = new Tr1();
                    songGrid.ItemsSource = null;
                }
                else
                {
                    ht = new Hashtable();
                    pr = new List<Producer>();
                }
            }
        }

        // кнопки сонги
        private void addSong_Click(object sender, RoutedEventArgs e)
        {
            string a = songName.Text, b = authName.Text, c = vocName.Text;
            bool f1 = check(a), f2 = check(b), f3 = check(c);
            if (f1 && f2 && f3)
            {
                if (ht.find(new Producer(b, "", 0), 1, ref t) != -1)
                {
                    Song song = new Song(a, b, c);
                    if (!tr1.node.find(song, tr1.node.head))
                    {
                        bool f = true;
                        Un1 un1 = tr1.node.head;
                        Un2 un2 = tr2.node.head;
                        tr1.node.add(song, ref un1, ref f);
                        f = true;
                        tr2.node.add(song, ref un2, ref f);
                        addSongFile(song);
                        songGrid.ItemsSource = null;
                        songGrid.ItemsSource = s;
                    }
                    else MessageBox.Show("Song already exists.", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("This producer does not exist.", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Wrong format.", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);

            songName.Text = "";
            authName.Text = "";
            vocName.Text = "";
        }
        private void delSong_Click(object sender, RoutedEventArgs e)
        {
            string a = songName.Text, b = authName.Text, c = vocName.Text;
            bool f1 = check(a), f2 = check(b), f3 = check(c);
            if (f1 && f2 && f3)
            {
                Song song = new Song(a, b, c);
                if (tr1.node.find(song, tr1.node.head))
                {
                    bool f = true, fl = false;
                    tr1.node.delete(song, tr1.node.head, ref f, ref fl);
                    tr2.node.delete(song, tr2.node.head, ref f, ref fl);
                    delSongFile(song);
                    songGrid.ItemsSource = null;
                    songGrid.ItemsSource = s;
                }
                else MessageBox.Show("Song does not exist.", "Delete error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Wrong format.", "Delete error", MessageBoxButton.OK, MessageBoxImage.Error);

            songName.Text = "";
            authName.Text = "";
            vocName.Text = "";
        }
        private void searchSong_Click(object sender, RoutedEventArgs e)
        {
            string a = authName.Text;
            bool f = check(a);
            if (f)
            {
                k = 0;
                List<Song> res = new List<Song>();
                tr1.node.head.findProds(a, tr1.node.head, ref res, ref k);

                if (res.Count != 0)
                {
                    Wind s = new Wind(res, k);
                    s.Owner = this;
                    s.Show();
                }
                else MessageBox.Show("Songs by this producer do not exist.", "Search error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Wrong format.", "Search error", MessageBoxButton.OK, MessageBoxImage.Error);


            songName.Text = "";
            authName.Text = "";
            vocName.Text = "";
        }
        private void showS_Click(object sender, RoutedEventArgs e)
        {
            Wind s = new Wind(tr1);
            s.Owner = this;
            s.Show();
        }

        // кнопки проды
        private void addProd_Click(object sender, RoutedEventArgs e)
        {
            string a = prodName.Text, b = prodCountry.Text, c = prodDate.Text;
            bool f1 = check(a), f2 = check(b), f3 = check(c);
            if (f1 && f2 && f3)
            {
                int cc = int.Parse(c);
                if (cc >= 2004 && cc <= 2022)
                {
                    Producer prod = new Producer(a, b, cc);
                    if (ht.find(prod, 1, ref t) == -1)
                    {
                        ht.add(prod);
                        addProdFile(prod);
                        prodGrid.ItemsSource = null;
                        prodGrid.ItemsSource = pr;
                    }
                    else MessageBox.Show("Producer already exists.", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("Wrong format", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Wrong format", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);

            prodName.Text = "";
            prodCountry.Text = "";
            prodDate.Text = "";
        }
        private void delProd_Click(object sender, RoutedEventArgs e)
        {
            string a = prodName.Text, b = prodCountry.Text, c = prodDate.Text; 
            bool f1 = check(a), f2 = check(b), f3 = check(c);
            if (f1 && f2 && f3)
            {
                int cc = int.Parse(c);
                if (cc >= 2004 && cc <= 2022)
                {
                    Producer prod = new Producer(a, b, cc);
                    if (ht.find(prod, 0, ref t) != -1)
                    {
                        ht.del(prod);
                        delProdFile(prod);
                        prodGrid.ItemsSource = null;
                        prodGrid.ItemsSource = pr;

                        List<List<Song>> show = new List<List<Song>>();
                        if (tr1.node.head != null)
                        {
                            tr1.node.head.show(tr1.node.head, ref show);
                            for (int i = 0; i < show.Count; i++)
                                if (show[i] != null)
                                {
                                    if (show[i][0].author == a)
                                    {
                                        for (int j = show[i].Count - 1; j >= 0; j--)
                                        {
                                            delSongFile(show[i][j]);
                                            bool f = true, fl = false;
                                            tr2.node.delete(show[i][j], tr2.node.head, ref f, ref fl);
                                            f = true;
                                            fl = false;
                                            tr1.node.delete(show[i][j], tr1.node.head, ref f, ref fl);
                                        }
                                    }
                                }
                        }
                        songGrid.ItemsSource = null;
                        songGrid.ItemsSource = s;
                            
                    }
                    else MessageBox.Show("Producer does not exist.", "Delete error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("Wrong format", "Delete error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Wrong format", "Delete error", MessageBoxButton.OK, MessageBoxImage.Error);
            prodName.Text = "";
            prodCountry.Text = "";
            prodDate.Text = "";
        }
        private void searchProd_Click(object sender, RoutedEventArgs e)
        {
            string a = prodName.Text;
            bool f = check(a);
            if (f)
            {
                t = 1;
                int key = ht.find(new Producer(a, "", 0), 1, ref t);
                if (key != -1)
                {
                    Wind w = new Wind(ht.table[key].node, t);
                    w.Owner = this;
                    w.Show();
                }
                else MessageBox.Show("This producer does not exist.", "Search error", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
            else MessageBox.Show("Wrong format.", "Search error", MessageBoxButton.OK, MessageBoxImage.Error);

            prodCountry.Text = "";
            prodName.Text = "";
            prodDate.Text = "";
        }
        private void showP_Click(object sender, RoutedEventArgs e)
        {
            Wind p = new Wind(ht);
            p.Owner = this;
            p.Show();
        }

        // кнопки серчи
        private void search_Click(object sender, RoutedEventArgs e)
        {
            string v = vocSearch.Text, from = dateFrom.Text, to = dateTo.Text;
            bool f1 = check(v), f2 = check(from), f3 = check(to);
            if (f1 && f2 && f3)
            {
                int f = int.Parse(from), t = int.Parse(to);
                if (f >= 2004 && f <= 2022 && t >= 2004 && t <= 2022)
                {
                    k = 0;
                    List<Song> songs = new List<Song>();
                    tr2.node.findVocs(v, tr2.node.head, ref songs, ref k);
                    if (songs.Count != 0)
                    {
                        List<Song> res = ht.findSongs(songs, f, t, ref t);
                        if (res.Count != 0)
                        {
                            searchGrid.ItemsSource = null;
                            searchGrid.ItemsSource = res;
                            resFile(res);
                        }
                        else MessageBox.Show("Producers in this date range do not exist.", "Search error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else MessageBox.Show("Songs with this vocaloid do not exist.", "Search error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("Wrong format", "Search error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Wrong format", "Search error", MessageBoxButton.OK, MessageBoxImage.Error);

            vocSearch.Text = "";
            dateFrom.Text = "";
            dateTo.Text = "";
        }
        private void show_Click(object sender, RoutedEventArgs e)
        {
            Wind p = new Wind(tr2, k);
            p.Owner = this;
            p.Show();
        }

        bool check(string s)
        {
            if (s != "" && s[0] != ' ')
            {
                for (int i = 0; i < s.Length; i++)
                    if (!((s[i] >= 'a' && s[i] <= 'z') || (s[i] >= 'A' && s[i] <= 'Z') || s[i] == ' ' || s[i] == '!' || s[i] == '.' || s[i] == ',' || s[i] == '?' || s[i] == '-' || s[i] == '_' || char.IsDigit(s[i])))
                        return false;
                return true;
            }
            else return false;
        }
    }
}
