using HCI_Projekat.MVVM.Model;
using HCI_Projekat.MVVM.IO;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        private string pol = "";
        private string imе = "";
        private string prezime = "";
        public RegisterView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTxtBox.Text;
            string sifra = LozinkaTxtBox.Password.ToString();


            var picker = sender as DatePicker;
            DateTime date = (DateTime)Datum.SelectedDate;

            //PROVERA VALIDNOSTI PODATAKA
            if (imе == null || imе == "")
            {
                MessageBoxResult result = MessageBox.Show("Ime mora biti upisano!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (prezime == null || prezime == "")
            {
                MessageBoxResult result = MessageBox.Show("Prezime mora biti upisano!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            if (sifra == null || sifra == "")
            {
                MessageBoxResult result = MessageBox.Show("Lozinka mora biti upisana!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (pol == null || pol == "")
            {
                MessageBoxResult result = MessageBox.Show("Pol mora biti izabran!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Io io = new Io();

            List<Korisnik> listak = io.citajKorisnike();
            foreach(Korisnik ko in listak)
            {
                if(ko._email == email)
                {
                    MessageBoxResult result = MessageBox.Show("Korisnik sa tim email-om vec postoji!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;

                }
            }

            //DODAVANJE KORISNIKA
            

            MessageBoxResult potvrda = MessageBox.Show("Da li ste sigurni da su svi podaci dobro uneseni?", "Potvrda kreiranja proslave", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (potvrda)
            {
                case MessageBoxResult.Yes:
                    Korisnik k = new Korisnik(imе, prezime, email, sifra, pol, date, "klijent");

                    Liste l = new Liste();
                    List<Korisnik> lista = l.returnKorisnik();

                    lista.Add(k);
                    Io i = new Io();
                    i.pisiKorisnike(lista);
                    MessageBoxResult obavestenje = MessageBox.Show("Uspesno kreiran nalog!", "Potvrda kreiranja naloga", MessageBoxButton.OK, MessageBoxImage.Information);
                    var w = Application.Current.Windows[0];
                    w.Hide();
                    UserWindow user = new UserWindow();
                    user.ShowDialog();

                   

                    break;

                case MessageBoxResult.No:
                    break;
            } 
           
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pol_checked(object sender, RoutedEventArgs e)
        {
            pol = (string)(sender as RadioButton).Content;
        }

        private void ImeTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            imе = (string)(sender as TextBox).Text;
        }

        private void PrezimeTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            prezime = (sender as TextBox).Text;
        }
    }
}
