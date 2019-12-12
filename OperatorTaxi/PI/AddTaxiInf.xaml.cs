using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
/// /// Клас AddTaxiInf, створений для взаємодії з AddTaxiInf.xaml
/// </summary>

namespace PI
{

    public partial class OrderInfo : Page
    {
        public OrderInfo()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }
        /// <summary>
        /// Фу-ція UpdateItems витягує дані з БД для створення поїздки.
        /// </summary>
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
        /// <summary>
        /// ConfimAddTaxiInf_Click функція, що створює дані про нові поїздки.
        /// </summary>
        /// <remarks>
        /// Сворює дані про нову поїздку в базі даних, попередньо перевіряючи валідність заповлення полів.
        /// </remarks>
        /// <param name="sender">Об'єкт.</param>
        /// <param name="e">Маршрутизована подія</param>

        private void ConfimAddTaxiInf_Click(object sender, RoutedEventArgs e)
        {
            if (DepartCityComboBox.Text != "" && ArrivalCityComboBox.Text != "" && DepartDate.Text != "" && ArrivalDate.Text != "" &&
                DepartTimePicker.Text != "" && ArrivalTimePicker.Text != "" && TaxiIdComboBox.Text != "" && TaxBox.Text != "")
            {
                try
                {

                    string connectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
                    string query = $"INSERT INTO InfoOrder (DepartCity,ArriveCity,DepartDate,ArriveDate,DepartTime,ArriveTime,TAXIXID,DriverName) " +
                        $"VALUES ('{DepartCityComboBox.Text}','{ArrivalCityComboBox.Text}',CONVERT(date, '{DepartDate.Text}', 104),CONVERT(date, '{ArrivalDate.Text}', 104),'{DepartTimePicker.Text}','{ArrivalTimePicker.Text}','{TaxiIdComboBox.Text}','{TaxBox.Text}')";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    TaxBox.Clear();
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
