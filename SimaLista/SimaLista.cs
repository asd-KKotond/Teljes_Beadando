using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SimaLista
{
		internal class SimaLista
		{
				static void Main(string[] args)         // "Z:\\szabo zoli\\c#\\datas\\bigLista.csv"
				{                                       // auto,márka,típus,évjárat,ár
						Console.WriteLine("=============================== Sima lista megoldás ===============================");

						string mydocu = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "datas");

						string filepath = Path.Combine(mydocu, "bigLista.csv");

						if (!File.Exists(filepath))
						{
								Console.WriteLine($"{filepath} nem létezik!");

								return;
						}

						FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);

						StreamReader sr = new StreamReader(fs);

						List<string> rendszam = new List<string>();

						List<string> marka = new List<string>();

						List<string> tipus = new List<string>();

						List<ushort> evj = new List<ushort>();

						List<uint> ar = new List<uint>();

						sr.ReadLine();

						UInt64 osszar = 0; // osszar: 15 001 176 924 537  uint max: 4 294 967 296

						string sor = sr.ReadLine();

						while (sor != null)
						{
								string[] datas = sor.Split(',');

								rendszam.Add(datas[0]);

								marka.Add(datas[1]);

								tipus.Add(datas[2]);

								evj.Add(Convert.ToUInt16(datas[3]));

								ar.Add(Convert.ToUInt32(datas[4]));

								osszar += Convert.ToUInt64(datas[4]);

								sor = sr.ReadLine();
						}

						sr.Close();

						fs.Close();

						Console.WriteLine($"2. feladat\n\tÁtlagár: {osszar / Convert.ToUInt64(ar.Count())}");

						int arindex = 0;

						uint maxar = 0;

						for (int i = 0; i < ar.Count; i++)
						{
								if (ar[i] > maxar)
								{
										arindex = i;
										maxar = ar[i];
								}
						}

						Console.WriteLine($"\n3. feladat\n\tA legdrágább autó adatai: \n\t\t{rendszam[arindex]}, {marka[arindex]}, {tipus[arindex]}, {evj[arindex]}, {ar[arindex]}");

						Dictionary<string, int> van = new Dictionary<string, int>();

						int counter = 0;

						for (int i = 0; i < marka.Count; i++)
						{
								if (van.ContainsKey(marka[i]))
								{
										van.TryGetValue(marka[i], out counter);

										van.Remove(marka[i]);

										van.Add(marka[i], counter + 1);
								}
								else
								{
										van.Add(marka[i], 1);
								}
						}

						Console.WriteLine($"\n4. feladat\n\tautók és darabszámuk: ");

						string maxdarab = "";

						int maxdarabszam = 0;

						foreach (var line in van)
						{
								Console.WriteLine($"\t\t{line}");

								if (line.Value > maxdarabszam)
								{
										maxdarab = line.Key;

										maxdarabszam = line.Value;
								}
						}

						Console.WriteLine($"\n5. feladat\n\tAz {maxdarab} márkából van a legtöbb.");

						int ev = 2026;

						int evindex = 0;

						for (int i = 0; i < evj.Count; i++)
						{
								if (evj[i] < ev)
								{
										evindex = i;
										ev = evj[i];
								}
						}

						Console.WriteLine($"\n6. feladat\n\tA legöregebb autó adatai:\n\t\t{rendszam[evindex]}, {marka[evindex]}, {tipus[evindex]}, {evj[evindex]}, {ar[evindex]}");

						Console.ForegroundColor = ConsoleColor.DarkMagenta;

						Console.WriteLine($"\n\n\nKódolta: Pekár-Héder Botond, Farkas Bence, Zsitva Dóra");

						Console.ForegroundColor = ConsoleColor.White;
				}
		}
}