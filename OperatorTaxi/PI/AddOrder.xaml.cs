using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;


namespace PI
{
    public partial class AddTaxi : Page
    {
        public AddTaxi()
        {
            InitializeComponent();
        }

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
