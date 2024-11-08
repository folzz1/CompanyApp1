using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CompanyApp1.Pages
{
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void TextBoxLogin_Changed(object sender, RoutedEventArgs e)
        {
            // Логика для отображения подсказки, если необходимо
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Логика для отображения подсказки, если необходимо
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var db = new CompanyDBEntities()) // Замените на ваш контекст базы данных
            {
                var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (user.PasswordHash != password) // Здесь лучше использовать хеширование паролей
                {
                    MessageBox.Show("Неверный пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("Авторизация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                switch (user.Role)
                {
                    case "Клиент":
                        NavigationService.Navigate(new UserPage(user.UserID));
                        break;
                    case "Администратор":
                        NavigationService.Navigate(new AdminPage());
                        break;
                }
            }
        }
    }
}