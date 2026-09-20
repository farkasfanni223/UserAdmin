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
    /// Interaction logic for MembersPage.xaml
    /// </summary>
    public partial class MembersPage : Page
    {
        public MembersPage()
        {
            InitializeComponent();
        }

        private void ContactMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Kapcsolat:\nEmail:farkaszoltan28@gmail.com\nTelefon:+36703123884", "Kapcsolat", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void HelpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Súgó:\nAz 'Új tag' gombbal új felhasználót vehetsz fel\nA táblázat soraiban 'Szerkesztés'-sel módosíthatód, a'Törlés'-sel eltávolíthatod a tagot.\nA 'Kijelentkezés' gombbal visszatérhetsz a bejelentkező oldalra.", "Súgó", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MemberEditPage(_userDbService, null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var member = MembersGrid.SelectedItem as User;

            var user = new User
            {
                Id = member.Id,
                Username = member.Username,
                Email = member.Email,
                Password = member.Password
            };

            NavigationService.Navigate(new MemberEditPage(_userDbService, user));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var member = MembersGrid.SelectedItem as User;

            _userDbService.Delete(member.Id.ToString());

            MessageBox.Show("Sikeres törlés", "Törlés", MessageBoxButton.OK, MessageBoxImage.Information);

            MembersGrid.ItemsSource = _userDbService.GetAll();
        }
    }
}
