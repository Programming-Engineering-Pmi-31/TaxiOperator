using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
namespace PI
{
    public partial class AddTaxiInf : Page
    {
        public AddTaxiInf()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }
        private void UpdateItems()
        {
            DepartCityComboBox.Items.Clear();
            ArrivalCityComboBox.Items.Clear();
            string connectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT DISTINCT City FROM AddOrder";
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DepartCityComboBox.Items.Add(dr["City"]);
                        ArrivalCityComboBox.Items.Add(dr["City"]);
                    }
                    dr.Close();
                    cmd.CommandText = "SELECT DISTINCT Id FROM Taxi";
                    SqlDataReader dr1 = cmd.ExecuteReader();
                    while (dr1.Read())
                    {
                        TaxiIdComboBox.Items.Add(dr1["Id"]);
                    }
                    dr1.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            UpdateItems();

        }

        private void ConfimAddTaxiInf_Click(object sender, RoutedEventArgs e)
        {
            if (DepartCityComboBox.Text != "" && ArrivalCityComboBox.Text != "" && DepartDate.Text != "" && ArrivalDate.Text != "" &&
                DepartTimePicker.Text != "" && ArrivalTimePicker.Text != "" && TaxiIdComboBox.Text != "" && AirlineBox.Text != "")
            {
                try
                {

                    string connectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
                    string query = $"INSERT INTO InfoOrder (DepartCity,ArriveCity,DepartDate,ArriveDate,DepartTime,ArriveTime,TAXIXID,DriverName) " +
                        $"VALUES ('{DepartCityComboBox.Text}','{ArrivalCityComboBox.Text}',CONVERT(date, '{DepartDate.Text}', 104),CONVERT(date, '{ArrivalDate.Text}', 104),'{DepartTimePicker.Text}','{ArrivalTimePicker.Text}','{TaxiIdComboBox.Text}','{AirlineBox.Text}')";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    AirlineBox.Clear();
                    DepartCityComboBox.Text = "";
                    ArrivalCityComboBox.Text = "";
                    DepartDate.Text = "";
                    ArrivalDate.Text = "";
                    DepartTimePicker.Text = "";
                    ArrivalTimePicker.Text = "";
                    TaxiIdComboBox.Text = "";
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            else
            {
                MessageBox.Show("Something wrong");
            }
        }
    }
}
