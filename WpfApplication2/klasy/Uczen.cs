using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.klasy
{
   
    public class Uczen : Osoba
    {
        public Klasy Klasa { get; set; }
        public Uczen()
        {
        }

        public Uczen(KimJest kimJest, string imie, string nazwisko, PlecMF plec, string narodowosc, int nrTelefonu, Klasy klasa) : base(kimJest, imie, nazwisko, plec, narodowosc, nrTelefonu)
        {
            this.Klasa = klasa;
        }

       
      

        
       
    }

}
