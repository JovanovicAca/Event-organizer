using System;
using System.Windows;
using System.Windows.Forms;

namespace HCI_Projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string message = "Da li ste sigurni da želite da ugasite Aplikaciju ??";
            string title = "Potvrda Gasenja Aplikacije";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if(result == System.Windows.Forms.DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string message = "Da li ste sigurni da želite da se odjavite?";
            string title = "Potvrda odjavljivanja";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                MainWindow window = new MainWindow();
                window.Show();
                Closed += (s, args) => this.Close();
                
            }
        }
    }
}
