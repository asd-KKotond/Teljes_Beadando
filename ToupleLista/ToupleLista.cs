using System;
using System.Collections.Generic;
using System.IO;

namespace ToupleLista
{
		internal class ToupleLista
		{
				static void Main(string[] args) // "Z:\\szabo zoli\\c#\\datas\\bigLista.csv"
				{                               // auto,márka,típus,évjárat,ár
						Console.WriteLine("=============================== Touple lista megoldás ===============================");

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

						List<(string rendszam, string marka, string tipus, ushort evj, uint ar)> autok
							= new List<(string, string, string, ushort, uint)>();

						ulong osszar = 0; // ossz ár

						string sor = sr.ReadLine();

						while ((sor = sr.ReadLine()) != null)
						{
								string[] datas = sor.Split(',');

								autok.Add((
									datas[0],
									datas[1],
									datas[2],
									Convert.ToUInt16(datas[3]),
									Convert.ToUInt32(datas[4])
								));

								osszar += Convert.ToUInt64(datas[4]);

								;
						}

						sr.Close();
						fs.Close();

						// 2. feladat: Átlagár 
						Console.WriteLine($"2. feladat\n\tÁtlagár: {osszar / Convert.ToUInt64(autok.Count)}");

						// 3. feladat: Legdrágább autó
						int arindex = 0;
						uint maxar = 0;

						for (int i = 0; i < autok.Count; i++)
						{
								if (autok[i].ar > maxar)
								{
										maxar = autok[i].ar;
										arindex = i;
								}
						}

						Console.WriteLine(
							$"\n3. feladat\n\tA legdrágább autó adatai: " +
							$"\n\t\t{autok[arindex].rendszam}, {autok[arindex].marka}, {autok[arindex].tipus}, {autok[arindex].evj}, {autok[arindex].ar}"
						);

						// 4. feladat: Márkák darabszáma
						Dictionary<string, int> van = new Dictionary<string, int>();

						for (int i = 0; i < autok.Count; i++)
						{
								if (van.ContainsKey(autok[i].marka))
								{
										van[autok[i].marka]++;
								}
								else
								{
										van.Add(autok[i].marka, 1);
								}
						}

						Console.WriteLine($"\n4. feladat\n\tautók és darabszámuk:");

						string maxdarab = "";
						int maxdarabszam = 0;

						foreach (var line in van)
						{
								Console.WriteLine($"\t\t{line}");

								if (line.Value > maxdarabszam)
								{
										maxdarabszam = line.Value;
										maxdarab = line.Key;
								}
						}

						// 5. feladat: Legtöbb autó márka
						Console.WriteLine($"\n5. feladat\n\tAz {maxdarab} márkából van a legtöbb.");

						// 6. feladat: Legöregebb autó
						int ev = 2026;
						int evindex = 0;

						for (int i = 0; i < autok.Count; i++)
						{
								if (autok[i].evj < ev)
								{
										ev = autok[i].evj;
										evindex = i;
								}
						}

						Console.WriteLine(
							$"\n6. feladat\n\tA legöregebb autó adatai:" +
							$"\n\t\t{autok[evindex].rendszam}, {autok[evindex].marka}, {autok[evindex].tipus}, {autok[evindex].evj}, {autok[evindex].ar}"
						);

						Console.ForegroundColor = ConsoleColor.DarkMagenta;
						Console.WriteLine("\n\n\nKódolta: Pekár-Héder Botond, Farkas Bence, Zsitva Dóra");
						Console.ForegroundColor = ConsoleColor.White;
				}
		}
}
