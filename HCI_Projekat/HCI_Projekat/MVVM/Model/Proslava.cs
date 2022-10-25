using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.MVVM.Model
{
    class Proslava
    {
        public int _id { get; set; }
        public string _klijent_email { get; set; }
        public Korisnik _organizator { get; set; }//nije u konst
        public Saradnik _saradnik { get; set; }
        public string _tema { get; set; }
        public DateTime _datum { get; set; }
        public double _budzet { get; set; }
        public int _kapacitet { get; set; }
        public string _komentar { get; set; }
        public string _imeProslave { get; set; }
        public List<Sto> _stolovi { get; set; }//nije u konst
        public string _status { get; set; }//zakazana, otkazana

        public Proslava(int id, string klijent_email, Saradnik saradnik, string tema, DateTime datum, double budzet, int kapacitet, string komentar, string imeProslave)
        {
            _id = id;
            _klijent_email = klijent_email;
            _saradnik = saradnik;
            _tema = tema;
            _datum = datum;
            _budzet = budzet;
            _kapacitet = kapacitet;
            _komentar = komentar;
            _imeProslave = imeProslave;

        }

        public Proslava()
        {

        }

        public int generateID()
        {
            int id = 0;

            List<Proslava> listaProslava = new List<Proslava>();
            Liste l = new Liste();
            listaProslava = l.returnProslave();

            foreach (Proslava p in listaProslava)
            {
                id = p._id + 1;
            }

            return id;

        }

        public Proslava returnProslavaById(int id)
        {
            Proslava k = new Proslava();
            Liste l = new Liste();
            List<Proslava> lista = new List<Proslava>();
            lista = l.returnProslave();

            foreach (Proslava kor in lista)
            {
                if (kor._id == id)
                {
                    k = kor;
                    return k;
                }
            }
            return null;
        }

        public String returnStringFile()
        {
            string stoBroj = "";
            foreach (Sto sto in _stolovi)
            {
                stoBroj += sto._br_stola + ",";
            }
            if (!(stoBroj.Equals("")))
            {
                stoBroj = stoBroj.Remove(stoBroj.Length - 1);
            }
            else
            {
                stoBroj = "Nema stolova";
            }
            string organizatorMail = "";
            if(_organizator == null)
            {
                organizatorMail = "Nema organizatora";
            }
            else
            {
                organizatorMail = _organizator._email;
            }

            return _id.ToString() + ";" + _klijent_email + ";" + organizatorMail + ";" + _saradnik._pib.ToString() + ";" + _tema + ";" + _datum.ToString() + ";"
                + _budzet.ToString() + ";" + _kapacitet.ToString() + ";" + _komentar + ";" + _imeProslave + ";" + _status + ";" + stoBroj;
        }
    }
}
