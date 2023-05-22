using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace WPFProjekt
    {
        /// <summary>
        /// Logika interakcji dla klasy MainWindow.xaml
        /// </summary>
    
        public partial class MainWindow : Window
        {
            private DataTable table;
            public MainWindow()
            {
              
                InitializeComponent();
                string queryString = "SELECT Nazwisko  FROM  Klient";
          
                string connectionString = "Data Source=DESKTOP-CVD8VKU;Initial Catalog=Wypozyczalnia2;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Login.Items.Add((reader["Nazwisko"]));// etc
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //window2 win2 = new window2();
            //win2.Show();
            Window1 window1 = new Window1();
            window1.Show();
        }
    }

    public class window2
        {
        }
    }
