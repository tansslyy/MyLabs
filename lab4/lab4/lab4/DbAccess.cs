using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace lab4 
{
    public class DbAccess
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        // 1. Читання (Read)
        public DataTable GetClientsTable()
        {
            DataTable clientsTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Id, Name, Phone, Address, OrderAmount FROM Clients";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                try { adapter.Fill(clientsTable); }
                catch (Exception ex) { MessageBox.Show("Помилка читання: " + ex.Message); }
            }
            return clientsTable;
        }

        // Додавання (Create)
        public void AddClient(int id, string name, string phone, string address, string amountText)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                decimal orderAmount = Convert.ToDecimal(amountText.Replace(".", ","));

                string sql = "INSERT INTO Clients (Id, Name, Phone, Address, OrderAmount) VALUES (@Id, @Name, @Phone, @Address, @OrderAmount)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@OrderAmount", orderAmount);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Оновлення (Update)
        public void UpdateClient(int id, string name, string phone, string address, string amountText)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                decimal orderAmount = Convert.ToDecimal(amountText.Replace(".", ","));

                string sql = "UPDATE Clients SET Name=@Name, Phone=@Phone, Address=@Address, OrderAmount=@OrderAmount WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@OrderAmount", orderAmount);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // 4. Видалення (Delete)
        public void DeleteClient(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Clients WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Id", id);

                try { connection.Open(); cmd.ExecuteNonQuery(); }
                catch (Exception ex) { MessageBox.Show("Помилка видалення: " + ex.Message); }
            }
        }
    }
}