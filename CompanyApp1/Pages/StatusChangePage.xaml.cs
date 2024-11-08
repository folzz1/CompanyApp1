using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CompanyApp1.Pages
{
    public partial class StatusChangePage : Page
    {
        private int requestId; // ID заявки

        public StatusChangePage(int requestId)
        {
            InitializeComponent();
            this.requestId = requestId; // Сохраняем ID заявки
            LoadRequestDetails();
        }

        private void LoadRequestDetails()
        {
            using (var db = new CompanyDBEntities()) // Замените на ваш контекст базы данных
            {
                var request = db.Requests.FirstOrDefault(r => r.RequestID == requestId);
                if (request != null)
                {
                    RequestIdTextBlock.Text = request.RequestID.ToString();
                    EquipmentTextBlock.Text = request.Equipment;
                    FaultTypeTextBlock.Text = request.FaultType;
                    ProblemDescriptionTextBlock.Text = request.ProblemDescription;
                    StatusTextBlock.Text = request.Status;

                    // Устанавливаем текущий статус в ComboBox
                    StatusComboBox.SelectedItem = StatusComboBox.Items
                        .Cast<ComboBoxItem>()
                        .FirstOrDefault(item => item.Content.ToString() == request.Status);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new CompanyDBEntities()) // Замените на ваш контекст базы данных
            {
                var request = db.Requests.FirstOrDefault(r => r.RequestID == requestId);
                if (request != null)
                {
                    // Обновляем статус заявки
                    request.Status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                    db.SaveChanges();

                    MessageBox.Show("Статус заявки успешно изменен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack(); // Возвращаемся на предыдущую страницу
                }
            }
        }
    }
}