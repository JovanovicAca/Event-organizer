using HCI_Projekat.MVVM.IO;
using HCI_Projekat.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using forms = System.Windows.Forms;

namespace HCI_Projekat.MVVM.View
{
    /// <summary>
    /// Interaction logic for OrgTrenutniSaradniciView.xaml
    /// </summary>
    public partial class OrgTrenutniSaradniciView : UserControl
    {
        private string slikaURL;
        private int pib = -1;
        private Saradnik saradnik;
        private List<Saradnik> listaSaradnika { get; set; }
        private bool check = false;
        private string picture_location = "";
        private string pibO = "";
        public OrgTrenutniSaradniciView()
        {
            InitializeComponent();
            initFirstListBox();
        }

        private void initFirstListBox()
        {
            LB_SveProslave.Items.Clear();
            check = false;
            saradnik = new Saradnik();
            listaSaradnika = new List<Saradnik>();
            Liste l = new Liste();
            listaSaradnika = l.returnSaradnik();

            foreach(Saradnik s in listaSaradnika)
            {
                ListBoxItem lbi = new ListBoxItem();
                lbi.Content = s._pib + ": " + s._ime;
                LB_SveProslave.Items.Add(lbi);
            }

            PibTxtBox.Text = "";
            AdresaLokala.Text = "";
            KapacitetSale.Text = "";
            ImeLokala.Text = "";
            BrojStolova.Text = "";
            Slika.Source = null;

        }

        private void fillTextBoxes(Saradnik s)
        {
            PibTxtBox.Text = s._pib.ToString();
            pibO = PibTxtBox.Text;
            AdresaLokala.Text = s._adresa;
            KapacitetSale.Text = s._kapacitet.ToString();
            ImeLokala.Text = s._ime;
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

        private void LB_OdabranaProslava_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
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

        //Sli
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

        //Arz
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            listaSaradnika = new List<Saradnik>();
            Liste l = new Liste();
            listaSaradnika = l.returnSaradnik();
            
            try
            {
                try
                {
                    pib = Int32.Parse(PibTxtBox.Text);
                    if (Math.Floor(Math.Log10(pib) + 1) != 9)
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
                string adresa = AdresaLokala.Text;
                if (adresa == null || adresa == "")
                {
                    MessageBoxResult result = MessageBox.Show("Adresa lošeg formata!", "Upozorenje adresa!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                int kapacitet = Int32.Parse(KapacitetSale.Text);
                string imeLokala = ImeLokala.Text;
                if (imeLokala == null || imeLokala == "")
                {
                    MessageBoxResult result = MessageBox.Show("Ime lokala lošeg formata!", "Upozorenje ime lokala!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!check)
                {
                    MessageBoxResult result = MessageBox.Show("Morate odabrati proslavu klikom u beli pravougaonik!", "Proslava upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                int brojStolova;
                try
                {
                    brojStolova = Int32.Parse(BrojStolova.Text);
                }
                catch
                {
                    MessageBoxResult result = MessageBox.Show("Broj stolova mora biti broj!", "Broj stolova greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Io io = new Io();
                List<Saradnik> s1 = io.citajSaradnike();
                foreach (Saradnik sar in s1)
                {
                    if (sar._pib == pib)
                    {
                        if(sar._pib != Int32.Parse(pibO))
                        {
                            MessageBoxResult result = MessageBox.Show("Saradnik sa tim pibom je vec registrovan!", "Pib upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                       
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
                                s._br_stolova = brojStolova;
                                s._ime = imeLokala;
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
            catch
            {
                MessageBoxResult result = MessageBox.Show("Greška pri unosu podataka!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
        }

        //Obr
        private void Button_Click_4(object sender, RoutedEventArgs e)
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

        private void LB_SveProslave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
