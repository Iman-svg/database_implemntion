using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Data;

namespace LogisticsWarehouse.Repositories
{
    public interface IManufacturerRepository
    {
        bool AddManufacturer(Manufacturer manufacturer);
        List<Manufacturer> GetAllManufacturers();
        Manufacturer GetManufacturerById(int id);
        bool UpdateManufacturer(Manufacturer manufacturer);
        bool DeleteManufacturer(int id);
    }

    public class ManufacturerRepository : IManufacturerRepository
    {
        private DatabaseConnection _db = new DatabaseConnection();

        public bool AddManufacturer(Manufacturer manufacturer)
        {
            try
            {
                string query = "INSERT INTO Manufacturer (Name, Contact_info, Country) VALUES (@Name, @ContactInfo, @Country)";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", manufacturer.Name);
                        cmd.Parameters.AddWithValue("@ContactInfo", manufacturer.ContactInfo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Country", manufacturer.Country ?? (object)DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding manufacturer: " + ex.Message);
                return false;
            }
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();

            try
            {
                string query = "SELECT * FROM Manufacturer";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                manufacturers.Add(new Manufacturer
                                {
                                    ManufacturerId = (int)reader["Manufacturer_id"],
                                    Name = reader["Name"].ToString(),
                                    ContactInfo = reader["Contact_info"].ToString(),
                                    Country = reader["Country"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting manufacturers: " + ex.Message);
            }

            return manufacturers;
        }

        public Manufacturer GetManufacturerById(int id)
        {
            Manufacturer manufacturer = null;

            try
            {
                string query = "SELECT * FROM Manufacturer WHERE Manufacturer_id = @Id";

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
                                manufacturer = new Manufacturer
                                {
                                    ManufacturerId = (int)reader["Manufacturer_id"],
                                    Name = reader["Name"].ToString(),
                                    ContactInfo = reader["Contact_info"].ToString(),
                                    Country = reader["Country"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting manufacturer: " + ex.Message);
            }

            return manufacturer;
        }

        public bool UpdateManufacturer(Manufacturer manufacturer)
        {
            try
            {
                string query = "UPDATE Manufacturer SET Name = @Name, Contact_info = @ContactInfo, Country = @Country WHERE Manufacturer_id = @Id";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", manufacturer.ManufacturerId);
                        cmd.Parameters.AddWithValue("@Name", manufacturer.Name);
                        cmd.Parameters.AddWithValue("@ContactInfo", manufacturer.ContactInfo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Country", manufacturer.Country ?? (object)DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating manufacturer: " + ex.Message);
                return false;
            }
        }

        public bool DeleteManufacturer(int id)
        {
            try
            {
                string query = "DELETE FROM Manufacturer WHERE Manufacturer_id = @Id";

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
                Console.WriteLine("Error deleting manufacturer: " + ex.Message);
                return false;
            }
        }
    }
}