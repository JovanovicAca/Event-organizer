using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using forms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HCI_Projekat.MVVM.Model;
using HCI_Projekat.MVVM.IO;

namespace HCI_Projekat.MVVM.View
{
    /// <summary>
    /// Interaction logic for OrgKrSaradnikaView.xaml
    /// </summary>
    public partial class OrgKrSaradnikaView : UserControl
    {
        private string picture_location = "Nema slike";
        public OrgKrSaradnikaView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int pib;
            //PROVERE DA LI SU BROJEVI TO STO TREBA
            try
            {
                pib = Int32.Parse(PIB.Text);
                if(Math.Floor(Math.Log10(pib) + 1) != 9)
                {
                    MessageBoxResult result = MessageBox.Show("Pib mora imati 9 cifara!", "Pib upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Pib mora biti broj!", "Pib upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string ime_lokala = Lokal.Text;
            if(ime_lokala == null || ime_lokala == "")
            {
                MessageBoxResult result = MessageBox.Show("Ime lokala mora biti uneseno!", "Ime upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string adresa = Adresa.Text;
            if (adresa == null || adresa == "")
            {
                MessageBoxResult result = MessageBox.Show("Adresa mora biti unesena!", "Adresa upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int kapacitet;
            try
            {
                kapacitet = Int32.Parse(Kapacitet.Text);
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Kapacitet mora broj!", "Kapacitet upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(kapacitet == 0)
            {
                MessageBoxResult result = MessageBox.Show("Kapacitet ne sme biti 0!", "Kapacitet upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(kapacitet > 1000000)
            {
                MessageBoxResult result = MessageBox.Show("Kapacitet prevelik!", "Kapacitet upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int br_stolova;
            try
            {
                br_stolova = Int32.Parse(Stolovi.Text);
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Broj stolova mora broj!", "Broj stolova upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (br_stolova == 0)
            {
                MessageBoxResult result = MessageBox.Show("Broj stolova ne sme biti 0!", "Broj stolova upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(br_stolova > 500)
            {
                MessageBoxResult result = MessageBox.Show("Broj stolova prevelik!", "Broj stolova upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Io io = new Io();
            List<Saradnik> s1 = io.citajSaradnike();
            foreach(Saradnik sar in s1)
            {
                if(sar._pib == pib)
                {
                    MessageBoxResult result = MessageBox.Show("Saradnik sa tim pibom je vec registrovan!", "Pib upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            if (picture_location == "Nema slike")
            {
                MessageBoxResult potvrda1 = MessageBox.Show("Da li ste sigurni da ne želite da dodate sliku?", "Dodavanje slike", MessageBoxButton.YesNo, MessageBoxImage.Question);

                switch (potvrda1)
                { 
                    case MessageBoxResult.Yes:
                        break;

                    case MessageBoxResult.No:
                        return;
                }
            }
            MessageBoxResult potvrda = MessageBox.Show("Da li ste sigurni da su svi podaci dobro uneseni?", "Potvrda kreiranja saradnika", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (potvrda)
            {
                case MessageBoxResult.Yes:

                    Saradnik s = new Saradnik(pib, ime_lokala, adresa, kapacitet, br_stolova, picture_location);
                    Liste l = new Liste();
                    List<Saradnik> saradnici = l.returnSaradnik();
                    saradnici.Add(s);
                    io.pisiSaradnike(saradnici);
                    MessageBoxResult obavestenje1 = MessageBox.Show("Uspesno kreiran saradnik!", "Potvrda kreiranja saradnika", MessageBoxButton.OK, MessageBoxImage.Information);
                    PIB.Text = "";
                    Lokal.Text = "";
                    Adresa.Text = "";
                    Kapacitet.Text = "";
                    Stolovi.Text = "";
                    Slika.Source = null;
                    return;
                case MessageBoxResult.No:
                    return;
            }
        }

            private void Button_Click_1(object sender, RoutedEventArgs e)
            {
                forms.OpenFileDialog openFileDialog = new forms.OpenFileDialog();
                openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                forms.DialogResult dr = openFileDialog.ShowDialog();

                
                try
                {
                    Uri fileUri = new Uri(openFileDialog.FileName);
                    Slika.Source = new BitmapImage(fileUri);
                    picture_location = Slika.Source.ToString();
                }
                catch
                {
                    //MessageBoxResult result = MessageBox.Show("Odaberite drugu sliku!", "Greska sa slikom!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
    }
}
