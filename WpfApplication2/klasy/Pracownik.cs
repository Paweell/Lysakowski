using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.klasy
{
    

    public class Pracownik : Osoba
    {
        public Wyksztalcenie Wyksztalcenie { get; set; }
        public Funkcja Funkcja { get; set; }
        public Pracownik()
        {
        }
        public Pracownik(KimJest kimJest, string imie, string nazwisko, PlecMF plec, string narodowosc, int nrTelefonu, Wyksztalcenie wyksztalcenie, Funkcja funkcja) : base(kimJest, imie, nazwisko, plec, narodowosc, nrTelefonu)
        {
            this.Wyksztalcenie = wyksztalcenie;
            this.Funkcja = funkcja;
        }

      

      
       
    }
}
