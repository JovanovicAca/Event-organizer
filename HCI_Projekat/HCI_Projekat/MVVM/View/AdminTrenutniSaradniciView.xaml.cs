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
using forms = System.Windows.Forms;

namespace HCI_Projekat.MVVM.View
{
    /// <summary>
    /// Interaction logic for AdminTrenutniSaradniciView.xaml
    /// </summary>
    public partial class AdminTrenutniSaradniciView : UserControl
    {
        private bool check = false;
        private string slikaURL;
        int pib = -1;
        private string pibO = "";
        private string picture_location = "";
        private List<Saradnik> listaSaradnika { get; set; }
        int i = 0;

        private Saradnik saradnik;
        private List<Saradnik> listaProslava { get; set; }
        public AdminTrenutniSaradniciView()
        {
            InitializeComponent();
            initFirstListBox();

        }
        private void initFirstListBox()
        {
            LB_SveProslave.Items.Clear();
            check = false;
            saradnik = new Saradnik();
            DropDownDugme.Content = FindResource("Dole");
            listaProslava = new List<Saradnik>();
            Liste l = new Liste();
            listaProslava = l.returnSaradnik();

            foreach (Saradnik p in listaProslava)
            {

                ListBoxItem lbi = new ListBoxItem();
                lbi.Content = p._pib + ": " + p._ime;
                LB_SveProslave.Items.Add(lbi);

            }

            AdresaLokala.Text = "";
            KapacitetSale.Text = "";
            ImeLokala.Text = "";
            ImeLokala_Copy.Text = "";
            BrojStolova.Text = "";
            Slika.Source = null;

        }
        private void proslava_promena(object sender, SelectionChangedEventArgs e)
        {

            if (((ListBox)sender).SelectedItem != null)
            {
                PibTxtBox.Text = ((ListBox)sender).SelectedItem.ToString();
                ListboxPravougaonik.Visibility = Visibility.Hidden;
                SaradniciListBox.Visibility = Visibility.Hidden;
                i = 1;

            }
            else
                i = 0;

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

        private void LB_SveProslave_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mPos = e.GetPosition(null);
            if (LB_OdabranaProslava.Items.Count == 1)
            {
                LB_SveProslave.Items.Clear();
                initFirstListBox();
                LB_OdabranaProslava.Items.Clear();
                pib = -1;
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

        private void dodajSaradnike()
        {

            Liste l = new Liste();
            List<Korisnik> lista = new List<Korisnik>();
            lista = l.returnKorisnik();

            List<string> listaImena = new List<string>();

            foreach (Korisnik s in lista)
            {

                listaImena.Add(s._email);

            }

            foreach (string s in listaImena)
            {

                SaradniciListBox.Items.Add(s);

            }
        }

        private void LB_OdabranaProslava_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListBox)sender).SelectedItem != null)
            {

                string data = ((ListBox)sender).SelectedItem.ToString();
                string[] tokens;
                tokens = data.Split(':');
                Saradnik s = new Saradnik();
                pib = Int32.Parse(tokens[1]);
                s = s.returnSaradnikByPib(pib);
                check = true;
                fillTextBoxes(s);
            }
        }

        private void fillTextBoxes(Saradnik s)
        {
            AdresaLokala.Text = s._pib.ToString();
            pibO = AdresaLokala.Text;
            KapacitetSale.Text = s._adresa;
            ImeLokala.Text = s._kapacitet.ToString();
            ImeLokala_Copy.Text = s._ime;
            BrojStolova.Text = s._br_stolova.ToString();

            slikaURL = s._slika;
            try
            {
                Uri fileUri = new Uri(slikaURL);
                Slika.Source = new BitmapImage(fileUri);
                picture_location = Slika.Source.ToString();
            }
            catch
            {
                picture_location = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            listaSaradnika = new List<Saradnik>();
            Liste l = new Liste();
            listaSaradnika = l.returnSaradnik();
            int pib;

            try
            {
                pib = Int32.Parse(AdresaLokala.Text);
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Pib mora biti broj!", "Pib greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string adresa = KapacitetSale.Text;
            if (adresa == null || adresa == "")
            {
                MessageBoxResult result = MessageBox.Show("Adresa ne sme biti prazna!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int kapacitet;

            try
            {
                kapacitet = Int32.Parse(ImeLokala.Text);
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Kapacitet mora biti broj!", "Kapacitet greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string ime_lokala = ImeLokala_Copy.Text;
            if (ime_lokala == null || ime_lokala == "")
            {
                MessageBoxResult result = MessageBox.Show("Ime lokala ne sme biti prazno!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int br_stolova;
            try
            {
                br_stolova = Int32.Parse(BrojStolova.Text);
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Broj stolova mora biti broj!", "Broj stolova greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (i == 0)
            {
                MessageBoxResult result = MessageBox.Show("Saradnik mora biti odabran!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!check)
            {
                MessageBoxResult result = MessageBox.Show("Morate odabrati proslavu klikom u beli pravougaonik!", "Proslava upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
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
            MessageBoxResult potvrda = MessageBox.Show("Da li ste sigurni da su svi podaci dobro uneseni?", "Potvrda ažuriranja", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (potvrda)
            {
                case MessageBoxResult.Yes:
                    foreach (Saradnik s in listaSaradnika)
                    {
                        if (s._pib == pib)
                        {
                            s._pib = pib;
                            s._adresa = adresa;
                            s._br_stolova = br_stolova;
                            s._ime = ime_lokala;
                            s._kapacitet = kapacitet;
                            s._slika = slikaURL;
                        }
                    }
                    Io io1 = new Io();
                    io1.pisiSaradnike(listaSaradnika);
                    MessageBoxResult result = MessageBox.Show("Uspešno ažuriranje!", "Potvrda ažuriranja", MessageBoxButton.OK, MessageBoxImage.Information);
                    PibTxtBox.Text = "";
                    AdresaLokala.Text = "";
                    KapacitetSale.Text = "";
                    ImeLokala.Text = "";
                    BrojStolova.Text = "";
                    initFirstListBox();
                    check = false;
                    LB_OdabranaProslava.Items.Clear();
                    return;

                case MessageBoxResult.No:
                    return;
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!check)
            {
                MessageBoxResult result = MessageBox.Show("Morate odabrati proslavu klikom u beli pravougaonik!", "Proslava upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBoxResult potvrda = MessageBox.Show("Da li ste sigurni da su želite da obrišete saradnika?", "Potvrda brisanja saradnika", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (potvrda)
            {
                case MessageBoxResult.Yes:
                    listaSaradnika = new List<Saradnik>();
                    List<Saradnik> listaNova = new List<Saradnik>();
                    Liste l = new Liste();
                    listaSaradnika = l.returnSaradnik();
                    try
                    {
                        if (pib == -1)
                        {
                            MessageBoxResult result = MessageBox.Show("Nije odabran saradnik za brisanje!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        foreach (Saradnik s in listaSaradnika)
                        {
                            if (s._pib == pib)
                            {
                                continue;
                            }
                            listaNova.Add(s);
                        }
                        Io io = new Io();
                        io.pisiSaradnike(listaNova);
                        MessageBoxResult result2 = MessageBox.Show("Uspešno brisanje saradnika!", "Uspešno brisanje!", MessageBoxButton.OK, MessageBoxImage.Information);
                        initFirstListBox();
                        check = false;
                        LB_OdabranaProslava.Items.Clear();
                        return;
                    }

                    catch
                    {
                        MessageBoxResult result = MessageBox.Show("Greška pri brisanju!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                case MessageBoxResult.No:
                    return;
            }
        }
    }
}
