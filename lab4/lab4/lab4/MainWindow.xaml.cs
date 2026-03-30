using System;
using System.Windows;

namespace lab4 
{
    public partial class MainWindow : Window
    {
        DbAccess db = new DbAccess();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            list.ItemsSource = db.GetClientsTable().DefaultView;
            if (list.Items.Count > 0) list.SelectedIndex = 0;
            list.Focus();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Перевірка на порожні поля
                if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Будь ласка, заповніть ID та Ім'я!");
                    return;
                }

                db.AddClient(
                    Convert.ToInt32(txtId.Text),
                    txtName.Text,
                    txtPhone.Text,
                    txtAddress.Text,
                    txtOrderAmount.Text // передаємо текст, метод сам виправить крапку
                );

                RefreshData();
                MessageBox.Show("Клієнта додано!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.UpdateClient(
                    Convert.ToInt32(txtId.Text),
                    txtName.Text,
                    txtPhone.Text,
                    txtAddress.Text,
                    txtOrderAmount.Text
                );
                RefreshData();
                MessageBox.Show("Дані оновлено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка оновлення: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtId.Text);
                db.DeleteClient(id);
                RefreshData();
                MessageBox.Show("Запис видалено!");
            }
            catch (Exception ex) { MessageBox.Show("Помилка: " + ex.Message); }
        }
    }
}