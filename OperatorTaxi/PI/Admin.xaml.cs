using System.Windows;
using System.Windows.Input;

namespace PI
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
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
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DeleteInfoOrder_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new DeleteTaxi();
        }

        private void ChangeInfoOrder_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ChangeTaxi();
        }

        private void AddAddOrder_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddTaxi();
        }

        private void AddInfoOrder_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddTaxiInf();
        }

        private void AddTaxiButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddTaxiUser();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddTaxiInf();
        }
    }
}
