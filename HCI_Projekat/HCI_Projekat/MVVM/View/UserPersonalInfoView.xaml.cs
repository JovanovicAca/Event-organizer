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
    /// Interaction logic for UserPersonalInfo.xaml
    /// </summary>
    public partial class UserPersonalInfo : UserControl
    {
        string email;
        public UserPersonalInfo()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ime = ImePrez.Text;
            if (ime == null || ime == "")
            {
                MessageBoxResult result = MessageBox.Show("Ime mora biti upisano!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string email = Email.Text;
            if (email == null || email == "")
            {
                MessageBoxResult result = MessageBox.Show("Ime mora biti upisano!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Io io = new Io();

            List<Korisnik> listak = io.citajKorisnike();
            foreach (Korisnik ko in listak)
            {
                if (ko._email == email)
                {
                    MessageBoxResult result = MessageBox.Show("Korisnik sa tim email-om vec postoji!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;

                }
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
        }
    }
}
