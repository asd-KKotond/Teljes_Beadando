using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimaLista_Linq
{
		internal class SimaLista_Linq
		{
				static void Main(string[] args)
				{
						Console.WriteLine("=============================== Sima lista + Linq megoldás ===============================");

						string mydocu = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "datas");

						string filepath = Path.Combine(mydocu, "bigLista.csv");

						if (!File.Exists(filepath))
						{
								Console.WriteLine($"{filepath} nem létezik!");
								return;
						}

						FileStream fs = new FileStream(
							filepath,
							FileMode.Open,
							FileAccess.Read,
							FileShare.Read
						);

						StreamReader sr = new StreamReader(fs);

						List<string> rendszam = new List<string>();
						List<string> marka = new List<string>();
						List<string> tipus = new List<string>();
						List<ushort> evj = new List<ushort>();
						List<uint> ar = new List<uint>();

						sr.ReadLine(); // fejléc kihagyása

						string sor;
						while ((sor = sr.ReadLine()) != null)
						{
								string[] datas = sor.Split(',');

								rendszam.Add(datas[0]);
								marka.Add(datas[1]);
								tipus.Add(datas[2]);
								evj.Add(Convert.ToUInt16(datas[3]));
								ar.Add(Convert.ToUInt32(datas[4]));
						}

						sr.Close();
						fs.Close();

						// 2. feladat: Átlagár
						double atlagAr = ar.Average(x => x);
						Console.WriteLine($"2. feladat\n\tÁtlagár: {atlagAr:F0}");

						// 3. feladat: Legdrágább autó						
						uint maxAr = ar.Max();
						int arindex = ar.IndexOf(maxAr);

						Console.WriteLine(
							$"\n3. feladat\n\tA legdrágább autó adatai:" +
							$"\n\t\t{rendszam[arindex]}, {marka[arindex]}, {tipus[arindex]}, {evj[arindex]}, {ar[arindex]}"
						);

						// 4. feladat: Autók darabszáma márkánként
						var markaDarab = marka
							.GroupBy(x => x)
							.Select(g => new { Marka = g.Key, Db = g.Count() });

						Console.WriteLine("\n4. feladat\n\tautók és darabszámuk:");

						foreach (var m in markaDarab)
						{
								Console.WriteLine($"\t\t{m.Marka}: {m.Db} db");
						}

						// 5. feladat: Legtöbb autóval rendelkező márka
						var legtobb = markaDarab
							.OrderByDescending(x => x.Db)
							.First();

						Console.WriteLine($"\n5. feladat\n\tAz {legtobb.Marka} márkából van a legtöbb.");

						// 6. feladat: Legöregebb autó
						ushort minEv = evj.Min();
						int evindex = evj.IndexOf(minEv);

						Console.WriteLine(
							$"\n6. feladat\n\tA legöregebb autó adatai:" +
							$"\n\t\t{rendszam[evindex]}, {marka[evindex]}, {tipus[evindex]}, {evj[evindex]}, {ar[evindex]}"
						);

						Console.ForegroundColor = ConsoleColor.DarkMagenta;
						Console.WriteLine("\n\n\nKódolta: Pekár-Héder Botond, Farkas Bence, Zsitva Dóra");
						Console.ForegroundColor = ConsoleColor.White;
				}
		}
}