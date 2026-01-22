using System;
using System.IO;

namespace Tomb
{
		internal class Tomb
		{
				static void Main(string[] args)
				{
						string mydocu = Path.Combine(
							Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
							"datas"
						);

						string filepath = Path.Combine(mydocu, "bigLista.csv");

						if (!File.Exists(filepath))
						{
								Console.WriteLine($"{filepath} nem létezik!");
								return;
						}

						int sorokSzama = 0;

						FileStream fsCount = new FileStream(filepath, FileMode.Open, FileAccess.Read);
						StreamReader srCount = new StreamReader(fsCount);

						srCount.ReadLine();

						while (srCount.ReadLine() != null)
						{
								sorokSzama++;
						}

						srCount.Close();
						fsCount.Close();

						string[] rendszam = new string[sorokSzama];
						string[] marka = new string[sorokSzama];
						string[] tipus = new string[sorokSzama];
						ushort[] evj = new ushort[sorokSzama];
						uint[] ar = new uint[sorokSzama];

						FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
						StreamReader sr = new StreamReader(fs);

						sr.ReadLine();

						int index = 0;
						string sor;

						while ((sor = sr.ReadLine()) != null)
						{
								string[] datas = sor.Split(',');

								rendszam[index] = datas[0];
								marka[index] = datas[1];
								tipus[index] = datas[2];
								evj[index] = Convert.ToUInt16(datas[3]);
								ar[index] = Convert.ToUInt32(datas[4]);

								index++;
						}

						sr.Close();
						fs.Close();

						// 2. Feladat: Autók átlagárának kiszámítása
						ulong osszar = 0;
						for (int i = 0; i < ar.Length; i++)
						{
								osszar += ar[i];
						}

						double atlagAr = (double)osszar / ar.Length;
						Console.WriteLine($"2. feladat\n\tÁtlagár: {atlagAr:F0}");

						// 3. Feladat: A legdrágább autó meghatározása
						int arIndex = 0;
						uint maxAr = ar[0];

						for (int i = 1; i < ar.Length; i++)
						{
								if (ar[i] > maxAr)
								{
										maxAr = ar[i];
										arIndex = i;
								}
						}

						Console.WriteLine(
							$"\n3. feladat\n\tA legdrágább autó adatai:" +
							$"\n\t\t{rendszam[arIndex]}, {marka[arIndex]}, {tipus[arIndex]}, {evj[arIndex]}, {ar[arIndex]}"
						);

						// 4. Feladat: Autók darabszáma márkánként
						string[] markak = new string[sorokSzama];
						int[] darab = new int[sorokSzama];
						int markaDb = 0;

						for (int i = 0; i < marka.Length; i++)
						{
								int j;
								for (j = 0; j < markaDb; j++)
								{
										if (markak[j] == marka[i])
										{
												darab[j]++;
												break;
										}
								}

								if (j == markaDb)
								{
										markak[markaDb] = marka[i];
										darab[markaDb] = 1;
										markaDb++;
								}
						}

						Console.WriteLine("\n4. feladat\n\tautók és darabszámuk:");

						for (int i = 0; i < markaDb; i++)
						{
								Console.WriteLine($"\t\t{markak[i]}: {darab[i]} db");
						}

						// 5. Feladat: Legtöbb autóval rendelkező márka
						int maxDb = darab[0];
						int maxIndex = 0;

						for (int i = 1; i < markaDb; i++)
						{
								if (darab[i] > maxDb)
								{
										maxDb = darab[i];
										maxIndex = i;
								}
						}

						Console.WriteLine($"\n5. feladat\n\tAz {markak[maxIndex]} márkából van a legtöbb.");

						// 6. Feladat: A legöregebb autó meghatározása
						ushort minEv = evj[0];
						int evIndex = 0;

						for (int i = 1; i < evj.Length; i++)
						{
								if (evj[i] < minEv)
								{
										minEv = evj[i];
										evIndex = i;
								}
						}

						Console.WriteLine(
							$"\n6. feladat\n\tA legöregebb autó adatai:" +
							$"\n\t\t{rendszam[evIndex]}, {marka[evIndex]}, {tipus[evIndex]}, {evj[evIndex]}, {ar[evIndex]}"
						);

						Console.ForegroundColor = ConsoleColor.DarkMagenta;
						Console.WriteLine("\n\n\nKódolta: Pekár-Héder Botond\nSegítettek: Farkas Bence, Zsitva Dóra");
						Console.ForegroundColor = ConsoleColor.White;
				}
		}
}
