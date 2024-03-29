﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace PI
{
    /// <summary>
    /// Клас ChangeOrder, створений для взаємодії з ChangeOrder.xaml
    /// </summary>
    public partial class ChangeOrder : Page
    {
        public ChangeOrder()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ChangeOrderButton_Click функція, що дає змогу змінити дані поїздки.
        /// </summary>
        /// <param name="sender">Об'єкт.</param>
        /// <param name="e">Маршрутизована подія</param>
        private void ChangeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteDateGrid.SelectedItem != null)
            {
                try
                {
                    DataRowView row = DeleteDateGrid.SelectedItem as DataRowView;
                    string connectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
                    string query = $"Update InfoOrder " +
                        $"Set DepartDate = CONVERT(date, '{row.Row.ItemArray[3].ToString()}', 104), " +
                        $"ArriveDate = CONVERT(date, '{row.Row.ItemArray[4].ToString()}', 104), " +
                        $"DepartTime = '{row.Row.ItemArray[5].ToString()}'," +
                        $"ArriveTime = '{row.Row.ItemArray[6].ToString()}' " +
                        $"Where Id = {row.Row.ItemArray[0].ToString()}";
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
        /// <summary>
        /// Витягує дані з БД , про інформацію поїздки.
        /// </summary>
        /// <param name="sender">Об'єкт</param>
        /// <param name="e">Маршрутизована подія</param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
                string query = "SELECT Id,DepartCity as 'Depart City',ArriveCity as 'Arrive City',convert(varchar(10),DepartDate,104) as 'Depart Date'," +
                    "convert(varchar(10),ArriveDate,104) as 'Arrival Date',CAST(DepartTime AS CHAR(5)) as 'Depart Time',CAST(ArriveTime AS CHAR(5)) as 'Arrive Time',TAXIXID as 'Taxi Id',DriverName as 'Phone number'  FROM InfoOrder";
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
    }
}
