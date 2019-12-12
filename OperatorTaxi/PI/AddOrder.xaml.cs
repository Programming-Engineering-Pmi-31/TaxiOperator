using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;


namespace PI
{
    /// <summary>
    /// Клас AddOrder, створений для взаємодії з AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Page
    {
        public AddOrder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ConfimAddTaxi_Click функція, що створює дані про кінцевий пункт поїздки.
        /// </summary>
        /// <remarks>
        /// Сворює дані про кінцевий пункт в базі даних, попередньо перевіряючи валідність заповлення полів.
        /// </remarks>
        /// <param name="sender">Об'єкт.</param>
        /// <param name="e">Маршрутизована подія</param>


        private void ConfimAddTaxi_Click(object sender, RoutedEventArgs e)
        {
            if (AddNumBox.Text != "" && AddStreetBox.Text != "" && AddCityBox.Text != "")
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
                    string query = $"INSERT INTO AddOrder (city,seats,Phone) VALUES ('{AddNumBox.Text}', '{AddStreetBox.Text}', '{AddCityBox.Text}');";


                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    AddNumBox.Clear();
                    AddStreetBox.Clear();
                    AddCityBox.Clear();
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
