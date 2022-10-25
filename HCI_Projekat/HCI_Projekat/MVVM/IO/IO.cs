using HCI_Projekat.MVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.MVVM.IO
{
    class Io
    {
        // Delimiter: ;
        // Delimiter stolova (Proslava): ,
        // Delimiter ljudi kod stolova (Sto): ,

        // Proslava: Nema stolova -> ako nema stolova(lista Sto) dodeljenih proslavi
        //           Nema organizatora -> ako nema organizatora koji je dodeljen proslavi

        // Sto: Nema gostiju -> ako nema ljudi, gostiju,(lista string) dodeljenih stolu

        public Io() { }

        public void pisiKorisnike(List<Korisnik> listaKorisnika)
        {

            using (StreamWriter writetext = new StreamWriter("../../MVVM/Data/korisnici.txt"))
            {

                foreach (Korisnik korisnik in listaKorisnika)
                {
                    writetext.WriteLine(korisnik.returnStringFile());
                }

            }

        }

        public void pisiProslave(List<Proslava> listaProslava)
        {

            using (StreamWriter writetext = new StreamWriter("../../MVVM/Data/proslave.txt"))
            {

                foreach (Proslava proslava in listaProslava)
                {
                    writetext.WriteLine(proslava.returnStringFile());
                }

            }

        }

        public void pisiSaradnike(List<Saradnik> listaSaradnika)
        {

            using (StreamWriter writetext = new StreamWriter("../../MVVM/Data/saradnici.txt"))
            {

                foreach (Saradnik saradnik in listaSaradnika)
                {
                    writetext.WriteLine(saradnik.returnStringFile());
                }

            }

        }

        public void pisiStolove(List<Sto> listaStolova)
        {

            using (StreamWriter writetext = new StreamWriter("../../MVVM/Data/stolovi.txt"))
            {

                foreach (Sto sto in listaStolova)
                {
                    writetext.WriteLine(sto.returnStringFile());
                }

            }

        }

        public List<Korisnik> citajKorisnike()
        {
            List<Korisnik> listaKorisnika = new List<Korisnik>();
            string[] listaStringova;

            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead("../../MVVM/Data/korisnici.txt"))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    listaStringova = line.Split(';');

                    string ime = listaStringova[0];
                    string prezime = listaStringova[1];
                    string email = listaStringova[2];
                    string sifra = listaStringova[3];
                    string pol = listaStringova[4];
                    DateTime datum = Convert.ToDateTime(listaStringova[5]);
                    string role = listaStringova[6];
                    Korisnik k = new Korisnik(ime, prezime, email, sifra, pol, datum, role);
                    listaKorisnika.Add(k);

                }
            }

            return listaKorisnika;

        }

        public List<Saradnik> citajSaradnike()
        {
            List<Saradnik> listaSaradnika = new List<Saradnik>();
            string[] listaStringova;


            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead("../../MVVM/Data/saradnici.txt"))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    listaStringova = line.Split(';');

                    int pib = Int32.Parse(listaStringova[0]);
                    string ime = listaStringova[1];
                    string adresa = listaStringova[2];
                    int kapacitet = Int32.Parse(listaStringova[3]);
                    int br = Int32.Parse(listaStringova[4]);
                    string slika = listaStringova[5];
                    Saradnik s = new Saradnik(pib, ime, adresa, kapacitet, br, slika);
                    listaSaradnika.Add(s);

                }
            }

            return listaSaradnika;

        }

        public List<Sto> citajStolove()
        {
            List<Sto> listaStolova = new List<Sto>();
            string[] listaStringova;

            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead("../../MVVM/Data/stolovi.txt"))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    listaStringova = line.Split(';');

                    int brojStola = Int32.Parse(listaStringova[0]);
                    int brojMesta = Int32.Parse(listaStringova[1]);
                    int flagLjudi = 0;

                    List<string> ljudi = new List<string>();
                    if (!listaStringova[2].Equals("Nema gostiju"))
                    {
                        try
                        {
                            ljudi = listaStringova[8].Split(',').ToList();
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        flagLjudi = 1;
                    }

                    Sto s = new Sto(brojStola, brojMesta, ljudi);
                    listaStolova.Add(s);

                }
            }

            return listaStolova;

        }

        public List<Proslava> citajProslave(List<Korisnik> listaKorisnika, List<Saradnik> listaSaradnika, List<Sto> listaStolova)
        {
            List<Proslava> listaProslava = new List<Proslava>();
            string[] listaStringova;
            string[] listaBrojevaStolova;


            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead("../../MVVM/Data/proslave.txt"))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {

                    listaStringova = line.Split(';');

                    int flagOrg = 0, flagSto = 0;

                    int id = Int32.Parse(listaStringova[0]);

                    Korisnik klijent = new Korisnik();
                    foreach (Korisnik k in listaKorisnika)
                    {
                        if (k._email.Equals(listaStringova[1]))
                        {
                            klijent = k;
                        }
                    }

                    Korisnik organizator = new Korisnik();
                    if (!listaStringova[2].Equals("Nema organizatora"))
                    {

                        foreach (Korisnik k in listaKorisnika)
                        {
                            try
                            {
                                if (k._email.Equals(listaStringova[2]))
                                {
                                    organizator = k;
                                }
                            }
                            catch
                            {

                            }
                        }
                    }
                    else
                    {
                        flagOrg = 1;
                    }
                    Saradnik saradnik = new Saradnik();
                    foreach (Saradnik s in listaSaradnika)
                    {
                        if (s._pib.Equals(Int32.Parse(listaStringova[3])))
                        {
                            saradnik = s;
                        }
                    }

                    string tema = listaStringova[4];
                    DateTime datum = DateTime.Parse(listaStringova[5]);
                    double budzet = double.Parse(listaStringova[6]);
                    int kapacitet = Int32.Parse(listaStringova[7]);
                    string komentar = listaStringova[8];
                    string imeProslave = listaStringova[9];
                    string status = listaStringova[10];

                    List<Sto> stolovi = new List<Sto>();
                    if (!listaStringova[11].Equals("Nema stolova"))
                    {
                        try
                        {
                            listaBrojevaStolova = listaStringova[10].Split(',');


                            foreach (string s in listaBrojevaStolova)
                            {
                                foreach (Sto sto in listaStolova)
                                {

                                    if (Int32.Parse(s) == sto._br_stola)
                                    {
                                        stolovi.Add(sto);
                                    }

                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        flagSto = 1; // Nema stolova
                    }

                    Proslava p = new Proslava(id, klijent._email, saradnik, tema, datum, budzet, kapacitet, komentar, imeProslave);

                    p._status = status;

                    if (flagOrg == 0)
                    {
                        p._organizator = organizator;
                    }

                    if (flagSto == 0 || flagSto == 1)
                    {
                        p._stolovi = stolovi;
                    }

                    listaProslava.Add(p);

                }

            }

            return listaProslava;

        }

    }
}
