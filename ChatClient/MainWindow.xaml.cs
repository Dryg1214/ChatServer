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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            
            var mainWindow = new Window1();
            mainWindow.Show();
            this.Close();
            //MessageBox.Show("Меня нажали я ЛОГИН");
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Меня нажали я Регистрация");
        }
    }
}
