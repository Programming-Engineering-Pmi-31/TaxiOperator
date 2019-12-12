using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace PI
{
    /// <summary>
    ///Клас DeleteOrder, створений для взаємодії з DeleteOrder.xaml
    /// </summary>
    public partial class DeleteOrder : Page
    {
        public DeleteOrder()
        {
            InitializeComponent();
        }
        /// <summary>
        /// UpdateButton_Click функція витягує дані з БД, про всі наявні поїздки.
        /// </summary>
        /// <param name="sender">Об'єкт</param>
        /// <param name="e">Маршштрутизована подія</param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
                string query = "SELECT Id,DepartCity as 'Depart City',ArriveCity as 'Arrive City',convert(varchar(10),DepartDate,104) as 'Depart Date'," +
                    "convert(varchar(10),ArriveDate,104) as 'Arrival Date',CAST(DepartTime AS CHAR(5)) as 'Depart Time',CAST(ArriveTime AS CHAR(5)) as 'Arrive Time',TAXIXID as 'Taxi Id',DriverName as 'Phone number' FROM InfoOrder";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    DeleteDateGrid.ItemsSource = ds.Tables[0].DefaultView;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        /// <summary>
        /// ConfirmDeleteOrderButton_Click функція, видаляє дані поїздки з БД.
        /// </summary>
        /// <param name="sender">об'єкт</param>
        /// <param name="e">Маршрутизована подія</param>
        private void ConfirmDeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteDateGrid.SelectedItem != null)
            {
                try
                {
                    DataRowView row = DeleteDateGrid.SelectedItem as DataRowView;
                    string connectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
                    string query = $"DELETE FROM InfoOrder WHERE ID = '{row.Row.ItemArray[0].ToString()}'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    UpdateButton_Click(sender, e);
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }
    }
}
