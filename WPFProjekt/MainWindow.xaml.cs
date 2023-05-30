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
using System.IO;
using Path = System.IO.Path;
namespace WPFProjekt
    {


    public partial class MainWindow : Window
    {
        private string GetDatabaseFilePath()
        {
            string databaseFileName = "Wypozyczalnia2.mdf";
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName).FullName;
            string databaseFilePath = Path.Combine(projectDirectory, databaseFileName);

            return databaseFilePath;
        }


        private DataTable table;
        public MainWindow()
        {

            InitializeComponent();
            string queryString = "SELECT Nazwisko  FROM  Klient";

            string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={GetDatabaseFilePath()};Integrated Security=True;Connect Timeout=30";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
               
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Login.Items.Add((reader["Nazwisko"]));
                    }
                }
                finally
                {
                
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
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            SqlConnection connetionString = new SqlConnection(($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={GetDatabaseFilePath()};Integrated Security=True;Connect Timeout=30"));
            connetionString.Open();
            SqlCommand pokaz = new SqlCommand("Select Nazwisko From Klient", connetionString);

           
            string sql = "SELECT * FROM Klient";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connetionString);
            DataTable table = new DataTable();
            adapter.Fill(table);

          
            connetionString.Close();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = Login.SelectedItem.ToString();
            string queryString = "Select Klient.Nazwisko From Klient";
            string connectionString = ($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={GetDatabaseFilePath()};Integrated Security=True;Connect Timeout=30");

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
}
