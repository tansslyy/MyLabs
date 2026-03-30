using System.Windows;

namespace lab4 // Назва вашого проєкту
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Метод для отримання контексту даних (Викликається при завантаженні вікна)
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Викликаємо клас, який ми створили в Завданні 2
            DbAccess db = new DbAccess();

            // Передаємо таблицю як контекст даних для нашого списку
            list.DataContext = db.GetClientsTable();

            // Якщо в базі є дані, автоматично виділяємо першого клієнта
            if (list.Items.Count > 0)
            {
                list.SelectedIndex = 0;
            }
            list.Focus();
        }
    }
}