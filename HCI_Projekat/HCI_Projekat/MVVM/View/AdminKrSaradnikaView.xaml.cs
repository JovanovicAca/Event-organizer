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
    /// Interaction logic for AdminKrSaradnikaView.xaml
    /// </summary>
    public partial class AdminKrSaradnikaView : UserControl
    {
        public AdminKrSaradnikaView()
        {
            InitializeComponent();
            DropDownDugme.Content = FindResource("Dole");
        }

        private void DropDownDugme_Click(object sender, RoutedEventArgs e)
        {
            if (ListboxPravougaonik.Visibility == Visibility.Visible && SaradniciListBox.Visibility == Visibility.Visible)
            {
                DropDownDugme.Content = FindResource("Dole");
                ListboxPravougaonik.Visibility = Visibility.Hidden;
                SaradniciListBox.Visibility = Visibility.Hidden;
            }
            else
            {
                DropDownDugme.Content = FindResource("Gore");
                ListboxPravougaonik.Visibility = Visibility.Visible;
                SaradniciListBox.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(PIB.Text == "")
            {
                string message = "Morate uneti PIB Saradnika !";
                string title = "Neispravni podaci !";
                System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                    System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                return;
            }
            else
            {
                if (!double.TryParse(PIB.Text, out var parsedNumber))
                {
                    string message = "PIB Mora biti broj od 9 cifara !";
                    string title = "Neispravni podaci";
                    System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
                    System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                        System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                    return;
                }
                if(PIB.Text.Length != 9)
                {
                    string message = "PIB Mora biti broj od 9 cifara !";
                    string title = "Neispravni podaci";
                    System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
                    System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                        System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                    return;
                }
            }
            if (Lokal.Text == "")
            {
                string message = "Morate uneti Ime Lokala Saradnika !";
                string title = "Neispravni podaci !";
                System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                    System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                return;
            }
            if (Adresa.Text == "")
            {
                string message = "Morate uneti Adresu Lokala Saradnika";
                string title = "Neispravni podaci !";
                System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                    System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                return;
            }
            if (Kapacitet.Text == "")
            {
                string message = "Morate uneti Kapacitet Lokala Saradnika";
                string title = "Neispravni podaci !";
                System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                    System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                return;
            }
            else
            {
                if (!double.TryParse(Kapacitet.Text, out var parsedNumber))
                {
                    string message = "Kapacitet Lokala Saradnika mora biti broj veci od 0 !";
                    string title = "Neispravni podaci";
                    System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
                    System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                        System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                    return;
                }
            }
            if (Stolovi.Text == "")
            {
                string message = "Morate uneti Broj Stolova u Lokalu Saradnika !";
                string title = "Neispravni podaci !";
                System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                    System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                return;
            }
            else
            {
                if (!double.TryParse(Stolovi.Text, out var parsedNumber))
                {
                    string message = "Broj Stolova u Lokalu Saradnika mora biti broj veci od 0 !";
                    string title = "Neispravni podaci";
                    System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
                    System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                        System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                    return;
                }
            }
            Io io = new Io();
            Liste l = new Liste();
            List < Saradnik > lista = new List<Saradnik>();
            lista = l.returnSaradnik();
           // string slika = Slika.Source.ToString();
           // if(slika == null)
            //{
             //   slika = "Nema slike";
            //}
            
            Saradnik s = new Saradnik(
                Int32.Parse(PIB.Text.ToString()),
                Lokal.Text,
                Adresa.Text.ToString(),
                Int32.Parse(Kapacitet.Text.ToString()),
                Int32.Parse(Stolovi.Text.ToString())
                ) ;
            s._slika = "Nema slike";
            lista.Add(s);

            io.pisiSaradnike(lista);
            MessageBoxResult result2 = MessageBox.Show("Saradnik uspešno dodat!", "Uspeh!", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
    }
}
