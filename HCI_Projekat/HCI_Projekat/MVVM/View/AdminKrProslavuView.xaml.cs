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
    /// Interaction logic for AdminKrProslavuView.xaml
    /// </summary>
    public partial class AdminKrProslavuView : UserControl
    {
        public AdminKrProslavuView()
        {
            InitializeComponent();
            DropDownDugme.Content = FindResource("Dole");
            DropDownDugme2.Content = FindResource("Dole2");
        }

        private void DropDownDugme_Click(object sender, RoutedEventArgs e)
        {
            DropDownDugme2.Content = FindResource("Dole2");
            ListboxPravougaonikOrg.Visibility = Visibility.Hidden;
            OrgListBox.Visibility = Visibility.Hidden;
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

        private void DropDownDugme2_Click(object sender, RoutedEventArgs e)
        {

            DropDownDugme.Content = FindResource("Dole");
            ListboxPravougaonik.Visibility = Visibility.Hidden;
            SaradniciListBox.Visibility = Visibility.Hidden;
            if (ListboxPravougaonikOrg.Visibility == Visibility.Visible && OrgListBox.Visibility == Visibility.Visible)
            {
                DropDownDugme2.Content = FindResource("Dole2");
                ListboxPravougaonikOrg.Visibility = Visibility.Hidden;
                OrgListBox.Visibility = Visibility.Hidden;
            }
            else
            {
                DropDownDugme2.Content = FindResource("Gore2");
                ListboxPravougaonikOrg.Visibility = Visibility.Visible;
                OrgListBox.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(ImePrezTxtBot.Text == "")
            {
                string message = "Morate uneti Ime i Prezime Klijenta";
                string title = "Neispravni podaci !";
                System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                    System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                return;
            }
            if (EmailTxtBox.Text == "")
            {
                string message = "Morate uneti Email Klijenta";
                string title = "Neispravni podaci !";
                System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                    System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                return;
            }
            else
            {
                if(!EmailTxtBox.Text.ToLower().Contains('@'))
                {
                    string message = "Email nije u ispravnom formatu. Ispravan format :\n primer@domen.com";
                    string title = "Neispravni podaci !";
                    System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
                    System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                        System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                    return;
                }
            }
            if (BrStolovaTxtBox.Text == "")
            {
                string message = "Morate uneti Budzet Proslave";
                string title = "Neispravni podaci !";
                System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                    System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                return;
            }
            else
            {
                if (!double.TryParse(BrStolovaTxtBox.Text, out var parsedNumber))
                {
                    string message = "Budzet Proslave mora biti broj veci od 0 !";
                    string title = "Neispravni podaci";
                    System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
                    System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                        System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                    return;
                }
            }
            if (BudzetTxtBox.Text == "")
            {
                string message = "Morate uneti Temu Proslave";
                string title = "Neispravni podaci !";
                System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                    System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                return;
            }

            if(KomentarTxtBox.Text == "")
            {
                string message = "Da li ste sigurni da ne zelite da ostavite Dodatni Komentar ?";
                string title = "Korisnicka Potvrda";
                System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.YesNo;
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Io io = new Io();
                    Liste l = new Liste();
                    List<Proslava> lista = new List<Proslava>();
                    lista = l.returnProslave();

                    Proslava p = new Proslava();

                    p._klijent_email = EmailTxtBox.Text;
                    p._komentar = KomentarTxtBox.Text;
                    

                    io.pisiProslave(lista);
                    MessageBoxResult result2 = MessageBox.Show("Proslava uspešno dodata!", "Uspeh!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

        }

        private void RasporedDugme_Click(object sender, RoutedEventArgs e)
        {
            RasporedSedenja w = new RasporedSedenja();
            w.Show();
        }
    }
}
