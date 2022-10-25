using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.MVVM.Model
{
    class Saradnik
    {
        public int _pib { get; set; }
        public string _ime { get; set; }
        public string _adresa { get; set; }
        public int _kapacitet { get; set; }
        public int _br_stolova { get; set; }
        public string _slika { get; set; }// Source="pack://application:,,,/Images/island.jpg"


        public Saradnik(int pib, string ime, string adresa, int kapacitet, int br_stolova, string slika)
        {
            _pib = pib;
            _ime = ime;
            _adresa = adresa;
            _kapacitet = kapacitet;
            _br_stolova = br_stolova;
            _slika = slika;
        }

        public Saradnik(int pib, string ime, string adresa, int kapacitet, int br_stolova)
        {
            _pib = pib;
            _ime = ime;
            _adresa = adresa;
            _kapacitet = kapacitet;
            _br_stolova = br_stolova;
        }

        public Saradnik() { }

        public Saradnik returnSaradnikByPib(int pib)
        {
            Saradnik s = new Saradnik();
            List<Saradnik> lista = new List<Saradnik>();
            Liste l = new Liste();
            lista = l.returnSaradnik();

            foreach(Saradnik sar in lista)
            {
                if(pib == sar._pib)
                {
                    return sar;
                }
            }
            return null;
        }
        public String returnStringFile()
        {
            return _pib.ToString() + ";" + _ime + ";" + _adresa + ";" + _kapacitet.ToString() + ";" + _br_stolova.ToString() + ";" + _slika;
        }

    }
}
