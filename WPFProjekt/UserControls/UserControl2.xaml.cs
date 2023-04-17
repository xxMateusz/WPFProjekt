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

namespace WPFProjekt.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy UserControl2.xaml
    /// </summary>
    public partial class UserControl2 : UserControl
    {

        public UserControl2()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Klient klient = new Klient();
            klient.IdKlienta = 1;
            klient.Haslo = "12";

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Klient klient = new Klient();
            klient.IdKlienta = 1;
            Klient klient2 = new Klient();
            klient2.IdKlienta = 2;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
        }
    }
}
