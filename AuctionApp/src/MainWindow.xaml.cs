using AuctionApp.src.database;
using AuctionApp.src.model;
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
using AuctionApp.src.util;
using System.Windows.Navigation;
using AuctionApp.src.pages;

namespace AuctionApp.src
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private User CurrentUser { get; set; }
        private DatabaseAccessor DatabaseAccessor = new DatabaseAccessor();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            LoginBox.BorderBrush = new SolidColorBrush(Colors.White);
            PasswordBox.BorderBrush = new SolidColorBrush(Colors.White);

            if (LoginBox.Text.Length == 0 || PasswordBox.Text.Length == 0) {
                LoginBox.BorderBrush = new SolidColorBrush(Colors.Red);
                PasswordBox.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }

            User user = User.GetUser(DatabaseAccessor, LoginBox.Text);
            if (user == null)
            {
                LoginBox.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }
            if (!SecretHasher.Verify(PasswordBox.Text, user.GetPassword()))
            {
                PasswordBox.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }

            NavigationService.GetNavigationService(this).Navigate(new MainMenuPage(user));

        }

        private string Hash(string text)
        {
            return SecretHasher.Hash(text);
        }
    }
}
