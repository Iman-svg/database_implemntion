using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Data;

namespace LogisticsWarehouse.Repositories
{
    public class FacilityRepository : IFacilityRepository
    {
        private DatabaseConnection _db = new DatabaseConnection();

        // CREATE
        public bool AddFacility(Facility facility)
        {
            try
            {
                string query = "INSERT INTO Facility (Name, Zone, Climate_control, Total_capacity, Current_occupancy, Address) " +
                               "VALUES (@Name, @Zone, @ClimateControl, @TotalCapacity, @CurrentOccupancy, @Address)";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", facility.Name);
                        cmd.Parameters.AddWithValue("@Zone", facility.Zone);
                        cmd.Parameters.AddWithValue("@ClimateControl", facility.ClimateControl);
                        cmd.Parameters.AddWithValue("@TotalCapacity", facility.TotalCapacity);
                        cmd.Parameters.AddWithValue("@CurrentOccupancy", facility.CurrentOccupancy);
                        cmd.Parameters.AddWithValue("@Address", facility.Address);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding facility: " + ex.Message);
                return false;
            }
        }

        // READ ALL
        public List<Facility> GetAllFacilities()
        {
            List<Facility> facilities = new List<Facility>();

            try
            {
                string query = "SELECT * FROM Facility";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                facilities.Add(new Facility
                                {
                                    FacilityId = (int)reader["Facility_id"],
                                    Name = reader["Name"].ToString(),
                                    Zone = reader["Zone"].ToString(),
                                    ClimateControl = reader["Climate_control"].ToString(),
                                    TotalCapacity = (decimal)reader["Total_capacity"],
                                    CurrentOccupancy = (decimal)reader["Current_occupancy"],
                                    Address = reader["Address"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting facilities: " + ex.Message);
            }

            return facilities;
        }

        // READ BY ID
        public Facility GetFacilityById(int id)
        {
            Facility facility = null;

            try
            {
                string query = "SELECT * FROM Facility WHERE Facility_id = @Id";

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
                                facility = new Facility
                                {
                                    FacilityId = (int)reader["Facility_id"],
                                    Name = reader["Name"].ToString(),
                                    Zone = reader["Zone"].ToString(),
                                    ClimateControl = reader["Climate_control"].ToString(),
                                    TotalCapacity = (decimal)reader["Total_capacity"],
                                    CurrentOccupancy = (decimal)reader["Current_occupancy"],
                                    Address = reader["Address"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting facility: " + ex.Message);
            }

            return facility;
        }

        // UPDATE
        public bool UpdateFacility(Facility facility)
        {
            try
            {
                string query = "UPDATE Facility SET Name = @Name, Zone = @Zone, Climate_control = @ClimateControl, " +
                               "Total_capacity = @TotalCapacity, Current_occupancy = @CurrentOccupancy, Address = @Address " +
                               "WHERE Facility_id = @Id";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", facility.FacilityId);
                        cmd.Parameters.AddWithValue("@Name", facility.Name);
                        cmd.Parameters.AddWithValue("@Zone", facility.Zone);
                        cmd.Parameters.AddWithValue("@ClimateControl", facility.ClimateControl);
                        cmd.Parameters.AddWithValue("@TotalCapacity", facility.TotalCapacity);
                        cmd.Parameters.AddWithValue("@CurrentOccupancy", facility.CurrentOccupancy);
                        cmd.Parameters.AddWithValue("@Address", facility.Address);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating facility: " + ex.Message);
                return false;
            }
        }

        // DELETE
        public bool DeleteFacility(int id)
        {
            try
            {
                string query = "DELETE FROM Facility WHERE Facility_id = @Id";

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
                Console.WriteLine("Error deleting facility: " + ex.Message);
                return false;
            }
        }
    }
}