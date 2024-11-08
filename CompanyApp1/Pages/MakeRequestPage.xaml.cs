using System;
using System.Windows;
using System.Windows.Controls;

namespace CompanyApp1.Pages
{
    public partial class MakeRequestPage : Page
    {
        private int userId; // ID пользователя

        public MakeRequestPage(int userId)
        {
            InitializeComponent();
            this.userId = userId; // Сохраняем ID пользователя
        }

        private void CreateRequestButton_Click(object sender, RoutedEventArgs e)
        {
            string equipment = EquipmentTextBox.Text;
            string faultType = FaultTypeTextBox.Text;
            string problemDescription = ProblemDescriptionTextBox.Text;

            if (string.IsNullOrEmpty(equipment) || string.IsNullOrEmpty(faultType) || string.IsNullOrEmpty(problemDescription))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var db = new CompanyDBEntities()) // Замените на ваш контекст базы данных
            {
                var newRequest = new Requests // Изменено с Request на Requests
                {
                    Equipment = equipment,
                    FaultType = faultType,
                    ProblemDescription = problemDescription,
                    UserID = userId, // Автоматически назначаем ID пользователя
                    RequestDate = DateTime.Now,
                    Status = "В ожидании" // Статус по умолчанию
                };

                db.Requests.Add(newRequest);
                db.SaveChanges();

                MessageBox.Show("Заявка успешно создана!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack(); // Возвращаемся на предыдущую страницу
            }
        }
    }
}