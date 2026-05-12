using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Data;

namespace LogisticsWarehouse.Repositories
{
    public interface IIndustryGroupRepository
    {
        bool AddIndustryGroup(IndustryGroup group);
        List<IndustryGroup> GetAllIndustryGroups();
        IndustryGroup GetIndustryGroupById(int id);
        bool UpdateIndustryGroup(IndustryGroup group);
        bool DeleteIndustryGroup(int id);
    }

    public class IndustryGroupRepository : IIndustryGroupRepository
    {
        private DatabaseConnection _db = new DatabaseConnection();

        public bool AddIndustryGroup(IndustryGroup group)
        {
            try
            {
                string query = "INSERT INTO IndustryGroup (Name, Description) VALUES (@Name, @Description)";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", group.Name);
                        cmd.Parameters.AddWithValue("@Description", group.Description ?? (object)DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding industry group: " + ex.Message);
                return false;
            }
        }

        public List<IndustryGroup> GetAllIndustryGroups()
        {
            List<IndustryGroup> groups = new List<IndustryGroup>();

            try
            {
                string query = "SELECT * FROM IndustryGroup";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                groups.Add(new IndustryGroup
                                {
                                    IndustryGroupId = (int)reader["Industry_group_id"],
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting industry groups: " + ex.Message);
            }

            return groups;
        }

        public IndustryGroup GetIndustryGroupById(int id)
        {
            IndustryGroup group = null;

            try
            {
                string query = "SELECT * FROM IndustryGroup WHERE Industry_group_id = @Id";

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
                                group = new IndustryGroup
                                {
                                    IndustryGroupId = (int)reader["Industry_group_id"],
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting industry group: " + ex.Message);
            }

            return group;
        }

        public bool UpdateIndustryGroup(IndustryGroup group)
        {
            try
            {
                string query = "UPDATE IndustryGroup SET Name = @Name, Description = @Description WHERE Industry_group_id = @Id";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", group.IndustryGroupId);
                        cmd.Parameters.AddWithValue("@Name", group.Name);
                        cmd.Parameters.AddWithValue("@Description", group.Description ?? (object)DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating industry group: " + ex.Message);
                return false;
            }
        }

        public bool DeleteIndustryGroup(int id)
        {
            try
            {
                string query = "DELETE FROM IndustryGroup WHERE Industry_group_id = @Id";

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
                Console.WriteLine("Error deleting industry group: " + ex.Message);
                return false;
            }
        }
    }
}