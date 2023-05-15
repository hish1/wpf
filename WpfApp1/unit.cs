using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
    public class unit
    {
        public Song song;
        public unit next;
		public unit() { }
		public unit(Song s)
        {
			song = s;
			next = null;
		}
    }
	public class list
	{
		public unit head;
		public list()
		{
			head = null;
		}
		public bool isEmpty()
		{
			if (head == null)
				return true;
			else return false;
		}
		public void add(Song s)
		{
			unit l = new unit(s);
			if (isEmpty())
				head = l;
			else
			{
				unit a = head;
				while (a.next != null)
					a = a.next;
				a.next = l;
			}
		}
		public void del(Song s)
		{
			if (isEmpty())
			{ 
			}
			else
			{
				if (head.song == s)
					head = head.next;
				else
				{
					unit l = head;
					while (l.next != null)
					{
						if (l.next.song == s)
						{
							unit a = l.next;
							l.next = a.next;
							break;
						}
						l = l.next;
					}
				}
				GC.Collect();
			}
		}
		public bool find(Song s)
		{
			if (isEmpty())
				return false;
			else
			{
				unit l = head;
				while ((l.song != s) && (l.next != null))
					l = l.next;

				if (l.song == s)
					return true;
				else return false;
			}
		}
		public int size()
		{
			if (isEmpty())
				return 0;
			else
			{
				int s = 1;
				unit l = head;
				while (l.next != null)
				{
					l = l.next;
					s++;
				}
				return s;
			}
		}
		public List<Song> songs()
        {
			List<Song> res = new List<Song>();

			if (isEmpty())
				return null;
			else
            {
				unit a = head;
				while (a.next!=null)
                {
					res.Add(a.song);
					a = a.next;
                }
				res.Add(a.song);
				return res;
            }
        }
	}
}
