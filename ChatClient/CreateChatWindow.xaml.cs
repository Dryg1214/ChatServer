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
    /// Логика взаимодействия для CreateChatWindow.xaml
    /// </summary>
    public partial class CreateChatWindow : Window
    {
        public CreateChatWindow()
        {
            InitializeComponent();

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    this.ListView.ItemsSource = (window as MainWindow).ListViewUsers.Items;
                }
            }
        }

        //public CreateChatWindow(MainViewModel win)
        //{
        //    InitializeComponent();

        //    wind = win;

        //    foreach (Window window in Application.Current.Windows)
        //    {
        //        if (window.GetType() == typeof(Window1))
        //        {
        //            this.ListView.ItemsSource = (window as Window1).ListViewUsers.Items;
        //        }
        //    }
        //}

        //
        public delegate void EnteredUsers(string Users);
        public event EnteredUsers? Notify;
        private void CreatChat(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainViewModel wind = new MainViewModel();
                Notify = wind.AddChat; //подписать на событие
                Notify(TextBoxUsers.Text);
                this.Close();

            }
        }
    }
}
