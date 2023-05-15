using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Songs.xaml
    /// </summary>
    public partial class Wind : Window
    {
        public Wind(Tr1 tr)
        {
            InitializeComponent();
            text.Text = "Songs by producer";
            grid.Visibility = Visibility.Collapsed;
            list.Visibility = Visibility.Visible;
            show(tr.node.head, 5);
        }
        public Wind(Tr2 tr, int k)
        {
            InitializeComponent();
            text.Text = $"Songs found by vocaloid by {k} comparisons and by date range";
            grid.Visibility = Visibility.Collapsed;
            list.Visibility = Visibility.Visible;
            show(tr.node.head, 5);
        }
        public Wind(List<Song> s, int k)
        {
            InitializeComponent();
            text.Text = $"Songs found by {k} comparisons";
            grid.ItemsSource = s;
        }
        public Wind(Producer s, int k)
        {
            InitializeComponent();
            text.Text = $"Producers found by {k} comparisons";
            grid.Visibility = Visibility.Collapsed;
            list.Visibility = Visibility.Visible;
            list.Items.Add(s.all());
        }
        public Wind(Hashtable ht)
        {
            InitializeComponent();
            text.Text = "Producers";
            List<string> res = ht.print();
            grid.Visibility = Visibility.Collapsed;
            list.Visibility = Visibility.Visible;
            for (int i = 0; i < res.Count; i++)
                list.Items.Add(res[i]);
        }

        public void show(Un1 u, int s)
        {
            if ((u != null))
            {
                show(u.right, s + 15);

                string tabs = "";
                for (int i = 1; i <= s; i++)
                    tabs += " ";

                list.Items.Add(tabs + $"{u.l.head.song.all()}({u.l.size()})");

                show(u.left, s + 15);
            }
        }
        public void show(Un2 u, int s)
        {
            if ((u != null))
            {
                show(u.right, s + 15);

                string tabs = "";
                for (int i = 1; i <= s; i++)
                    tabs += " ";

                list.Items.Add(tabs + $"{u.l.head.song.all()}({u.l.size()})");

                show(u.left, s + 15);
            }
        }
    }
}
