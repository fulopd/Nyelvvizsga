using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyelvvizsga
{
    class Vizsga
    {
     

        //nyelv;2009;2010;2011;2012;2013;2014;2015;2016;2017;2018
        //angol;336;231;258;350;93;194;128;308;198;174

        public string Nyelv { get; set; }

        public Dictionary<short,int> sikeresVizsgak { get; set; }
        public Dictionary<short, int> sikerestelenVizsgak { get; set; }

        public Vizsga(string nyelv)
        {
            Nyelv = nyelv;
            this.sikeresVizsgak = new Dictionary<short, int>();
            this.sikerestelenVizsgak = new Dictionary<short, int>();

        }


        public int OsszesVizsgazo 
        {
            get 
            {
                int db = 0;
                foreach (int item in sikerestelenVizsgak.Values)
                {
                    db += item;
                }
                foreach (int item in sikeresVizsgak.Values)
                {
                    db += item;
                }
                return db;
            }
        }

        public double SikertelenAtlagEvszerint(short evszam)
        {
            double siker = sikeresVizsgak[evszam];
            double sikertelen = sikerestelenVizsgak[evszam];
            if (siker + sikertelen == 0)
            {
                return 0;
            }
            var eredmeny = sikertelen / (sikertelen + siker) * 100;
            return Math.Round(eredmeny,2);
        }


        public int VizsgazokEvszerint(short evszam)
        {
            return sikeresVizsgak[evszam] + sikerestelenVizsgak[evszam];
        }

        public double sikeresAtlag()
        {
            double siker = sikeresVizsgak.Sum(x => x.Value);
            double sikertelen = sikerestelenVizsgak.Sum(x => x.Value);
            if (siker + sikertelen == 0)
            {
                return 0;
            }
            var eredmeny = siker / (sikertelen + siker) * 100;
            return Math.Round(eredmeny, 2);

        }

    }
}
