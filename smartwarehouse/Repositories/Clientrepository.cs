using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Data;

namespace LogisticsWarehouse.Repositories
{
    public interface IClientRepository
    {
        bool AddClient(Client client);
        List<Client> GetAllClients();
        Client GetClientById(int id);
        bool UpdateClient(Client client);
        bool DeleteClient(int id);
    }

    public class ClientRepository : IClientRepository
    {
        private DatabaseConnection _db = new DatabaseConnection();

        public bool AddClient(Client client)
        {
            try
            {
                string query = "INSERT INTO Client (Name, Contact_person, Email, Phone_number, Business_address) " +
                               "VALUES (@Name, @ContactPerson, @Email, @PhoneNumber, @BusinessAddress)";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", client.Name);
                        cmd.Parameters.AddWithValue("@ContactPerson", client.ContactPerson ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", client.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@BusinessAddress", client.BusinessAddress ?? (object)DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding client: " + ex.Message);
                return false;
            }
        }

        public List<Client> GetAllClients()
        {
            List<Client> clients = new List<Client>();

            try
            {
                string query = "SELECT * FROM Client";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clients.Add(new Client
                                {
                                    ClientId = (int)reader["Client_id"],
                                    Name = reader["Name"].ToString(),
                                    ContactPerson = reader["Contact_person"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    PhoneNumber = reader["Phone_number"].ToString(),
                                    BusinessAddress = reader["Business_address"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting clients: " + ex.Message);
            }

            return clients;
        }

        public Client GetClientById(int id)
        {
            Client client = null;

            try
            {
                string query = "SELECT * FROM Client WHERE Client_id = @Id";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                client = new Client
                                {
                                    ClientId = (int)reader["Client_id"],
                                    Name = reader["Name"].ToString(),
                                    ContactPerson = reader["Contact_person"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    PhoneNumber = reader["Phone_number"].ToString(),
                                    BusinessAddress = reader["Business_address"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting client: " + ex.Message);
            }

            return client;
        }

        public bool UpdateClient(Client client)
        {
            try
            {
                string query = "UPDATE Client SET Name = @Name, Contact_person = @ContactPerson, Email = @Email, " +
                               "Phone_number = @PhoneNumber, Business_address = @BusinessAddress WHERE Client_id = @Id";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", client.ClientId);
                        cmd.Parameters.AddWithValue("@Name", client.Name);
                        cmd.Parameters.AddWithValue("@ContactPerson", client.ContactPerson ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", client.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@BusinessAddress", client.BusinessAddress ?? (object)DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating client: " + ex.Message);
                return false;
            }
        }

        public bool DeleteClient(int id)
        {
            try
            {
                string query = "DELETE FROM Client WHERE Client_id = @Id";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting client: " + ex.Message);
                return false;
            }
        }
    }
}