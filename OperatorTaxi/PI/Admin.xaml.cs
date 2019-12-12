using System.Windows;
using System.Windows.Input;

namespace PI
{
    /// <summary>
    /// Клас Admin це навігація сторінки адміністратора.
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
            Main.Content = new DeleteOrder();
        }

        private void ChangeInfoOrder_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ChangeOrder();
        }

        private void AddAddOrder_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddOrder();
        }

        private void AddInfoOrder_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new OrderInfo();
        }

        private void AddTaxiButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddTaxi();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.Content = new OrderInfo();
        }
    }
}
