using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Data;

namespace LogisticsWarehouse.Repositories
{
    public interface ISectionRepository
    {
        bool AddSection(Section section);
        List<Section> GetAllSections();
        Section GetSectionById(int id);
        List<Section> GetSectionsByFacility(int facilityId);
        bool UpdateSection(Section section);
        bool DeleteSection(int id);
    }

    public class SectionRepository : ISectionRepository
    {
        private DatabaseConnection _db = new DatabaseConnection();

        public bool AddSection(Section section)
        {
            try
            {
                string query = "INSERT INTO Section (Facility_id, Section_name, Section_type, Capacity, Current_occupancy) " +
                               "VALUES (@FacilityId, @SectionName, @SectionType, @Capacity, @CurrentOccupancy)";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FacilityId", section.FacilityId);
                        cmd.Parameters.AddWithValue("@SectionName", section.SectionName);
                        cmd.Parameters.AddWithValue("@SectionType", section.SectionType);
                        cmd.Parameters.AddWithValue("@Capacity", section.Capacity);
                        cmd.Parameters.AddWithValue("@CurrentOccupancy", section.CurrentOccupancy);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding section: " + ex.Message);
                return false;
            }
        }

        public List<Section> GetAllSections()
        {
            List<Section> sections = new List<Section>();

            try
            {
                string query = "SELECT * FROM Section";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                sections.Add(new Section
                                {
                                    SectionId = (int)reader["Section_id"],
                                    FacilityId = (int)reader["Facility_id"],
                                    SectionName = reader["Section_name"].ToString(),
                                    SectionType = reader["Section_type"].ToString(),
                                    Capacity = (decimal)reader["Capacity"],
                                    CurrentOccupancy = (decimal)reader["Current_occupancy"]
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting sections: " + ex.Message);
            }

            return sections;
        }

        public Section GetSectionById(int id)
        {
            Section section = null;

            try
            {
                string query = "SELECT * FROM Section WHERE Section_id = @Id";

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
                                section = new Section
                                {
                                    SectionId = (int)reader["Section_id"],
                                    FacilityId = (int)reader["Facility_id"],
                                    SectionName = reader["Section_name"].ToString(),
                                    SectionType = reader["Section_type"].ToString(),
                                    Capacity = (decimal)reader["Capacity"],
                                    CurrentOccupancy = (decimal)reader["Current_occupancy"]
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting section: " + ex.Message);
            }

            return section;
        }

        public List<Section> GetSectionsByFacility(int facilityId)
        {
            List<Section> sections = new List<Section>();

            try
            {
                string query = "SELECT * FROM Section WHERE Facility_id = @FacilityId";

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
                                sections.Add(new Section
                                {
                                    SectionId = (int)reader["Section_id"],
                                    FacilityId = (int)reader["Facility_id"],
                                    SectionName = reader["Section_name"].ToString(),
                                    SectionType = reader["Section_type"].ToString(),
                                    Capacity = (decimal)reader["Capacity"],
                                    CurrentOccupancy = (decimal)reader["Current_occupancy"]
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting sections by facility: " + ex.Message);
            }

            return sections;
        }

        public bool UpdateSection(Section section)
        {
            try
            {
                string query = "UPDATE Section SET Facility_id = @FacilityId, Section_name = @SectionName, " +
                               "Section_type = @SectionType, Capacity = @Capacity, Current_occupancy = @CurrentOccupancy " +
                               "WHERE Section_id = @Id";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", section.SectionId);
                        cmd.Parameters.AddWithValue("@FacilityId", section.FacilityId);
                        cmd.Parameters.AddWithValue("@SectionName", section.SectionName);
                        cmd.Parameters.AddWithValue("@SectionType", section.SectionType);
                        cmd.Parameters.AddWithValue("@Capacity", section.Capacity);
                        cmd.Parameters.AddWithValue("@CurrentOccupancy", section.CurrentOccupancy);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating section: " + ex.Message);
                return false;
            }
        }

        public bool DeleteSection(int id)
        {
            try
            {
                string query = "DELETE FROM Section WHERE Section_id = @Id";

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
                Console.WriteLine("Error deleting section: " + ex.Message);
                return false;
            }
        }
    }
}