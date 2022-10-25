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
    /// Interaction logic for AdminGotoveProslaveView.xaml
    /// </summary>
    public partial class AdminGotoveProslaveView : UserControl
    {
        private int idProslave = -1;
        private Proslava proslava;
        private List<Proslava> listaProslava { get; set; }
        public AdminGotoveProslaveView()
        {
            InitializeComponent();
            initFirstListBox();
        }

        private void initFirstListBox()
        {
            proslava = new Proslava();
            listaProslava = new List<Proslava>();
            Liste l = new Liste();
            listaProslava = l.returnProslave();

            foreach (Proslava p in listaProslava)
            {
                if (p._status == "Otkazana")
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
            if (k == null)
            {

            }
            else
            {
                string imePrezime = k._ime + " " + k._prezime;
                ImeKlijenta.Text = imePrezime;

                ImeLokala.Text = p._saradnik._ime;

                Datum.Text = p._datum.ToString();
                TemaProslave.Text = p._tema;
                Budzet.Text = p._budzet.ToString();
                Komentar.Text = p._komentar;
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
        private void LB_OdabranaProslava_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (((ListBox)sender).SelectedItem != null)
            {

                string data = ((ListBox)sender).SelectedItem.ToString();
                string[] tokens;
                tokens = data.Split(':');
                idProslave = Int32.Parse(tokens[1]);
                proslava = proslava.returnProslavaById(idProslave);

                fillTextBoxes(proslava);
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
        // :D
        private void Komentar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RasporedSedenja w = new RasporedSedenja();
            w.Show();
        }
    }
}