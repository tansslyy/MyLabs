using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace lab4 // Назва вашого проєкту
{
    public class DbAccess
    {
        private DataTable clientsTable = null;

        public DataTable GetClientsTable()
        {
            if (clientsTable != null) return clientsTable;

            clientsTable = new DataTable();

            // БЕЗПЕЧНЕ ЗЧИТУВАННЯ:
            var configSettings = ConfigurationManager.ConnectionStrings["MyDbConnection"];

            // Якщо налаштування не знайдено, показуємо віконце і зупиняємо підключення
            if (configSettings == null)
            {
                MessageBox.Show("Помилка: Не знайдено 'MyDbConnection' у файлі App.config! Перевірте правильність написання назви.");
                return clientsTable;
            }

            string connectionString = configSettings.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandText = "SELECT Id, Name, Phone, Address, OrderAmount FROM Clients";

                try
                {
                    adapter.Fill(clientsTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка підключення до БД: " + ex.Message);
                }
            }

            return clientsTable;
        }
    }
}