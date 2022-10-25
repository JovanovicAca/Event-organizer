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
    /// Interaction logic for UserKrProslavuView.xaml
    /// </summary>
    public partial class UserKrProslavuView : UserControl
    {
        private Saradnik saradnik;
        private int i = 0;
        private Korisnik organizator;
        private List<Korisnik> listOrganizatora { get; set; }
        private bool check = false;
        public UserKrProslavuView()
        {
            InitializeComponent();
            InitFirstListBox();
            //DropDownDugme.Content = FindResource("Dole");
        }

        private void InitFirstListBox()
        {
            LB_SveProslave.Items.Clear();
            check = false;
            organizator = new Korisnik();
            DropDownDugme.Content = FindResource("Dole");
            listOrganizatora = new List<Korisnik>();
            Liste l = new Liste();
            listOrganizatora = l.returnKorisnik();

            foreach (Korisnik k in listOrganizatora)
            {
                if (k._role == "organizator")
                {
                    ListBoxItem lbi = new ListBoxItem();
                    lbi.Content = k._email;
                    LB_SveProslave.Items.Add(lbi);
                }
            }

            ImeLokala.Text = "";
            Budzet.Text = "";
            TemaProslave.Text = "";
           

        }
    

        private void LB_SveProslave_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Point mPos = e.GetPosition(null);
            if (LB_OdabranaProslava.Items.Count == 1)
            {
                LB_SveProslave.Items.Clear();
                InitFirstListBox();
                LB_OdabranaProslava.Items.Clear();
                //idProslave = -1;
            }

            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(mPos.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(mPos.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                try
                {
                    ListBoxItem selectedItem = (ListBoxItem)LB_OdabranaProslava.SelectedItem;
                    LB_OdabranaProslava.Items.Remove(selectedItem);
                    DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem), DragDropEffects.Copy);
                    if (selectedItem.Parent == null)
                    {
                        LB_OdabranaProslava.Items.Add(selectedItem);
                    }
                }
                catch { }
            }
        }
        private void LB_OdabranaProslava_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mPos = e.GetPosition(null);

            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(mPos.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(mPos.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                try
                {
                    ListBoxItem selectedItem = (ListBoxItem)LB_SveProslave.SelectedItem;
                    LB_SveProslave.Items.Remove(selectedItem);
                    DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem), DragDropEffects.Copy);
                    if (selectedItem.Parent == null)
                    {
                        LB_SveProslave.Items.Add(selectedItem);
                    }
                }
                catch { }
            }
        }
        private void LB_SveProslave_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void LB_OdabranaProslava_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void LB_OdabranaProslava_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                LB_OdabranaProslava.Items.Add(listItem);
            }
        }

        private void LB_SveProslave_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                LB_SveProslave.Items.Add(listItem);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = LoginView.email_user;
            int saradnik_pib = saradnik._pib;
            
            string tema = TemaProslave.Text;
            if (tema == null || tema == "")
            {
                MessageBoxResult result = MessageBox.Show("Tema mora biti upisana!", "Upozorenje tema!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string ime_proslave = ImeProslave.Text;
            if (ime_proslave == null || ime_proslave == "")
            {
                MessageBoxResult result = MessageBox.Show("Ime proslave mora biti upisano!", "Upozorenje ime proslave!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            double budzet;
            try
            {
                budzet = Double.Parse(Budzet.Text);
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Budzet mora biti broj!", "Budžet upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(budzet == 0)
            {
                MessageBoxResult result = MessageBox.Show("Budzet ne sme biti 0!", "Budžet upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!check)
            {
                MessageBoxResult result = MessageBox.Show("Morate odabrati proslavu klikom u beli pravougaonik!", "Proslava upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DateTime date;
            try
            {
                date = (DateTime)Datum1.SelectedDate;
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Pogrešan format datuma!", "Datum greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            List<Saradnik> listaS = new List<Saradnik>();
            Liste l = new Liste();
            listaS = l.returnSaradnik();
            saradnik = new Saradnik();
            string lokal = ImeLokala.Text;
            foreach (Saradnik s in listaS)
            {
                if (ImeLokala.Text == s._ime)
                {
                    saradnik = s;
                    lokal = saradnik._ime;
                }
            }

            MessageBoxResult potvrda = MessageBox.Show("Da li ste sigurni da su svi podaci dobro uneseni?", "Potvrda kreiranja proslave", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (potvrda)
            {
                case MessageBoxResult.Yes:
                    Proslava p = new Proslava();
                    int id = p.generateID();
                    p._id = id;
                    p._klijent_email = email;
                    p._organizator = organizator;
                    p._saradnik = saradnik;
                    p._tema = tema;
                    p._datum = date;
                    p._budzet = budzet;
                    p._kapacitet = saradnik._kapacitet;
                    p._komentar = "Nema komentar";
                    p._imeProslave = ime_proslave;
                    p._stolovi = new List<Sto>();
                    p._status = "Zakazana";

                    Io io = new Io();
                    Liste li = new Liste();
                    List<Proslava> lista = li.returnProslave();
                    lista.Add(p);

                    io.pisiProslave(lista);


                    MessageBoxResult obavestenje1 = MessageBox.Show("Uspesno kreirana proslava!", "Potvrda kreiranja proslave", MessageBoxButton.OK, MessageBoxImage.Information);
                    ImeLokala.Text = "";
                    Budzet.Text = "";
                    ImeProslave.Text = "";
                    TemaProslave.Text = "";
                    ImeProslave.Text = "";
                    check = false;
                    InitFirstListBox();
                    LB_OdabranaProslava.Items.Clear();
                    break;

                case MessageBoxResult.No:
                    return;
            }





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
                SaradniciListBox.Items.Clear();
                DropDownDugme.Content = FindResource("Gore");
                ListboxPravougaonik.Visibility = Visibility.Visible;
                SaradniciListBox.Visibility = Visibility.Visible;
                dodajSaradnike();
            }
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
            foreach (Saradnik s in saradnici)
            {
                if (s._ime == imeLokala)
                {
                    return s;
                }
            }
            return null;
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

        private void LB_SveProslave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LB_OdabranaProslava_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListBox)sender).SelectedItem != null)
            {

                string data = ((ListBox)sender).SelectedItem.ToString();
                string[] tokens;
                tokens = data.Split(':');
                string email = tokens[1];
                organizator = organizator.returnKorisnikByMail(email.Trim());
                check = true;
                Console.WriteLine("AAAAAAAAA");

                // fillTextBoxes(data);
            }
        }

        private void RasporedDugme_Click(object sender, RoutedEventArgs e)
        {
            RasporedSedenja w = new RasporedSedenja();
            w.Show();
        }
    }
}
