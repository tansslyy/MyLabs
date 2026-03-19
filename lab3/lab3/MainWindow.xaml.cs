using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace lab3
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> Notes { get; set; }

        public ICommand AddNoteCommand { get; set; }
        public ICommand ClearInputCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Notes = new ObservableCollection<string>();
            NotesList.ItemsSource = Notes;
            AddNoteCommand = new RelayCommand(ExecuteAddNote, CanExecuteAddNote);
            ClearInputCommand = new RelayCommand(ExecuteClearInput, CanExecuteClearInput);
            DeleteNoteCommand = new RelayCommand(ExecuteDeleteNote, CanExecuteDeleteNote);

            this.DataContext = this;
        }

        // Логіка "Додати"
        private bool CanExecuteAddNote(object parameter)
        {
            return !string.IsNullOrWhiteSpace(NoteInputBox.Text);
        }

        private void ExecuteAddNote(object parameter)
        {
 
            string time = DateTime.Now.ToString("HH:mm:ss");
            Notes.Insert(0, $"[{time}] {NoteInputBox.Text.Trim()}");
            NoteInputBox.Clear();
        }

        // Логіка "Очистити"
        private bool CanExecuteClearInput(object parameter)
        {
            return !string.IsNullOrWhiteSpace(NoteInputBox.Text);
        }

        private void ExecuteClearInput(object parameter)
        {
            NoteInputBox.Clear();
        }

        // Логіка "Видалити"
        private bool CanExecuteDeleteNote(object parameter)
        {
            return NotesList.SelectedIndex != -1;
        }

        private void ExecuteDeleteNote(object parameter)
        {
            if (NotesList.SelectedIndex != -1)
            {
                Notes.RemoveAt(NotesList.SelectedIndex);
            }
        }

        // Додатковий обробник події, щоб кнопки оновлювали свій стан при введенні тексту
        private void NoteInputBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }

        // Обробник, щоб оновлювати стан кнопки "Видалити" при виборі елемента списку
        private void NotesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }

  
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}