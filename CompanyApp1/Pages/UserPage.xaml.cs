using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CompanyApp1.Pages
{
    public partial class UserPage : Page
    {
        private int userId;

        public UserPage(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadUserRequests();
        }

        private void LoadUserRequests()
        {
            using (var db = new CompanyDBEntities())
            {
                var userRequests = db.Requests
                    .Where(r => r.UserID == userId)
                    .ToList();

                RequestsDataGrid.ItemsSource = userRequests;
            }
        }

        private void CreateRequestButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MakeRequestPage(userId));
        }
    }
}