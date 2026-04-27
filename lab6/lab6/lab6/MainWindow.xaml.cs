using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace lab6 // Змінили з великої літери на маленьку
{
    public partial class MainWindow : Window
    {
        // Змінна для підключення до бази 
        NotebookDBEntities context;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Завантаження вікна: ініціалізуємо БД і виводимо список
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context = new NotebookDBEntities();
            RefreshList();
        }

        // Метод для оновлення списку на екрані
        private void RefreshList()
        {
            NotesList.ItemsSource = context.Notes.ToList();
        }

        // ДОДАВАННЯ НОВОГО ЗАПИСУ
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NoteTextBox.Text))
            {
                MessageBox.Show("Будь ласка, введіть текст запису!");
                return;
            }

            // Створюємо об'єкт нового запису (використовуємо клас Notes та імена колонок з БД)
            Notes newNote = new Notes()
            {
                NoteText = NoteTextBox.Text,
                CreatedAt = DateTime.Now
            };

            // Додаємо в БД і зберігаємо
            context.Notes.Add(newNote);
            context.SaveChanges();

            NoteTextBox.Clear();
            RefreshList();
        }

        // ЗБЕРЕЖЕННЯ ЗМІН (РЕДАГУВАННЯ)
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Перевіряємо, чи вибрано якийсь запис зі списку
            if (NotesList.SelectedItem is Notes selectedNote) // Використовуємо Notes
            {
                // Оновлюємо текст і зберігаємо
                selectedNote.NoteText = NoteTextBox.Text;
                context.SaveChanges();
                RefreshList();
                MessageBox.Show("Зміни успішно збережено!");
            }
            else
            {
                MessageBox.Show("Виберіть запис зі списку для редагування.");
            }
        }

        // ВИДАЛЕННЯ ЗАПИСУ
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (NotesList.SelectedItem is Notes selectedNote) // Використовуємо Notes
            {
                // Видаляємо з БД
                context.Notes.Remove(selectedNote);
                context.SaveChanges();

                NoteTextBox.Clear();
                RefreshList();
            }
        }

        // ВІДОБРАЖЕННЯ ВИБРАНОГО ЗАПИСУ
        private void NotesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Коли клікаємо на запис зліва, його текст з'являється справа
            if (NotesList.SelectedItem is Notes selectedNote) // Використовуємо Notes
            {
                NoteTextBox.Text = selectedNote.NoteText;
            }
        }
    }
}