using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.ComponentModel;
using System.Windows;
using System.Collections;
using WPFProjekt.UserControls;
using System.Collections.ObjectModel;
using System.IO;
using Path = System.IO.Path;
namespace WPFProjekt
{
 
    public class ViewModel : INotifyPropertyChanged
    {
        private Samochod selectedSamochod;
        private int iloscDni;

        public ViewModel()
        {
            this.Samochody = new ObservableCollection<Samochod>();
        }

        public ObservableCollection<Samochod> Samochody { get; set; }

        public Samochod SelectedSamochod
        {
            get { return this.selectedSamochod; }
            set
            {
                if (this.selectedSamochod != value)
                {
                    this.selectedSamochod = value;
                    OnPropertyChanged(nameof(SelectedSamochod));
                }
            }
        }

        public int IloscDni
        {
            get { return this.iloscDni; }
            set
            {
                if (this.iloscDni != value)
                {
                    this.iloscDni = value;
                    OnPropertyChanged(nameof(IloscDni));
            }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
            {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    public partial class Window2 : Window
    {
        private string GetDatabaseFilePath()
        {
            string databaseFileName = "Wypozyczalnia2.mdf";
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName).FullName;
            string databaseFilePath = Path.Combine(projectDirectory, databaseFileName);

            return databaseFilePath;
        }
        private System.Data.DataTable table;
        public Window2()
        {

            InitializeComponent();
            string queryString = "SELECT MarkaSamochodu  FROM  Samochod";

            string connectionString = ($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={GetDatabaseFilePath()};Integrated Security=True;Connect Timeout=30")    ;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
               
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        SamochodyInfo.Items.Add((reader["MarkaSamochodu"]));
                    }
                }
                finally
                {
                  
                    reader.Close();
                }
            }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string queryString = "Select MarkaSamochodu, ModelSamochodu, CenaZaDzien From Samochod";
            string connectionString = ($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={GetDatabaseFilePath()};Integrated Security=True;Connect Timeout=30");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    table = new DataTable();
                    table.Load(reader);

                   
                    Modele1.ItemsSource = table.DefaultView;

              
                    Modele1.AutoGenerateColumns = true;

                    
                    Modele1.CanUserAddRows = false;
                }
                finally
                {
                    reader.Close();
                }
            }
        }
        private void DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataRow selectedRow = table.Rows[index];
            string r = selectedRow.ToString();
            System.Windows.Forms.MessageBox.Show(r);
           

        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string selected = SamochodyInfo.SelectedItem.ToString();
            string queryString = "Select ModelSamochodu, CenaZaDzien From Samochod Where MarkaSamochodu = @MarkaSamochodu";
            string connectionString = ($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={GetDatabaseFilePath()};Integrated Security=True;Connect Timeout=30");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@MarkaSamochodu", selected);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    table = new DataTable();
                    table.Load(reader);

                  
                    Modele1.ItemsSource = table.DefaultView;

                   
                    Modele1.AutoGenerateColumns = true;

                 
                    Modele1.CanUserAddRows = false;
                }
                finally
                {
                    reader.Close();
                }
            }
        }
       
        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (Modele1.SelectedItem != null)
            {
                ((ViewModel)DataContext).SelectedSamochod = (Samochod)Modele1.SelectedItem;
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        { }

 
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            string selected = Modele1.SelectedItem.ToString();
            string queryString = "SELECT CenaZaDzien FROM Samochod ";
            string connectionString = ($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={GetDatabaseFilePath()};Integrated Security=True;Connect Timeout=30");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@CenaZaDzien", selected);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    table = new DataTable();
                    table.Load(reader);

                   
                    Models.ItemsSource = table.DefaultView;

                 
                    Models.AutoGenerateColumns = true;

                    Models.CanUserAddRows = false;
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        private void ComboBox_SelectionChanged12(object sender, SelectionChangedEventArgs e)
        {
          

                string selected = SamochodyInfo1.SelectedItem.ToString();
                string queryString = "Select ModelSamochodu From Samochod Where MarkaSamochodu = @MarkaSamochodu";
                string connectionString = ($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={GetDatabaseFilePath()};Integrated Security=True;Connect Timeout=30");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@MarkaSamochodu", selected);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    try
                    {
                        table = new DataTable();
                        table.Load(reader);

                     
                        Modele1.ItemsSource = table.DefaultView;

                        Modele1.AutoGenerateColumns = true;

                       
                        Modele1.CanUserAddRows = false;
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }
        
    }
   
     

       
    }
