using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.MVVM.Model
{
    class Korisnik
    {
        public string _ime { get; set; }
        public string _prezime { get; set; }
        public string _email { get; set; }
        public string _sifra { get; set; }
        public string _pol { get; set; }
        public DateTime _datumRodjenja { get; set; }
        public string _role { get; set; }//klijent, organizator, admin


        public Korisnik(string ime, string prezime, string email, string sifra, string pol, DateTime datumRodj, string role)
        {
            _ime = ime;
            _prezime = prezime;
            _email = email;
            _sifra = sifra;
            _pol = pol;
            _datumRodjenja = datumRodj;
            _role = role;
        }
        public Korisnik() { }

        public Korisnik returnKorisnikByMail(string mail)
        {
            Korisnik k = new Korisnik();
            Liste l = new Liste();
            List<Korisnik> lista = new List<Korisnik>();
            lista = l.returnKorisnik();

            foreach (Korisnik kor in lista)
            {
                if (kor._email.Equals(mail))
                {
                    k = kor;
                    return k;
                }
            }
            return null;
        }

        public String returnStringFile()
        {
            return _ime + ";" + _prezime + ";" + _email + ";" + _sifra + ";" + _pol + ";" + _datumRodjenja.ToString() + ";" + _role;
        }

    }
}
