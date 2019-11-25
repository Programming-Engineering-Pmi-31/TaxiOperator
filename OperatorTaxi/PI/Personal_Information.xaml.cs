using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PI
{
    /// <summary>
    /// Логика взаимодействия для Personal_Information.xaml
    /// </summary>
    public partial class Personal_Information : Window
    {
        public Personal_Information()
        {
            InitializeComponent();
        }
        public Personal_Information(string Login, int IdInfoOrder)
        {
            this.IdInfoOrder = IdInfoOrder;
            this.Login = Login;
            InitializeComponent();
        }
        public string Login { get; set; }
        public int IdInfoOrder { get; set; }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void AddPassenger_Click(object sender, RoutedEventArgs e)
        {
            if (SecondName.Text != "" && EconomName.Text != "" && Document.Text != "" && Gender.Text != "" && BirthDate.Text != "" && Seating.Text != "")
            {
                var sum = Int32.Parse(new String(string.Join(" ", Seating.Text.Split(' ').ToList()[2]).Where(Char.IsDigit).ToArray()));
                string query = $"INSERT INTO PersonalInformation (InfoOrderId,SecondName,EconomName,Gender,BirthDate,Document,Seating,Login)" +
                        $"VALUES ('{IdInfoOrder}','{SecondName.Text}','{EconomName.Text}','{Gender.Text}',CONVERT(date, '{BirthDate.Text}', 104),'{Document.Text}','{string.Join(" ", Seating.Text.Split(' ').ToList().GetRange(0, 2))}','{Login}')";
                Class1.Add(query, sum);
                this.Visibility = Visibility.Hidden;
                Payment paymentWindow = new Payment(Login);
                paymentWindow.Show();
            }
            else
            {
                MessageBox.Show("Input all information");
            }
        }
    }
}
