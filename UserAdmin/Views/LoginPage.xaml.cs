using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserAdmin.Services;

namespace UserAdmin.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly UserDbService _userDbService = new();


        public LoginPage()
        {
            InitializeComponent();
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var email = EmailBox.Text.Trim();
            var password = PasswordBoxInput.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ShowError("Érvénytelen email vagy jelszó!");
            }

            var user = _userDbService.FindByEmail(email);

            if (user is null)
            {
                ShowError("Nem regisztrált tag.");
                return;
            }
            else if (user.Email == email && user.Password == password)
            {
                NavigationService.Navigate(new MembersPage());
            }
            else
            {
                ShowError("Nem megfelelő email vagy jelszó.");
                return;
            }
        }

        private void RegisterLink_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new RegisterPage());

        }

        private void ShowError(string message)
        {
            ErrorText.Text = message;
            ErrorText.Visibility = Visibility.Visible;
        
        }
    }
}
