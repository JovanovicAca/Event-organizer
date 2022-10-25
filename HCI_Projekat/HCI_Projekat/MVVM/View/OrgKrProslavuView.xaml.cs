using HCI_Projekat.MVVM.IO;
using HCI_Projekat.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Projekat.MVVM.View
{
    /// <summary>
    /// Interaction logic for OrgKrProslavuView.xaml
    /// </summary>
    public partial class OrgKrProslavuView : UserControl
    {
        private Saradnik saradnik;
        private int i = 0;
        public OrgKrProslavuView()
        {
            InitializeComponent();
            DropDownDugme.Content = FindResource("Dole");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //PROVERA DA LI SU SVI PODACI VALIDNI U INPUTIMA AKO JESU 
            //ISKACE DIJALOG DA LI STE SIGURNI DA ZELITE DA NAPRAVITE PROSLAVU

            string imePrez = ImePrezTxtBot.Text;
            string email = EmailTxtBox.Text;

            string tema = TemaTxtBox.Text;
            string ime_proslave = ImeProslave.Text;
            //DateTime date = (DateTime)Datum.SelectedDate;
            //PROVERE DA LI SU BROJEVI ONO STO TREBA DA BUDE
           
            double budzet;
            try
            {
                budzet = Double.Parse(BudzetTxtBox.Text);
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Budžet mora biti broj!", "Budžet greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            string komentar = KomentarTxtBox.Text;

            //PROVERE DA LI SU UNETI SVI PODACI
            if (imePrez == null || imePrez == "")
            {
                MessageBoxResult result = MessageBox.Show("Ime i prezime mora biti upisano!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (email == null || email == "")
            {
                MessageBoxResult result = MessageBox.Show("Email mora biti upisan!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                email = new System.Net.Mail.MailAddress(email).ToString();

            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Email mora biti u pravoj formi (example@domen.com)!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }
            if (ime_proslave == null || ime_proslave == "")
            {
                MessageBoxResult result = MessageBox.Show("Ime i prezime mora biti upisano!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (tema == null || tema == "")
            {
                MessageBoxResult result = MessageBox.Show("Tema mora biti upisana!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //PROVERA DA LI SU 0
            
            if (budzet == 0)
            {
                MessageBoxResult result = MessageBox.Show("Budzet ne sme biti 0!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
           

            if (i == 0)
            {
                MessageBoxResult result = MessageBox.Show("Saradnik mora biti odabran!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DateTime date;
            try
            {

                 date = (DateTime)Datum.SelectedDate;
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Datum u nepravilnom obliku", "Datum greška!", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
            if(date == null)
            {
                MessageBoxResult result = MessageBox.Show("Datum u nepravilnom obliku", "Datum greška!", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
            //DODATNI KOMENTAR PITAJ JEL SIGURAN
            if (komentar == null || komentar == "")
            {
                    MessageBoxResult result1 = MessageBox.Show("Da li ste sigurni da ne zelite da ostavite komentar ?", "Potvrda komentara", MessageBoxButton.YesNo,MessageBoxImage.Question);

                switch (result1)
                {
                    case MessageBoxResult.Yes:
                       
                        break;
                    case MessageBoxResult.No:
                        return;
                       
                }
            }
            MessageBoxResult potvrda = MessageBox.Show("Da li ste sigurni da su svi podaci dobro uneseni?", "Potvrda kreiranja proslave", MessageBoxButton.YesNo,MessageBoxImage.Question);
            switch (potvrda)
            {
                case MessageBoxResult.Yes:
                    
                    Proslava p = new Proslava();
                    int id = p.generateID();
                    p._id = id;
                    p._klijent_email = email;
                    p._saradnik = saradnik;
                    p._tema = tema;
                    p._datum = date;
                    p._budzet = budzet;
                    p._kapacitet = saradnik._kapacitet;
                    p._komentar = komentar;
                    p._imeProslave = ime_proslave;
                    p._stolovi = new List<Sto>();
                    p._status = "Zakazana";
                    p._organizator = findByEmail(LoginView.email_user);

                    Io io = new Io();
                    Liste l = new Liste();
                    List<Proslava> lista = l.returnProslave();
                    lista.Add(p);
                  
                    io.pisiProslave(lista);


                    MessageBoxResult obavestenje1 = MessageBox.Show("Uspesno kreirana proslava!", "Potvrda kreiranja proslave", MessageBoxButton.OK, MessageBoxImage.Information);
                    ImePrezTxtBot.Text = "";
                    EmailTxtBox.Text = "";
                    TemaTxtBox.Text = "";
                    
                    BudzetTxtBox.Text = "";
                    ImeProslave.Text = "";
                   
                    KomentarTxtBox.Text = "";
                    break;

                case MessageBoxResult.No:
                    break;
            }


        }

        private Korisnik findByEmail(string email_user)
        {
            Io io = new Io();
            List<Korisnik> org = io.citajKorisnike();
            foreach(Korisnik k in org)
            {
                if(k._email == email_user)
                {
                    return k;
                }
            }
            return null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(ListboxPravougaonik.Visibility == Visibility.Visible && SaradniciListBox.Visibility == Visibility.Visible)
            {
                DropDownDugme.Content = FindResource("Dole");
                ListboxPravougaonik.Visibility = Visibility.Hidden;
                SaradniciListBox.Visibility = Visibility.Hidden;
            }
            else
            {
                SaradniciListBox.Items.Clear();
                DropDownDugme.Content = FindResource("Gore");
                ListboxPravougaonik.Visibility = Visibility.Visible;
                SaradniciListBox.Visibility = Visibility.Visible;
                dodajSaradnike();
            }

        }

        private void dodajSaradnike()
        {
            Liste l = new Liste();
            List<Saradnik> lista = new List<Saradnik>();
            lista = l.returnSaradnik();

            List<string> listaImena = new List<string>();

            foreach (Saradnik s in lista)
            {

                listaImena.Add(s._ime);

            }

            foreach (string s in listaImena)
            {

                SaradniciListBox.Items.Add(s);

            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void proslava_promena(object sender, SelectionChangedEventArgs e)
        {
            string imeLokala = "";
            if (((ListBox)sender).SelectedItem != null)
            {
                imeLokala = ((ListBox)sender).SelectedItem.ToString();
                ImeLokala.Text = imeLokala;
                ListboxPravougaonik.Visibility = Visibility.Hidden;
                SaradniciListBox.Visibility = Visibility.Hidden;
                i = 1;

            }
            else
                i = 0;

            saradnik = FindByName(imeLokala);

        }

        private Saradnik FindByName(string imeLokala)
        {
            Io i = new Io();
            List<Saradnik> saradnici = i.citajSaradnike();
            foreach(Saradnik s in saradnici)
            {
                if(s._ime == imeLokala)
                {
                    return s;
                }
            }
            return null;
        }
    }
}
