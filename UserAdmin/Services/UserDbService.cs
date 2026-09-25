using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Navigation;
using UserAdmin.Models;

namespace UserAdmin.Services
{
    public class UserDbService
    {
        public string ConnectionString = "Server=localhost;Database=useradmin;User=root;Password=;";


        public void Delete(string id)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            string sql = "DELETE FROM `users` WHERE id = @id";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            connection.Close();

        }
        public void Update(User user)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            MessageBox.Show(user.Id.ToString());

            string sql = @"UPDATE `users` SET `username`=@username,`email`=@email,`password`=@password WHERE id = @id;";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@id", user.Id);

            cmd.ExecuteNonQuery();

            connection.Close();

        }

        public void Add(User user)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            string sql = @"INSERT INTO `users`(`username`, `email`, `password`, `registeredAt`) 
VALUES (@Username,@Email,@Password,@RegisteredAt)";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@RegisteredAt", user.RegisteredAt);
            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public User? FindByEmail(string email)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            string sql = @"SELECT `username`, `email`, `password`, `registeredAt` FROM `users` WHERE email = @email";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@email", email);

            var reader = cmd.ExecuteReader();


            if (reader.Read())
            {
                var user = new User
                {
                    Username = reader.GetString(0),
                    Email = reader.GetString(1),
                    Password = reader.GetString(2),
                    RegisteredAt = reader.GetDateTime(3)
                };

                connection.Close();
                return user;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public List<User> GetAll()
        {
            var users = new List<User>();

            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            string sql = @"SELECT `id`,`username`, `email`, `password`, `registeredAt` FROM `users` ORDER BY RegisteredAt";

            var cmd = new MySqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var user = new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Email = reader.GetString(2),
                    Password = reader.GetString(3),
                    RegisteredAt = reader.GetDateTime(4)
                };

                users.Add(user);
            }

            connection.Close();

            return users;

        }
    }
}
