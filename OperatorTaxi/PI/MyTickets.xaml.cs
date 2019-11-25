using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace PI
{
    /// <summary>
    /// Логика взаимодействия для MyTickets.xaml
    /// </summary>
    public partial class MyTickets : Page
    {
        public MyTickets()
        {
            InitializeComponent();
        }
        public MyTickets(string Login)
        {
            InitializeComponent();
            this.Login = Login;
        }

        public string Login { get; set; }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
                string query = $"SELECT SecondName as 'Second Name',EconomName as 'Econom Name',convert(varchar(10),BirthDate,104) as 'Birth Date'," +
                    $"DepartCity as 'Depart City',ArriveCity as 'Arrive City',convert(varchar(10),DepartDate,104) as 'Depart Date'," +
                    $"CAST(DepartTime AS CHAR(5)) as 'Depart Time',convert(varchar(10),ArriveDate,104) as 'Arrival Date',CAST(ArriveTime AS CHAR(5)) as 'Arrive Time',Seating " +
                    $"FROM InfoOrder Join PersonalInformation ON Id = InfoOrderId WHERE Login='{Login}' ORDER BY DepartDate,DepartTime,SecondName,EconomName";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    MyTicketsDataGrid.ItemsSource = ds.Tables[0].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
