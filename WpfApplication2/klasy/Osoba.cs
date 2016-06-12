using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication2.klasy;

namespace WpfApplication2
{
   


    public class Osoba
    {   
        public KimJest KimJest { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public PlecMF Plec { get; set; }
        public string Narodowosc { get; set; }
        public int NrTelefonu { get; set; }




        
        public Osoba() { }
        // konstruktor użytkowy
        public Osoba(KimJest kimJest, string imie, string nazwisko, PlecMF plec, string narodowosc, int nrTelefonu)
        {
            this.KimJest = kimJest;
            this.Imie = imie;
            this.Nazwisko = nazwisko;
            this.Narodowosc = narodowosc;
            this.NrTelefonu = nrTelefonu;
            this.Plec = plec;



        }


    }


}
