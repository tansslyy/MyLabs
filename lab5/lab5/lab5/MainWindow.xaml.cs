using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace lab5
{
    public partial class MainWindow : Window
    {
        ClientsDB_Lab5Entities context;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context = new ClientsDB_Lab5Entities();

            ClientsGrid.ItemsSource = context.Clients.ToList();
            CompaniesGrid.ItemsSource = context.Companies.ToList();

            LoadJoinedTable();
            LoadProfitableClients();
        }

        private void LoadJoinedTable()
        {
            var joined = context.Clients.Join(context.Companies,
                client => client.CompanyId,
                company => company.Id,
                (client, company) => new
                {
                    client.Id,
                    client.Name,
                    client.Phone,
                    Компанія = company.CompanyName,
                    client.Income,
                    client.Expenses
                }).ToList();

            JoinedGrid.ItemsSource = joined;
        }

        //  Пошук (LINQ Where)
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim().ToLower();

            var results = context.Clients
                .Where(c => c.Name.ToLower().Contains(searchText))
                .ToList();

            SearchGrid.ItemsSource = results;
        }

        // Фільтрація за умовою 
        private void LoadProfitableClients()
        {
            var profitable = context.Clients
                .Where(c => c.Income > c.Expenses)
                .ToList();

            ProfitableGrid.ItemsSource = profitable;
        }
    }
}