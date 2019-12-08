using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace PI
{
    /// <summary>
    /// Логика взаимодействия для FindPage.xaml
    /// </summary>
    public partial class FindPage : Page
    {
        public FindPage()
        {
            InitializeComponent();
        }
        public FindPage(string Login)
        {
            InitializeComponent();
            this.Login = Login;
        }
        public string Login { get; set; }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CountAdult.SelectedIndex = 0;
            CountChild.SelectedIndex = 0;
            CountInfant.SelectedIndex = 0;
            DepartTaxi.Items.Clear();
            ArrivalTaxi.Items.Clear();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LocalDataBase;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT DISTINCT City FROM AddOrder Order by City";
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DepartTaxi.Items.Add(dr["City"]);
                        ArrivalTaxi.Items.Add(dr["City"]);
                    }
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SearchTaxi_Click(object sender, RoutedEventArgs e)
        {
            if (DepartTaxi.Text != "" && ArrivalTaxi.Text != "" && DatePicker.Text != "")
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
                    string query = $"SELECT Id,DepartCity,ArriveCity,convert(varchar(10),DepartDate,104) as 'DepartDate'," +
                        $"convert(varchar(10),ArriveDate,104) as 'ArrivalDate',CAST(DepartTime AS CHAR(5)) as 'DepartTime',CAST(ArriveTime AS CHAR(5)) as 'ArriveTime',TAXIXID as 'TaxiID',DriverName as 'Driver Phone Number' FROM InfoOrder " +
                        $"where DepartCity = '{DepartTaxi.Text}' AND ArriveCity = '{ArrivalTaxi.Text}' and convert(varchar(10),DepartDate,104) = '{Convert.ToDateTime(DatePicker.Text).ToString("dd.MM.yyyy")}'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dataGrid.ItemsSource = ds.Tables[0].DefaultView;
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            else
            {
                MessageBox.Show("Заповніть всі поля");
            }
        }

        private void ReserveTicket_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                try
                {
                    DataRowView row = dataGrid.SelectedItem as DataRowView;
                    Personal_Information personalInformationWindow = new Personal_Information(Login, Convert.ToInt32(row.Row.ItemArray[0].ToString()));
                    Class1.Clear();
                    personalInformationWindow.Show();

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }
    }
}
