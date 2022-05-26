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
            




            //foreach (Window window in Application.Current.Windows)
            //{
            //    if (window.GetType() == typeof(Window1))
            //    {
            //        this.ListView.ItemsSource = (window as Window1).ListViewUsers.Items;
            //    }
            //}
        }
    }
}
