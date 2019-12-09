using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace PI
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void CloseProgram_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RegistationButton_Click(object sender, RoutedEventArgs e)

        {
            string email = EmailBlock.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);


            string password = PasswordBox.Password;
            Regex regex2 = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])\w{6,}$");
            Match match2 = regex2.Match(password);

            string login = LoginBlock.Text;
            Regex regex3 = new Regex(@"\w{4,}$");
            Match match3 = regex3.Match(login);

            if (match3.Success && match2.Success && match.Success && LoginBlock.Text != "" && EmailBlock.Text != "" && PasswordBox.Password != "")
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
                    string query = $"INSERT INTO Customer (Login,Password,Email) VALUES ('{LoginBlock.Text}', '{PasswordBox.Password}', '{EmailBlock.Text}');";


                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    MainWindow mainWindow = new MainWindow();
                    this.Visibility = Visibility.Hidden;
                    mainWindow.Show();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            else
            {
                MessageBox.Show("not valid email, login(at least four symbols) or password(at least one number, one lowercase and one uppercase letter" +
                    " at least six characters) ");

            }





        }
    }
}
