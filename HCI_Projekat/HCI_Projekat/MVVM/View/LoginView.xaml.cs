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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public static string email_user;

        public LoginView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTxtBox.Text;
            string sifra = PasswordTxtBox.Password.ToString();
            if (email == null || email == "")
            {
                MessageBoxResult result = MessageBox.Show("Email mora biti upisan!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (sifra == null || sifra == "")
            {
                MessageBoxResult result = MessageBox.Show("Sifra mora biti upisana!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                email = new System.Net.Mail.MailAddress(email).ToString();

            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Email mora biti u pravoj formi (example@domen.com)!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }
            Io io = new Io();
            List<Korisnik> korisnici = io.citajKorisnike();
            bool b = false;
            foreach(Korisnik k in korisnici){
                if(k._email == email)
                {
                    if (k._sifra == sifra)
                    {
                        email_user = email;
                        if (k._role == "klijent")
                        {
                           
                            UserWindow window = new UserWindow();
                            window.Show();
                            var w = Application.Current.Windows[0];
                            w.Close();

                        }
                        else if(k._role == "organizator")
                        {
                            
                            OrganizatorWindow window = new OrganizatorWindow();
                            window.Show();
                            var w = Application.Current.Windows[0];
                            w.Close();

                        }
                        else if (k._role == "admin")
                        {
                            
                            AdminWindow window = new AdminWindow();
                            window.Show();
                            var w = Application.Current.Windows[0];
                            w.Close();


                        }
                        b = true;
                    }
                }
            }
            if(!b)
            {
                MessageBoxResult result = MessageBox.Show("Pogresno uneti podaci!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }


    }
}
