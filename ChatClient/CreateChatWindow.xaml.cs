using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
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
using ChatClient.Model;
using ChatClient.ViewModel;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для CreateChatWindow.xaml
    /// </summary>
    public partial class CreateChatWindow : Window
    {
        public ObservableCollection<UserModel> Chats;
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
        public CreateChatWindow(ObservableCollection<UserModel> chats)
        {
            InitializeComponent();

            Chats = chats;

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    this.ListView.ItemsSource = (window as MainWindow).ListViewUsers.Items;
                }
            }
        }
        
        public delegate void EnteredUsers(string Users);
        public event EnteredUsers? Notify;

        public string User { get; set; } = string.Empty;
        private void CreatChat(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                List<string> usersGroup = new List<string>();
                List<string> allUsers = new List<string>();
                var text = TextBoxUsers.Text.ToString().Split(" ");
                foreach (var user in text)
                    usersGroup.Add(user);
                for (int i = 0; i < ListView.Items.Count; i++)
                {
                    UserModel item = (UserModel)ListView.Items[i];
                    allUsers.Add(item.Username);
                }
                foreach (var user in usersGroup)
                {
                    if (!allUsers.Contains(user))
                    {
                        MessageBox.Show("Incorrect usernames, try again");
                        return;
                    }
                }

                Chats.Add(new UserModel
                {
                    Username = TextBoxUsers.Text
                });
                this.Close();

            }
        }
    }
}
