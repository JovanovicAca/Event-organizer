using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HCI_Projekat
{
    /// <summary>
    /// Interaction logic for RasporedSedenja.xaml
    /// </summary>
    public partial class RasporedSedenja : Window
    {
        public RasporedSedenja()
        {
            InitializeComponent();
            Dugme.Content = FindResource("Lupa");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String text = ImePrezTxtBot.Text;
            if(ImePrezTxtBot.Text.Length == 0)
            {
                string message = "Ne mozete dodati gosta bez Imena i Prezimena !";
                string title = "Upozorenje";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, 
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                return;
            }
            else
            {
                SpisakGostijuListBox.Items.Add(text);
                ImePrezTxtBot.Text = "";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SpisakGostijuListBox.UnselectAll();
            bool found = false;
            for (int i = SpisakGostijuListBox.Items.Count - 1; i >= 0; --i)
            {
                var item = SpisakGostijuListBox.Items[i];
                if(item.ToString().Equals(ImePrezTxtBot_Copy.Text.ToString()))
                {
                    found = true;
                    SpisakGostijuListBox.SelectedItems.Add(item);
                    SpisakGostijuListBox.ScrollIntoView(item);
                }
            }
            if(!found)
            {
                if(ImePrezTxtBot_Copy.Text.Length == 0)
                {
                    string message = "Da biste uspesno pronasli gosta morate mu uneti Ime i Prezime";
                    string title = "Obavestenje";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    ImePrezTxtBot_Copy.Text = "";
                    return;
                }
                else
                {
                    string message = "Ne postoji gost pod tim Imenom i Prezimenom.";
                    string title = "Obavestenje";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    ImePrezTxtBot_Copy.Text = "";
                    return;
                }
            }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(SpisakGostijuListBox.SelectedItems.Count > 0)
            {
                for (int i = SpisakGostijuListBox.SelectedItems.Count - 1; i >= 0; --i)
                {
                    string message = "Da li ste sigurni da zelite da uklonite " + SpisakGostijuListBox.SelectedItems[i] + " sa spiska?";
                    string title = "Potvrda Brisanja Spiska Gostiju";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        SpisakGostijuListBox.Items.Remove(SpisakGostijuListBox.SelectedItems[i]);
                    }
                }
                ImePrezTxtBot_Copy.Text = "";
                SpisakGostijuListBox.UnselectAll();
            }
            else
            {
                string message = "Morate Oznaciti Gosta kog zelite da Uklonite !";
                string title = "Obavestenje";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                ImePrezTxtBot_Copy.Text = "";
                return;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(SpisakGostijuListBox.Items.Count > 0)
            {
                string message = "Da li ste sigurni da zelite da ispraznite Spisak Gostiju?";
                string title = "Potvrda Brisanja Spiska Gostiju";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    for (int i = SpisakGostijuListBox.Items.Count - 1; i >= 0; --i)
                    {
                        SpisakGostijuListBox.Items.Remove(SpisakGostijuListBox.Items[i]);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                string message = "Spisak Gostiju je vec prazan !";
                string title = "Obavestenje";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                ImePrezTxtBot_Copy.Text = "";
                return;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if(SpisakGostijuListBox.Items.Count > 0)
            {
                string message = "Spisak Gostiju vec sadrzi Goste. Da li ste sigurni da zelite jos gostiju da ucitate ?";
                string title = "Potvrda Dodavanja Novih Gostiju na Spisak";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                    DialogResult dr = openFileDialog.ShowDialog();

                    try
                    {
                        string path = openFileDialog.FileName;
                        using (var reader = new StreamReader(path))
                        {
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();
                                var values = line.Split(',');
                                String lik = values[0] + " " + values[1];
                                SpisakGostijuListBox.Items.Add(lik);
                            }
                        }
                    }
                    catch
                    {
                        string message2 = "Morate odabrati datoteku sa ekstenzijom .csv !";
                        string title2 = "Upozorenje";
                        MessageBoxButtons buttons2 = MessageBoxButtons.OK;
                        DialogResult result2 = System.Windows.Forms.MessageBox.Show(message2, title2, buttons2,
                            MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                DialogResult dr = openFileDialog.ShowDialog();

                try
                {
                    string path = openFileDialog.FileName;
                    using (var reader = new StreamReader(path))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            String lik = values[0] + " " + values[1];
                            SpisakGostijuListBox.Items.Add(lik);
                        }
                    }
                }
                catch
                {
                    string message2 = "Morate odabrati datoteku sa ekstenzijom .csv !";
                    string title2 = "Upozorenje";
                    MessageBoxButtons buttons2 = MessageBoxButtons.OK;
                    DialogResult result2 = System.Windows.Forms.MessageBox.Show(message2, title2, buttons2,
                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    return;
                }
            }
        }
    }
}
