using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace WPFProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        private string connectionString = "Data Source=DESKTOP-CVD8VKU;Initial Catalog=Wypozyczalnia2;Integrated Security=True";
        private List<Klient> listaKlientow;
        private System.Data.DataTable table;
        public Window3()
        {
            InitializeComponent();

        }
        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            string imie = ImieTextBox.Text;
            string nazwisko = NazwiskoTextBox.Text;
            string pesel = PeselTextBox.Text;
            string haslo = HasloPasswordBox.Password;

            if (!string.IsNullOrWhiteSpace(imie) && !string.IsNullOrWhiteSpace(nazwisko) && !string.IsNullOrWhiteSpace(pesel) && !string.IsNullOrWhiteSpace(haslo))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string queryString = "INSERT INTO Klient (Imie, Nazwisko, Pesel, Haslo) VALUES (@Imie, @Nazwisko, @Pesel, @Haslo)";

                        using (SqlCommand command = new SqlCommand(queryString, connection))
                        {
                            command.Parameters.AddWithValue("@IDklienta", imie);
                            command.Parameters.AddWithValue("@Imie", imie);
                            command.Parameters.AddWithValue("@Nazwisko", nazwisko);
                            command.Parameters.AddWithValue("@Pesel", pesel);
                            command.Parameters.AddWithValue("@Haslo", haslo);

                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Nowy rekord został dodany do bazy danych.");

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Wystąpił błąd podczas dodawania rekordu do bazy danych: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Proszę wypełnić wszystkie pola.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
        private void dgKlienci_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //string queryString = "Select Imie, Nazwisko From Kleint";
            //string connectionString = "Data Source=DESKTOP-CVD8VKU;Initial Catalog=Wypozyczalnia2;Integrated Security=True";

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand command = new SqlCommand(queryString, connection);

            //    connection.Open();
            //    SqlDataReader reader = command.ExecuteReader();

            //    try
            //    {
            //        table = new DataTable();
            //        table.Load(reader);


            //        dgKlienci.ItemsSource = table.DefaultView;

            //        // Włącz automatyczne generowanie kolumn
            //        dgKlienci.AutoGenerateColumns = true;

            //        // Wyłącz możliwość dodawania nowych wierszy ręcznie
            //        dgKlienci.CanUserAddRows = false;
            //    }
            //    finally
            //    {
            //        reader.Close();
            //    }
            //}
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string queryString = "Select Imie, Nazwisko From Klient";
            string connectionString = "Data Source=DESKTOP-CVD8VKU;Initial Catalog=Wypozyczalnia2;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    table = new DataTable();
                    table.Load(reader);


                    dgKlienci.ItemsSource = table.DefaultView;

                    // Włącz automatyczne generowanie kolumn
                    dgKlienci.AutoGenerateColumns = true;

                    // Wyłącz możliwość dodawania nowych wierszy ręcznie
                    dgKlienci.CanUserAddRows = false;
                }
                finally
                {
                    reader.Close();
                }
            }

        }
    }
}
                       