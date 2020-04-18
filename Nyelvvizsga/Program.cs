using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyelvvizsga
{
    class Program
    {
        static private List<Vizsga> nyelvvizygaLista = new List<Vizsga>();
        static void Main(string[] args)
        {

            #region 1. feladat
            void BeolvasSikeres()
            {

                using (StreamReader sr = new StreamReader("Forras/sikeres.csv"))
                {
                    string[] fejlec = sr.ReadLine().Split(';');
                    while (!sr.EndOfStream)
                    {
                        string[] sorTomb = sr.ReadLine().Split(';');
                        Vizsga nyelvvizsga = new Vizsga(sorTomb[0]);
                        nyelvvizsga.sikeresVizsgak.Add(short.Parse(fejlec[1]), Convert.ToInt32(sorTomb[1]));
                        nyelvvizsga.sikeresVizsgak.Add(short.Parse(fejlec[2]), Convert.ToInt32(sorTomb[2]));
                        nyelvvizsga.sikeresVizsgak.Add(short.Parse(fejlec[3]), Convert.ToInt32(sorTomb[3]));
                        nyelvvizsga.sikeresVizsgak.Add(short.Parse(fejlec[4]), Convert.ToInt32(sorTomb[4]));
                        nyelvvizsga.sikeresVizsgak.Add(short.Parse(fejlec[5]), Convert.ToInt32(sorTomb[5]));
                        nyelvvizsga.sikeresVizsgak.Add(short.Parse(fejlec[6]), Convert.ToInt32(sorTomb[6]));
                        nyelvvizsga.sikeresVizsgak.Add(short.Parse(fejlec[7]), Convert.ToInt32(sorTomb[7]));
                        nyelvvizsga.sikeresVizsgak.Add(short.Parse(fejlec[8]), Convert.ToInt32(sorTomb[8]));
                        nyelvvizsga.sikeresVizsgak.Add(short.Parse(fejlec[9]), Convert.ToInt32(sorTomb[9]));
                        nyelvvizsga.sikeresVizsgak.Add(short.Parse(fejlec[10]), Convert.ToInt32(sorTomb[10]));


                        nyelvvizygaLista.Add(nyelvvizsga);
                    }
                }

            }

            void BeolvasasSikertelen()
            {
                using (StreamReader sr = new StreamReader("Forras/sikertelen.csv"))
                {
                    string[] fejlec = sr.ReadLine().Split(';');
                    while (!sr.EndOfStream)
                    {
                        var sorTomb = sr.ReadLine().Split(';');
                        var nyelvvizsga = nyelvvizygaLista.SingleOrDefault(x => x.Nyelv == sorTomb[0]);

                        short evszam = 2009;
                        for (int i = 1; i < sorTomb.Length; i++)
                        {
                            var dbszam = Convert.ToInt32(sorTomb[i]);
                            //sikertelen listahoz hozzáadás
                            nyelvvizsga.sikerestelenVizsgak.Add(evszam, dbszam);
                            evszam++;
                        }
                    }
                }
            }

            BeolvasSikeres();
            BeolvasasSikertelen();

            #endregion

            #region 2. feladat
            Console.WriteLine("2. feladat: A legnépszerűbb nyelvek:");

            var nepszeruLista = nyelvvizygaLista.OrderByDescending(x => x.OsszesVizsgazo).ToList();

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("\t" + nepszeruLista[i].Nyelv);
            }

            #endregion

            #region 3. feladat
            Console.WriteLine("3. feladat:");
            short bekertEvszam = 0;
            do
            {
                Console.Write("\tVizsgálandó év: ");
                short.TryParse(Console.ReadLine(), out bekertEvszam);

            } while (bekertEvszam > 2018 || bekertEvszam < 2009);
            #endregion

            #region 4. feladat
            Console.WriteLine("4. feladat:");
            var sikertelenebb = nyelvvizygaLista.OrderByDescending(x => x.SikertelenAtlagEvszerint(bekertEvszam)).First();
            Console.WriteLine($"\t{bekertEvszam}. évben " +
                $"{sikertelenebb.Nyelv} nyelvből a sikertelen vizsgák aránya: " +
                $"{sikertelenebb.SikertelenAtlagEvszerint(bekertEvszam)}%");
            #endregion

            #region 5. feladat
            Console.WriteLine("5. feladat:");
            var vizsgazoSzam = nyelvvizygaLista
                .Where(x => x.VizsgazokEvszerint(bekertEvszam).Equals(0))
                //foreach LINQ -hoz kell
                .ToList();

            if (vizsgazoSzam.Any())
            {
                vizsgazoSzam.ForEach(x => Console.WriteLine("\t" + x.Nyelv));
            }
            else
            {
                Console.WriteLine("\tMinden nyelvből volt vizsgázó!");
            }

            #endregion

            #region 6. feladat
            using (StreamWriter sw = new StreamWriter("kimenet.csv", false, Encoding.UTF8))
            {
                foreach (var item in nyelvvizygaLista)
                {
                    var atlag = item.sikeresAtlag().ToString().Replace(',', '.');
                    sw.WriteLine($"{item.Nyelv};{item.OsszesVizsgazo};{atlag}%");
                }

            }
            #endregion

            Console.ReadKey();
        }
    }
}
