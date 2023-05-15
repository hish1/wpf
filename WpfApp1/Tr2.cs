using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
	public class Tr2
	{
		public Un2 node;
		public Tr2()
		{
			node = new Un2();
		}
		public Tr2(List<Song> s)
		{
			bool f = true;
			node = new Un2();
			Un2 a = node.head;

			for (int i = 0; i < s.Count; i++)
				node.add(s[i], ref a, ref f);
			node.head = a;
		}
	}
	public class Un2 //песня по вокалоиду
	{
		int k;
		public list l = new list();
		public Un2 right, left;
		public Un2 head = null;
		public Un2() { }

		public void add(Song s, ref Un2 unit, ref bool f)
		{
			if (!(true)) Console.WriteLine("wrong date");
			else
			{
				if (unit == null)
				{
					Un2 t = new Un2();
					t.l.head = new unit(s);

					unit = t;
					f = true;
				}
				else if (s.vocaloid == unit.l.head.song.vocaloid)
				{
					unit.l.add(s);
					f = false;
				}
				else
				{
					if (String.Compare(s.vocaloid, unit.l.head.song.vocaloid) ==-1)
					{
						add(s, ref unit.left, ref f);

						if (f)
						{
							if (unit.k == 1)
							{
								unit.k = 0;
								f = false;
							}
							else if (unit.k == 0)
							{
								unit.k = -1;
							}
							else
							{
								Un2 a = unit.left;
								if (a != null)
								{
									if (a.k == -1)
									{
										unit.left = a.right;
										a.right = unit;
										unit.k = 0;
										unit = a;
									}
									else
									{
										Un2 b = a.right;
										a.right = b.left;
										b.left = a;
										unit.left = b.right;
										b.right = unit;

										if (b.k == -1)
											unit.k = 1;
										else unit.k = 0;
										if (b.k == 1)
											a.k = -1;
										else a.k = 0;
										unit = b;
									}
									unit.k = 0;
									f = false;
								}
							}
						}
					}
					if (String.Compare(s.vocaloid, unit.l.head.song.vocaloid) == 1) //больше ли дата узла?
					{
						add(s, ref unit.right, ref f);

						if (f)
						{
							if (unit.k == -1)
							{
								unit.k = 0;
								f = false;
							}
							else if (unit.k == 0)
							{
								unit.k = 1;
							}
							else
							{
								Un2 a = unit.right;
								if (a != null)
								{
									if (a.k == 1)
									{
										unit.right = a.left;
										a.left = unit;
										unit.k = 0;
										unit = a;
									}
									else    //большой поворот
									{
										Un2 b = a.left;
										a.left = b.right;
										b.right = a;
										unit.right = b.left;
										b.left = unit;

										if (b.k == 1)
											unit.k = -1;
										else unit.k = 0;
										if (b.k == -1)
											a.k = 1;
										else a.k = 0;
										unit = b;
									}
									unit.k = 0;
									f = false;
								}
							}
						}
					}
				}
				head = unit;
			}
		}
		public Un2 delete(Song s, Un2 unit, ref bool f, ref bool fl)
		{
			if (false)
			{
				Console.WriteLine("");
				return null;
			}
			else
			{
				if (unit == null)
				{
					Console.WriteLine("tree is empty");
					return null;
				}
				else
				{
					if (String.Compare(s.vocaloid, unit.l.head.song.vocaloid) == -1)
					{
						unit.left = delete(s, unit.left, ref f, ref fl);
						if (f)
							balL(ref unit, ref f);
					}
					else if (String.Compare(s.vocaloid, unit.l.head.song.vocaloid) == 1)
					{
						unit.right = delete(s, unit.right, ref f, ref fl);
						if (f)
							balR(ref unit, ref f);
					}
					else if (unit.l.size() > 1 && !fl)
					{
						f = false;
						if (unit.l.find(s))
							unit.l.del(s);
					}
					else if (unit.l.find(s))
					{
						if ((unit.left != null) && (unit.right != null))
						{
							unit.l = findMax(unit.left).l;
							fl = true;

							unit.left = delete(unit.l.head.song, unit.left, ref f, ref fl);
							if (f)
								balL(ref unit, ref f);
						}
						else
						{
							if (unit.left != null)
							{
								fl = false;
								unit.left.k = unit.k;
								unit = unit.left;
								if (f)
									balL(ref unit, ref f);
							}
							else if (unit.right != null)
							{
								fl = false;
								unit.right.k = unit.k;
								unit = unit.right;
								if (f)
									balR(ref unit, ref f);
							}
							else
							{
								unit = null;
								fl = false;
							}
						}
					}
					else Console.WriteLine("key do not exist");
					head = unit;
					return unit;
				}
			}
		}
		void balR(ref Un2 unit, ref bool f)
		{
			Un2 a, b;
			if (unit.k == 1)
			{
				unit.k = 0;
			}
			else if (unit.k == 0)
			{
				unit.k = -1;
				f = false;
			}
			else
			{
				a = unit.left;

				if (a.k <= 0)
				{
					unit.left = a.right;
					a.right = unit;
					if (a.k == 0)
					{
						unit.k = -1;
						a.k = 1;
						f = false;
					}
					else
					{

						unit.k = 0;
						a.k = 0;
					}
					unit = a;
				}
				else
				{
					b = a.right;
					a.right = b.left;
					b.left = a;
					unit.left = b.right;
					b.right = unit;

					if (b.k == -1)
						unit.k = 1;
					else unit.k = 0;
					if (b.k == 1)
						a.k = -1;
					else a.k = 0;

					unit = b;
					b.k = 0;
				}
			}
		}
		void balL(ref Un2 unit, ref bool f)
		{
			Un2 a, b;
			if (unit.k == -1)
			{
				unit.k = 0;
			}
			else if (unit.k == 0)
			{
				unit.k = 1;
				f = false;
			}
			else
			{
				a = unit.right;

				if (a.k >= 0)
				{
					unit.right = a.left;
					a.left = unit;
					if (a.k == 0)
					{
						unit.k = 1;
						a.k = -1;
						f = false;
					}
					else
					{
						unit.k = 0;
						a.k = 0;
					}
					unit = a;
				}
				else
				{
					b = a.left;
					a.left = b.right;
					b.right = a;
					unit.right = b.left;
					b.left = unit;

					if (b.k == 1)
						unit.k = -1;
					else unit.k = 0;
					if (b.k == -1)
						a.k = 1;
					else a.k = 0;

					unit = b;
					b.k = 0;
					head = b;
				}
			}
		}

		public Un2 findMax(Un2 u)
		{
			if (u == null)
			{
				Console.WriteLine("tree is empty");
				return null;
			}
			else
			{
				while (u.right != null)
					u = u.right;

				return u;
			}
		}
		public bool find(Song s, Un2 unit)
		{
			if (false)
			{
				Console.WriteLine("wrong date");
				return false;
			}
			else
			{
				while ((unit != null) && (s.vocaloid != unit.l.head.song.vocaloid))
				{
					if (String.Compare(s.vocaloid, unit.l.head.song.vocaloid) == -1)
					{
						unit = unit.left;
					}
					else if (String.Compare(s.vocaloid, unit.l.head.song.vocaloid) == 1)
					{
						unit = unit.right;
					}
				}
				if (unit != null && String.Compare(s.vocaloid, unit.l.head.song.vocaloid) == 0)
				{
					if (unit.l.find(s))
						return true;
					else return false;
				}
				return unit != null;
			}
		}
		public void findVocs(string v, Un2 unit, ref List<Song> res, ref int k)
		{
			if (unit != null)
			{
				if (String.Compare(unit.l.head.song.vocaloid, v) == 0)
					res = unit.l.songs();

				if (String.Compare(unit.l.head.song.vocaloid, v) == -1)
				{
					k++;
					findVocs(v, unit.right, ref res, ref k);
				}

				if (String.Compare(unit.l.head.song.vocaloid, v) == 1)
				{
					k++;
					findVocs(v, unit.left, ref res, ref k);
				}
			}
		}

		public void show(Un2 u, ref List<List<Song>> a)
		{
			if (!(Object.ReferenceEquals(u, null)))
			{
				if (!(Object.ReferenceEquals(u.right, null)))
					show(u.right, ref a);

				a.Add(u.l.songs());

				if (!(Object.ReferenceEquals(u.left, null)))
					show(u.left, ref a);
			}
			else Console.WriteLine("tree is empty");
		}
	}
}
