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
using UserAdmin.Models;
using UserAdmin.Services;

namespace UserAdmin.Views
{
    /// <summary>
    /// Interaction logic for MemberEditPage.xaml
    /// </summary>
    public partial class MemberEditPage : Page
    {
        private readonly UserDbService _userDbService;
        private readonly User? _originalUser;

        public MemberEditPage(UserDbService userDbService, User? existingUser)
        {
            InitializeComponent();
            _userDbService = userDbService;
            _originalUser = existingUser;

            if (existingUser is not null)
            {
                HeaderText.Text = "Tag szerkesztése";
                IdBox.Text = existingUser.Id.ToString();
                UsernameBox.Text = existingUser.Username;
                EmailBox.Text = existingUser.Email;
                PasswordBoxInput.Password = existingUser.Password;
            }
            else
            {
                HeaderText.Text = "Új tag felvétele";
                PasswordText.Text = "Jelszó";
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var id = IdBox.Text;
            var username = UsernameBox.Text.Trim();
            var email = EmailBox.Text.Trim();
            var password = PasswordBoxInput.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
            {
                ErrorText.Text = "Felhasználónév és email kötelező!";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            if (username.Contains(',') || email.Contains(','))
            {
                ErrorText.Text = "Felhasználónév és email nem tartalmazhat vesszőt!";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            var existingWithEmail = _userDbService.FindByEmail(email);

            var isExistingEmail = existingWithEmail is not null && (_originalUser is null || !string.Equals(existingWithEmail.Email, _originalUser.Email, StringComparison.OrdinalIgnoreCase));

            if (isExistingEmail == true)
            {
                ErrorText.Text = "Ez az email cím már foglalt!";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            if (_originalUser is null && string.IsNullOrWhiteSpace(password))
            {
                ErrorText.Text = "Új tagnál a jelszó megadása kötelező!";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            if (!string.IsNullOrWhiteSpace(password) && password.Length < 6)
            {
                ErrorText.Text = "A jelszónak legalább 6 karakter hosszúnak kell lennie!";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            var result = new User
            {
                Id = Convert.ToInt32(id),
                Username = username,
                Email = email,
                Password = password,
                RegisteredAt = DateTime.Now
            };

            if (_originalUser is null)
            {
                _userDbService.Add(result);
                MessageBox.Show("Sikeres mentés", "Mentés", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _userDbService.Update(result);
                MessageBox.Show("Sikeres frissítés", "Frissítés", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
