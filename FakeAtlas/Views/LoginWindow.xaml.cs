using FakeAtlas.ViewModels;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FakeAtlas.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public SecureString SecurePassword { private get; set; }
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(this);
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
           SignUpFrame.Visibility = Visibility.Visible;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           SignUpFrame.Visibility = Visibility.Collapsed;
        }

        private void PressButton_Click(object sender, RoutedEventArgs e)
        {
            tbLogin.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
