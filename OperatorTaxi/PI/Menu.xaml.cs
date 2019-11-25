﻿using System.Windows;
using System.Windows.Input;

namespace PI
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }
        public Menu(string login)
        {
            Login = login;
            InitializeComponent();
        }
        public string Login { get; set; }
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

        private void CabinetButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Cabinet(Login);
        }

       

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new FindPage(Login);
        }
        private void MyTicketsButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MyTickets(Login);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.Content = new FindPage(Login);
        }
    }
}
