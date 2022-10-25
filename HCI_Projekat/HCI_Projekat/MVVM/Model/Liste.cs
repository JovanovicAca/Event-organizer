using HCI_Projekat.MVVM.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.MVVM.Model
{
    class Liste
    {

        public List<Korisnik> listaKorisnika;
        public List<Saradnik> listaSaradnika;
        public List<Proslava> listaProslava;
        public List<Sto> listaStolova;

        public Liste()
        {

        }

        public List<Korisnik> returnKorisnik()
        {
            updateLists();
            return listaKorisnika;
        }

        public List<Saradnik> returnSaradnik()
        {
            updateLists();
            return listaSaradnika;
        }

        public List<Sto> returnStolove()
        {
            updateLists();
            return listaStolova;
        }

        public List<Proslava> returnProslave()
        {
            updateLists();
            return listaProslava;
        }

        public void updateLists()
        {

            Io io = new Io();
            listaKorisnika = io.citajKorisnike();
            listaSaradnika = io.citajSaradnike();
            listaStolova = io.citajStolove();
            listaProslava = io.citajProslave(listaKorisnika, listaSaradnika, listaStolova);

        }
    }
}
