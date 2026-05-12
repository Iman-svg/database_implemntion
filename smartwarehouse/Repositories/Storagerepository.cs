using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Data;

namespace LogisticsWarehouse.Repositories
{
    public interface IStorageAgreementRepository
    {
        bool AddStorageAgreement(StorageAgreement agreement);
        List<StorageAgreement> GetAllStorageAgreements();
        StorageAgreement GetStorageAgreementById(int id);
        List<StorageAgreement> GetStorageAgreementsByClient(int clientId);
        bool UpdateStorageAgreement(StorageAgreement agreement);
        bool DeleteStorageAgreement(int id);
    }

    public class StorageAgreementRepository : IStorageAgreementRepository
    {
        private DatabaseConnection _db = new DatabaseConnection();

        public bool AddStorageAgreement(StorageAgreement agreement)
        {
            try
            {
                string query = "INSERT INTO StorageAgreement (Client_id, Facility_id, Product_id, Quantity, Start_date, End_date, Current_stock) " +
                               "VALUES (@ClientId, @FacilityId, @ProductId, @Quantity, @StartDate, @EndDate, @CurrentStock)";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClientId", agreement.ClientId);
                        cmd.Parameters.AddWithValue("@FacilityId", agreement.FacilityId);
                        cmd.Parameters.AddWithValue("@ProductId", agreement.ProductId);
                        cmd.Parameters.AddWithValue("@Quantity", agreement.Quantity);
                        cmd.Parameters.AddWithValue("@StartDate", agreement.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", agreement.EndDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CurrentStock", agreement.CurrentStock);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding storage agreement: " + ex.Message);
                return false;
            }
        }

        public List<StorageAgreement> GetAllStorageAgreements()
        {
            List<StorageAgreement> agreements = new List<StorageAgreement>();

            try
            {
                string query = "SELECT * FROM StorageAgreement";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                agreements.Add(new StorageAgreement
                                {
                                    AgreementId = (int)reader["Agreement_id"],
                                    ClientId = (int)reader["Client_id"],
                                    FacilityId = (int)reader["Facility_id"],
                                    ProductId = (int)reader["Product_id"],
                                    Quantity = (int)reader["Quantity"],
                                    StartDate = (DateTime)reader["Start_date"],
                                    EndDate = reader["End_date"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["End_date"],
                                    CurrentStock = (int)reader["Current_stock"]
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting storage agreements: " + ex.Message);
            }

            return agreements;
        }

        public StorageAgreement GetStorageAgreementById(int id)
        {
            StorageAgreement agreement = null;

            try
            {
                string query = "SELECT * FROM StorageAgreement WHERE Agreement_id = @Id";

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
                                agreement = new StorageAgreement
                                {
                                    AgreementId = (int)reader["Agreement_id"],
                                    ClientId = (int)reader["Client_id"],
                                    FacilityId = (int)reader["Facility_id"],
                                    ProductId = (int)reader["Product_id"],
                                    Quantity = (int)reader["Quantity"],
                                    StartDate = (DateTime)reader["Start_date"],
                                    EndDate = reader["End_date"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["End_date"],
                                    CurrentStock = (int)reader["Current_stock"]
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting storage agreement: " + ex.Message);
            }

            return agreement;
        }

        public List<StorageAgreement> GetStorageAgreementsByClient(int clientId)
        {
            List<StorageAgreement> agreements = new List<StorageAgreement>();

            try
            {
                string query = "SELECT * FROM StorageAgreement WHERE Client_id = @ClientId";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClientId", clientId);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                agreements.Add(new StorageAgreement
                                {
                                    AgreementId = (int)reader["Agreement_id"],
                                    ClientId = (int)reader["Client_id"],
                                    FacilityId = (int)reader["Facility_id"],
                                    ProductId = (int)reader["Product_id"],
                                    Quantity = (int)reader["Quantity"],
                                    StartDate = (DateTime)reader["Start_date"],
                                    EndDate = reader["End_date"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["End_date"],
                                    CurrentStock = (int)reader["Current_stock"]
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting storage agreements by client: " + ex.Message);
            }

            return agreements;
        }

        public bool UpdateStorageAgreement(StorageAgreement agreement)
        {
            try
            {
                string query = "UPDATE StorageAgreement SET Client_id = @ClientId, Facility_id = @FacilityId, Product_id = @ProductId, " +
                               "Quantity = @Quantity, Start_date = @StartDate, End_date = @EndDate, Current_stock = @CurrentStock " +
                               "WHERE Agreement_id = @Id";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", agreement.AgreementId);
                        cmd.Parameters.AddWithValue("@ClientId", agreement.ClientId);
                        cmd.Parameters.AddWithValue("@FacilityId", agreement.FacilityId);
                        cmd.Parameters.AddWithValue("@ProductId", agreement.ProductId);
                        cmd.Parameters.AddWithValue("@Quantity", agreement.Quantity);
                        cmd.Parameters.AddWithValue("@StartDate", agreement.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", agreement.EndDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CurrentStock", agreement.CurrentStock);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating storage agreement: " + ex.Message);
                return false;
            }
        }

        public bool DeleteStorageAgreement(int id)
        {
            try
            {
                string query = "DELETE FROM StorageAgreement WHERE Agreement_id = @Id";

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
                Console.WriteLine("Error deleting storage agreement: " + ex.Message);
                return false;
            }
        }
    }
}