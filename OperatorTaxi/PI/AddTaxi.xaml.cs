using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;

namespace PI
{
    
    public partial class AddTaxiUser : Page
    {
        public AddTaxiUser()
        {
            InitializeComponent();
        }

        private void ConfimAddTaxiUser_Click(object sender, RoutedEventArgs e)
        {
            if (AddTAXIXID.Text != "" && AddTaxiName.Text != "" && Passengers.Text != "" && BusinessClass.Text != "" && EconomClass.Text != "")
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
                    string query = $"INSERT INTO Taxi (Id,Model,Passengers,Business,Econom) " +
                        $"VALUES ('{AddTAXIXID.Text}', '{AddTaxiName.Text}', '{Passengers.Text}', '{BusinessClass.Text}', '{EconomClass.Text}');";


                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    AddTAXIXID.Clear();
                    AddTaxiName.Clear();
                    Passengers.Clear();
                    BusinessClass.Clear();
                    EconomClass.Clear();
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
