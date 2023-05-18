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

                Close();
            }
            private void MakeDataTableAndDisplay()
            {
                // Create new DataTable and DataSource objects.
                DataTable table = new DataTable();

                // Declare DataColumn and DataRow variables.
                DataColumn column;
                DataRow row;
                DataView view;

                // Create new DataColumn, set DataType, ColumnName and add to DataTable.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "id";
                table.Columns.Add(column);

                // Create second column.
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "item";
                table.Columns.Add(column);

                // Create new DataRow objects and add to DataTable.
                for (int i = 0; i < 10; i++)
                {
                    row = table.NewRow();
                    row["id"] = i;
                    row["item"] = "item " + i.ToString();
                    table.Rows.Add(row);
                }

                // Create a DataView using the DataTable.
                view = new DataView(table);

                // Set a DataGrid control's DataSource to the DataView.
                //  this.adresyGrid.DataSource = view;
            }
        private void Button_Click1 (object sender, RoutedEventArgs e)
        {
            SqlConnection connetionString = new SqlConnection("Data Source=DESKTOP-CVD8VKU;Initial Catalog=Wypozyczalnia2;Integrated Security=True");
            connetionString.Open();
            SqlCommand pokaz = new SqlCommand("Select Nazwisko From Klient", connetionString);

            // ComboBox wypozyczalnie = new ComboBox();
            string sql = "SELECT * FROM Klient";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connetionString);
            DataTable table = new DataTable();
            adapter.Fill(table);

            //dg.ItemsSource = table.DefaultView;
            connetionString.Close();
        }
            private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                string selected = Login.SelectedItem.ToString();
                string queryString = "Select Klient.Nazwisko From Klient"  ;
                string connectionString = "Data Source=DESKTOP-CVD8VKU;Initial Catalog=Wypozyczalnia2;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Nazwisko", selected);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    try
                    {
                        table = new DataTable();
                        table.Load(reader);

                        // Ustaw źródło danych DataGrid na DataTable
                       // Login.ItemsSource = table.DefaultView;

                        // Włącz automatyczne generowanie kolumn
                      //  Login.AutoGenerateColumns = true;

                        // Wyłącz możliwość dodawania nowych wierszy ręcznie
                      //  Login.CanUserAddRows = false;
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window3 window3 = new Window3();
            window3.Show();
        }
    }

    public class window2
        {
        }
    }
