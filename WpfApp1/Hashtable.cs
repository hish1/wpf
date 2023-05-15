using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
    public class Node
    {
        public Producer node;
        public int state;
        public int h1, h2;
        public Node() { }
        public Node(Producer n, int s, int k)
        {
            node = n;
            state = s;
            h1 = k;
            h2 = h1;
        }
        public Node(Producer n, int s, int k1, int k2)
        {
            node = n;
            state = s;
            h1 = k1;
            h2 = k2;
        }
    }
    public class Hashtable
    {
        static int size;
        int minSize = 1;
        int filled;
        int k;
        public Node[] table;
        public Hashtable() 
        {
            size = minSize;
            table = new Node[size];
            checkSize();
        }
        public Hashtable(List<Producer> s)
        {
            size = s.Count;
            k = findK();
            table = new Node[size];
            for (int i = 0; i < s.Count; i++)
                add(s[i]);
        }

        // хэш функции
        public int hash1(Producer prod)
        {
            string s = prod.name;
            //int key = 0;
            //for (int i = 0; i < s.Length; i++)
            //    key += s[i];
            //return key % size;

            int a = 67;
            int key = 0;
            for (int i = 0; i < s.Length; i++)
            {
                var d = s[i] - 'a';
                key = (key * a + s[i] - 'a') % size;
            }
            return Math.Abs(key);
        }
        public int hash2(int key, int j)
        {
            return (key + j * k) % size;
        }

        // добавление
        public void add(Producer prod)
        {
            int key = hash1(prod);
            if (table[key] == null)
            {
                table[key] = new Node(prod, 1, key);
                filled++;
                checkSize();
            }
            else if (table[key].state == 1 && table[key].node.name == prod.name)
                Console.WriteLine("song already exists");
            else addColl(prod);
        }
        void addColl(Producer prod)
        {
            int t = 0;
            int key = findColl(prod, 1, ref t);
            if (key == -1)
            {
                int j = 1;
                key = hash2(hash1(prod), j);

                int s = key;
                while (table[key] != null && table[key].state == 1 && table[key].node.name != prod.name && j <= size)
                {
                    j++;
                    key = hash2(hash1(prod), j);

                    s = key;
                    if (s == size)
                    {
                        s = 0;
                        key = 0;
                    }
                }

                if (table[key] == null)
                {
                    filled++;
                    table[key] = new Node(prod, 1, hash1(prod), key);
                    checkSize();
                }
                else if (table[key].state == 1 && table[key].node.name == prod.name)
                {
                    Console.WriteLine("song already exists");
                }
                else if (j == size)
                    Console.WriteLine("table is full");
                else
                {
                    filled++;
                    table[key] = new Node(prod, 1, hash1(prod), key);
                    checkSize();
                }
            }
            else Console.WriteLine("key already exists");
        }

        // удаление
        public void del(Producer prod)
        {
            if (table != null)
            {
                int key = hash1(prod);
                if (table[key] == null)
                    Console.WriteLine("key does not exist");
                else if (table[key].state != 2 && table[key].node.name == prod.name)
                {
                    filled--;
                    table[key].state = 2;
                    table[key].h1 = 0;
                    checkSize();
                }
                else if (table[key].node.name != prod.name)
                    delColl(prod);
                else Console.WriteLine("key does not exist");
            }
            else Console.WriteLine("table is empty");
        }
        void delColl(Producer prod)
        {
            int t = 0;
            int key = findColl(prod, 1, ref t);
            if (key == -1)
                Console.WriteLine("key does not exist");
            else
            {
                filled--;
                table[key].state = 2;
                checkSize();
                table[hash1(prod)].h1 = 0;
                table[key].h2 = 0;
            }
        }

        //динамика
        void checkSize()
        {
            if ((double)filled / (double)size >= 0.7)
                changeSize(size * 2);
            else if ((double)filled / (double)size <= 0.1 && size > minSize)
                changeSize(size / 2);
        }
        void changeSize(int s)
        {
            size = s;
            k = findK();
            Node[] nodes = new Node[s];

            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null && table[i].state == 1)
                {
                    int c = 0;
                    int hash = hash2(hash1(table[i].node), c);
                    int h = hash1(table[i].node);
                    while (nodes[hash] != null && nodes[hash].state == 1)
                    {
                        c++;
                        hash = hash2(hash1(table[i].node), c);
                    }
                    if (hash == h)
                        nodes[hash] = new Node(table[i].node, table[i].state, h, h);
                    else
                        nodes[hash] = new Node(table[i].node, table[i].state, h, hash);
                }
            }
            table = nodes;
            GC.Collect();
        }
        int findK()
        {
            for (int i = 2; i < size; i++)
            {
                bool found = size % i != 0;
                for (int j = 2; j < i; j++)
                    if (i % j == 0 && size % j == 0)
                        found = false;
                if (found)
                    return i;
            }
            return 1;
        }

        //поиск
        public int find(Producer prod, int f, ref int c)
        {
            int key = hash1(prod);
            if (table[key] != null && table[key].node.name == prod.name)
                if (f == 1)
                    return key;
                else
                {
                    if (table[key].node == prod)
                        return key;
                    else return -1;
                }
            else if (table[key] == null)
                return -1;
            else return findColl(prod, f, ref c);
        }
        int findColl(Producer prod, int f, ref int c)
        {
            int i = 0, key = 0;
            for (i = 1; i < size; i++)
            {
                key = hash2(hash1(prod), i);
                c++;
                if (table[key] == null)
                    break;
                if (table[key].node.name == prod.name)
                    break;
            }

            if (table[key] == null)
                return -1;
            else if (table[key].state == 1 && table[key].node.name == prod.name)
                if (f == 1)
                    return key;
                else
                {
                    if (table[key].node == prod)
                        return key;
                    else return -1;
                }
            else return -1;
        }
        public List<Song> findSongs(List<Song> s, int f, int t, ref int k)
        {
            List<Song> res = new List<Song>();

            for (int i = 0; i < s.Count; i++)
            {
                int key = find(new Producer(s[i].author, "", 0), 1, ref k);
                if (key != -1)
                {
                    if (table[key].node.data >= f && table[key].node.data <= t)
                        res.Add(s[i]);
                }
            }
            return res;
        }

        public List<string> print()
        {
            if (table != null)
            {
                List<string> res = new List<string>();
                for (int i = 0; i < size; i++)
                {
                    string a = "";
                    if (table[i] != null && table[i].state == 1)
                    {
                        a = i + ": state = 1, hash1 = ";
                        a += table[i].h1 + ", hash2 = " + table[i].h2 + ", ";
                        a += table[i].node.all();
                    }
                    else if (table[i] != null && table[i].state == 2)
                        a = i + ": state = 2";
                    else
                        a = i + ": state = 0";
                    res.Add(a);
                }
                return res;
            }
            else return null;
        }
    }
}
