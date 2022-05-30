using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChatClient.ViewModel;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string username;
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(string user)
        {
            InitializeComponent();
            username = user;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current != WindowState.Minimized)
            {
                Application.Current.WindowState = WindowState.Maximized;
                return;
            }
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            //else
            //  Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void PlusChatClick(object sender, MouseButtonEventArgs e)
        {
//((MainViewModel)DataContext).Chats
            var Window = new CreateChatWindow();
            Window.Show();
            
        }

    }
}
