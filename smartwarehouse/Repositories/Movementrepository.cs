using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Data;

namespace LogisticsWarehouse.Repositories
{
    public interface IMovementRepository
    {
        bool AddMovement(Movement movement);
        List<Movement> GetAllMovements();
        Movement GetMovementById(int id);
        List<Movement> GetMovementsByEmployee(int employeeId);
        List<Movement> GetMovementsByProduct(int productId);
        List<Movement> GetMovementsByFacility(int facilityId);
        bool DeleteMovement(int id);
    }

    public class MovementRepository : IMovementRepository
    {
        private DatabaseConnection _db = new DatabaseConnection();

        public bool AddMovement(Movement movement)
        {
            try
            {
                string query = "INSERT INTO Movement (Product_id, Source_section_id, Destination_section_id, Employee_id, Quantity_moved, Timestamp, Notes) " +
                               "VALUES (@ProductId, @SourceSectionId, @DestinationSectionId, @EmployeeId, @QuantityMoved, @Timestamp, @Notes)";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", movement.ProductId);
                        cmd.Parameters.AddWithValue("@SourceSectionId", movement.SourceSectionId);
                        cmd.Parameters.AddWithValue("@DestinationSectionId", movement.DestinationSectionId);
                        cmd.Parameters.AddWithValue("@EmployeeId", movement.EmployeeId);
                        cmd.Parameters.AddWithValue("@QuantityMoved", movement.QuantityMoved);
                        cmd.Parameters.AddWithValue("@Timestamp", movement.Timestamp);
                        cmd.Parameters.AddWithValue("@Notes", movement.Notes ?? (object)DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding movement: " + ex.Message);
                return false;
            }
        }

        public List<Movement> GetAllMovements()
        {
            List<Movement> movements = new List<Movement>();

            try
            {
                string query = "SELECT * FROM Movement ORDER BY Timestamp DESC";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                movements.Add(new Movement
                                {
                                    MovementId = (int)reader["Movement_id"],
                                    ProductId = (int)reader["Product_id"],
                                    SourceSectionId = (int)reader["Source_section_id"],
                                    DestinationSectionId = (int)reader["Destination_section_id"],
                                    EmployeeId = (int)reader["Employee_id"],
                                    QuantityMoved = (int)reader["Quantity_moved"],
                                    Timestamp = (DateTime)reader["Timestamp"],
                                    Notes = reader["Notes"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting movements: " + ex.Message);
            }

            return movements;
        }

        public Movement GetMovementById(int id)
        {
            Movement movement = null;

            try
            {
                string query = "SELECT * FROM Movement WHERE Movement_id = @Id";

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
                                movement = new Movement
                                {
                                    MovementId = (int)reader["Movement_id"],
                                    ProductId = (int)reader["Product_id"],
                                    SourceSectionId = (int)reader["Source_section_id"],
                                    DestinationSectionId = (int)reader["Destination_section_id"],
                                    EmployeeId = (int)reader["Employee_id"],
                                    QuantityMoved = (int)reader["Quantity_moved"],
                                    Timestamp = (DateTime)reader["Timestamp"],
                                    Notes = reader["Notes"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting movement: " + ex.Message);
            }

            return movement;
        }

        // Requirement: Get movements by specific employee
        public List<Movement> GetMovementsByEmployee(int employeeId)
        {
            List<Movement> movements = new List<Movement>();

            try
            {
                string query = "SELECT * FROM Movement WHERE Employee_id = @EmployeeId ORDER BY Timestamp DESC";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                movements.Add(new Movement
                                {
                                    MovementId = (int)reader["Movement_id"],
                                    ProductId = (int)reader["Product_id"],
                                    SourceSectionId = (int)reader["Source_section_id"],
                                    DestinationSectionId = (int)reader["Destination_section_id"],
                                    EmployeeId = (int)reader["Employee_id"],
                                    QuantityMoved = (int)reader["Quantity_moved"],
                                    Timestamp = (DateTime)reader["Timestamp"],
                                    Notes = reader["Notes"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting movements by employee: " + ex.Message);
            }

            return movements;
        }

        // Requirement: Get movements by specific product
        public List<Movement> GetMovementsByProduct(int productId)
        {
            List<Movement> movements = new List<Movement>();

            try
            {
                string query = "SELECT * FROM Movement WHERE Product_id = @ProductId ORDER BY Timestamp DESC";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                movements.Add(new Movement
                                {
                                    MovementId = (int)reader["Movement_id"],
                                    ProductId = (int)reader["Product_id"],
                                    SourceSectionId = (int)reader["Source_section_id"],
                                    DestinationSectionId = (int)reader["Destination_section_id"],
                                    EmployeeId = (int)reader["Employee_id"],
                                    QuantityMoved = (int)reader["Quantity_moved"],
                                    Timestamp = (DateTime)reader["Timestamp"],
                                    Notes = reader["Notes"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting movements by product: " + ex.Message);
            }

            return movements;
        }

        // Requirement: Get movements by facility (uses JOIN with Section table)
        public List<Movement> GetMovementsByFacility(int facilityId)
        {
            List<Movement> movements = new List<Movement>();

            try
            {
                // JOIN to get movements where source section belongs to this facility
                string query = "SELECT m.* FROM Movement m " +
                               "JOIN Section s ON m.Source_section_id = s.Section_id " +
                               "WHERE s.Facility_id = @FacilityId ORDER BY m.Timestamp DESC";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FacilityId", facilityId);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                movements.Add(new Movement
                                {
                                    MovementId = (int)reader["Movement_id"],
                                    ProductId = (int)reader["Product_id"],
                                    SourceSectionId = (int)reader["Source_section_id"],
                                    DestinationSectionId = (int)reader["Destination_section_id"],
                                    EmployeeId = (int)reader["Employee_id"],
                                    QuantityMoved = (int)reader["Quantity_moved"],
                                    Timestamp = (DateTime)reader["Timestamp"],
                                    Notes = reader["Notes"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting movements by facility: " + ex.Message);
            }

            return movements;
        }

        public bool DeleteMovement(int id)
        {
            try
            {
                string query = "DELETE FROM Movement WHERE Movement_id = @Id";

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
                Console.WriteLine("Error deleting movement: " + ex.Message);
                return false;
            }
        }
    }
}