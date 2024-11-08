using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CompanyApp1.Pages
{
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            LoadRequests();
        }

        private void LoadRequests()
        {
            using (var db = new CompanyDBEntities()) // Замените на ваш контекст базы данных
            {
                var requests = db.Requests.ToList(); // Получаем все заявки
                RequestsDataGrid.ItemsSource = requests; // Устанавливаем источник данных для DataGrid
            }
        }

        private void ChangeStatusButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var request = button?.DataContext as Requests;

            if (request != null)
            {
                NavigationService.Navigate(new StatusChangePage(request.RequestID));
            }
        }
    }
}