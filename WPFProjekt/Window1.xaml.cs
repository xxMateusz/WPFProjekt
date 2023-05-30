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
using WPFProjekt.UserControls;
using System.IO;
using Path = System.IO.Path;
namespace WPFProjekt
{

    public partial class Window1 : Window
    {
        private System.Data.DataTable table;
        private string GetDatabaseFilePath()
        {
            string databaseFileName = "Wypozyczalnia2.mdf";
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName).FullName;
            string databaseFilePath = Path.Combine(projectDirectory, databaseFileName);

            return databaseFilePath;
        }
        public Window1()
        {
           
            InitializeComponent();

            
            string queryString = "SELECT Miasto  FROM  Adres";
            string connectionString = ($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={GetDatabaseFilePath()};Integrated Security=True;Connect Timeout=30");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
             
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Wybierz_miasto.Items.Add(( reader["Miasto"]));
                    }
                }
                finally
                {
                
                    reader.Close();
                }
            }
        }
       
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
      
        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            InitializeComponent();
            Klient klient = new Klient();
            Show(klient.Pesel);
           
            
        }

        private void Show(string pesel)
        {
            throw new NotImplementedException();
        }

        private void Show(Klient klient)
        {
            throw new NotImplementedException();
        }
        private void Calendar(object sender, EventArgs e)
        {  
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connetionString = new SqlConnection(($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={GetDatabaseFilePath()};Integrated Security=True;Connect Timeout=30"));
            connetionString.Open();
            SqlCommand pokaz = new SqlCommand("Select Miasto From Adres", connetionString);

           
            string sql = "SELECT * FROM Adres";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connetionString);
            DataTable table = new DataTable();
            adapter.Fill(table);
           
           
            connetionString.Close();
        }
        private void MakeDataTableAndDisplay()
        {
           
            DataTable table = new DataTable();

           
            DataColumn column;
            DataRow row;
            DataView view;

           
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id";
            table.Columns.Add(column);

          
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "item";
            table.Columns.Add(column);

           
            for (int i = 0; i < 10; i++)
            {
                row = table.NewRow();
                row["id"] = i;
                row["item"] = "item " + i.ToString();
                table.Rows.Add(row);
            }

           
            view = new DataView(table);
          
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            
            string selected = Wybierz_miasto.SelectedItem.ToString();
            string queryString = "Select Wypozyczalnia.NazwaWypożyczalni , Adres.Miasto From Wypozyczalnia Left Join Adres  On Wypozyczalnia.IdAdres=Adres.IdAdresu Where Adres.Miasto = @miasto";
            string connectionString = ($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={GetDatabaseFilePath()};Integrated Security=True;Connect Timeout=30");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@miasto", selected);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    table = new DataTable();
                    table.Load(reader);

                   
                    adresyGrid.ItemsSource = table.DefaultView;

                   
                    adresyGrid.AutoGenerateColumns = true;

                   
                    adresyGrid.CanUserAddRows = false;
                }
                finally
                {
                    reader.Close();
                }
            }




        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            string selected = Wybierz_miasto.SelectedItem.ToString();
            string queryString = "Select *  From Samochod Left Join Wypozyczalnia On Samochod.IdWypozyczalni = Wypozyczalnia.IdWypozyczalni Where Wypozyczalnia.NazwaWypożyczalni = @nazwaWypożyczalni";
            string connectionString = ($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={GetDatabaseFilePath()};Integrated Security=True;Connect Timeout=30");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@nazwaWypożyczalni", selected);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    table = new DataTable();
                    table.Load(reader);

                   oferta.ItemsSource = table.DefaultView;

                  
                    oferta.AutoGenerateColumns = true;

                  
                    oferta.CanUserAddRows = false;
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            window2.Show();
            Close();
               

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(kalendarz.SelectedDates[0].ToString());
            MessageBox.Show(kalendarz.SelectedDates[1].ToString());
        }
    }
  
}
