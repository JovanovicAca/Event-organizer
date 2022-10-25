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
    /// Interaction logic for OrgTrenutneProslaveView.xaml
    /// </summary>
    public partial class OrgTrenutneProslaveView : UserControl
    {
        private int idProslave = -1;
        private Proslava proslava;
        private List<Proslava> listaProslava { get; set; }
        private bool check = false;
        int i = 0;
        public OrgTrenutneProslaveView()
        {
            InitializeComponent();
            initFirstListBox();
        }

        private void initFirstListBox()
        {
            LB_SveProslave.Items.Clear();
            check = false;
            proslava = new Proslava();
            DropDownDugme.Content = FindResource("Dole");
            listaProslava = new List<Proslava>();
            Liste l = new Liste();
            listaProslava = l.returnProslave();

            foreach (Proslava p in listaProslava)
            { if (p._status == "Zakazana")
                {
                    ListBoxItem lbi = new ListBoxItem();
                    lbi.Content = p._id + ": " + p._imeProslave;
                    LB_SveProslave.Items.Add(lbi);
                }
            }

            ImeKlijenta.Text = "";
            ImeLokala.Text = "";
            Datum.Text = ImeLokala.Text = "";
            TemaProslave.Text = "";
            Budzet.Text = "";
            Komentar.Text = "";

        }
        
        private void fillTextBoxes(Proslava p)
        {
            string email = p._klijent_email;
            Korisnik k = new Korisnik();
            k = k.returnKorisnikByMail(email);
   
            string imePrezime = k._ime + " " + k._prezime;
            ImeKlijenta.Text = imePrezime;

            ImeLokala.Text = p._saradnik._ime;

            Datum.Text = p._datum.ToString();
            TemaProslave.Text = p._tema;
            Budzet.Text = p._budzet.ToString();
            Komentar.Text = p._komentar;

            //ListboxPravougaonik.Visibility = Visibility.Hidden;
            //SaradniciListBox.Visibility = Visibility.Hidden;

        }

        private void dodajSaradnike()
        {
            
            Liste l = new Liste();
            List<Saradnik> lista = new List<Saradnik>();
            lista = l.returnSaradnik();

            List<string> listaImena = new List<string>();

            foreach(Saradnik s in lista)
            {

                listaImena.Add(s._ime);

            }

            foreach (string s in listaImena)
            {

                SaradniciListBox.Items.Add(s);

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

        private void LB_SveProslave_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Point mPos = e.GetPosition(null);
            if (LB_OdabranaProslava.Items.Count == 1)
            {
                LB_SveProslave.Items.Clear();
                initFirstListBox();
                LB_OdabranaProslava.Items.Clear();
                idProslave = -1;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ime = ImeKlijenta.Text;
            DateTime date;
            string tema = TemaProslave.Text;
            double budzet;
            string komentar = Komentar.Text;

            try
            {
                date = (DateTime)Datum.SelectedDate;
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Pogrešan format datuma!", "Datum greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            List<Proslava> lista = new List<Proslava>();
            List<Saradnik> listaS = new List<Saradnik>();
            Liste l = new Liste();
            lista = l.returnProslave();
            listaS = l.returnSaradnik();

            string lokal = ImeLokala.Text;
            Saradnik sar = new Saradnik();

            foreach (Saradnik s in listaS)
            {
                if (ImeLokala.Text == s._ime)
                {
                    sar = s;
                    lokal = sar._ime;
                }
            }

            if (ime == null || ime == "")
            {
                MessageBoxResult result = MessageBox.Show("Ime mora biti upisano!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (lokal == null || lokal == "")
            {
                MessageBoxResult result = MessageBox.Show("Saradnik ne sme biti prazan!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (tema == null || tema == "")
            {
                MessageBoxResult result = MessageBox.Show("Tema ne sme biti prazna!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                budzet = Double.Parse(Budzet.Text);
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Budžet mora biti broj!", "Budžet greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (budzet == 0)
            {
                MessageBoxResult result = MessageBox.Show("Budžet ne sme biti nula!", "Budžet upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (komentar == null || komentar == "")
            {
                MessageBoxResult potvrda1 = MessageBox.Show("Da li ste sigurni da su ne želite da ostavite komentar?", "Komentar proslave", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (potvrda1)
                {
                    case MessageBoxResult.Yes:

                        komentar = "Nema komentara";
                        break;
                    case MessageBoxResult.No:
                        return;
                }

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


                MessageBoxResult potvrda = MessageBox.Show("Da li ste sigurni da su svi podaci dobro uneseni?", "Potvrda kreiranja proslave", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (potvrda)
            {
                case MessageBoxResult.Yes:
                    foreach (Proslava p in lista)
                    {
                        if (p._id == idProslave)
                        {

                            p._budzet = budzet;
                            p._datum = date;
                            p._komentar = komentar;
                            p._saradnik = sar;

                        }
                    }


                    Io io = new Io();
                    io.pisiProslave(lista);
                    MessageBoxResult result = MessageBox.Show("Uspešno ažuriranje proslave!", "Potvrda ažuriranja", MessageBoxButton.OK, MessageBoxImage.Information);
                    ImeKlijenta.Text = "";
                    Budzet.Text = "";
                    ImeLokala.Text = "";
                    TemaProslave.Text = "";
                    Komentar.Text = "";
                    initFirstListBox();
                    check = false;
                    LB_OdabranaProslava.Items.Clear();
                    return;

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

        private void LB_OdabranaProslava_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (((ListBox)sender).SelectedItem != null)
            {
          
                string data = ((ListBox)sender).SelectedItem.ToString();
                string[] tokens;
                tokens = data.Split(':');
                idProslave = Int32.Parse(tokens[1]);
                proslava = proslava.returnProslavaById(idProslave);
                check = true;
                fillTextBoxes(proslava);
            }

        }

        private void ImeKlijenta_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void LB_OdabranaProslava_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ImeLokala_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void proslava_promena(object sender, SelectionChangedEventArgs e)
        {

            if (((ListBox)sender).SelectedItem != null)
            {
                ImeLokala.Text = ((ListBox)sender).SelectedItem.ToString();
                ListboxPravougaonik.Visibility = Visibility.Hidden;
                SaradniciListBox.Visibility = Visibility.Hidden;
                i = 1;

            }
            else
                i = 0;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!check)
            {
                MessageBoxResult result = MessageBox.Show("Morate odabrati proslavu klikom u beli pravougaonik!", "Proslava upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult potvrda = MessageBox.Show("Da li ste sigurni da su želite da otkažete proslavu?", "Potvrda otkazivanja proslave", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (potvrda)
            {
                case MessageBoxResult.Yes:
                    List<Proslava> lista = new List<Proslava>();
                    Liste l = new Liste();
                    lista = l.returnProslave();
                    try
                    {
                        if (idProslave == null || idProslave == -1)
                        {
                            MessageBoxResult result = MessageBox.Show("Nije odabrana proslava za otkaz!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        foreach (Proslava p in lista)
                        {
                            if (p._id == idProslave)
                            {

                                p._status = "Otkazana";

                            }
                        }

                        Io io = new Io();
                        io.pisiProslave(lista);
                        MessageBoxResult result2 = MessageBox.Show("Uspešno otkazivanje proslave!", "Uspešno otkazivanje!", MessageBoxButton.OK, MessageBoxImage.Information);
                        initFirstListBox();
                        check = false;
                        LB_OdabranaProslava.Items.Clear();
                        return;
                    }
                    catch
                    {
                        MessageBoxResult result = MessageBox.Show("Greška!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                case MessageBoxResult.No:
                    return;
            }
            
        }

        private void LB_SveProslave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            RasporedSedenja w = new RasporedSedenja();
            w.Show();
        }
    }
}

