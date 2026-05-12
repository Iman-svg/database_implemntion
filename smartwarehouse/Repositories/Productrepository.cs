using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Data;

namespace LogisticsWarehouse.Repositories
{
    public interface IProductRepository
    {
        bool AddProduct(Product product);
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        List<Product> GetProductsByManufacturer(int manufacturerId);
        List<Product> GetProductsByIndustryGroup(int industryGroupId);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int id);
    }

    public class ProductRepository : IProductRepository
    {
        private DatabaseConnection _db = new DatabaseConnection();

        public bool AddProduct(Product product)
        {
            try
            {
                string query = "INSERT INTO Product (Name, Length, Width, Height, Weight, Manufacturer_id, Industry_group_id, Description) " +
                               "VALUES (@Name, @Length, @Width, @Height, @Weight, @ManufacturerId, @IndustryGroupId, @Description)";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", product.Name);
                        cmd.Parameters.AddWithValue("@Length", product.Length);
                        cmd.Parameters.AddWithValue("@Width", product.Width);
                        cmd.Parameters.AddWithValue("@Height", product.Height);
                        cmd.Parameters.AddWithValue("@Weight", product.Weight);
                        cmd.Parameters.AddWithValue("@ManufacturerId", product.ManufacturerId);
                        cmd.Parameters.AddWithValue("@IndustryGroupId", product.IndustryGroupId);
                        cmd.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding product: " + ex.Message);
                return false;
            }
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            try
            {
                string query = "SELECT * FROM Product";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new Product
                                {
                                    ProductId = (int)reader["Product_id"],
                                    Name = reader["Name"].ToString(),
                                    Length = (decimal)reader["Length"],
                                    Width = (decimal)reader["Width"],
                                    Height = (decimal)reader["Height"],
                                    Weight = (decimal)reader["Weight"],
                                    ManufacturerId = (int)reader["Manufacturer_id"],
                                    IndustryGroupId = (int)reader["Industry_group_id"],
                                    Description = reader["Description"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting products: " + ex.Message);
            }

            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = null;

            try
            {
                string query = "SELECT * FROM Product WHERE Product_id = @Id";

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
                                product = new Product
                                {
                                    ProductId = (int)reader["Product_id"],
                                    Name = reader["Name"].ToString(),
                                    Length = (decimal)reader["Length"],
                                    Width = (decimal)reader["Width"],
                                    Height = (decimal)reader["Height"],
                                    Weight = (decimal)reader["Weight"],
                                    ManufacturerId = (int)reader["Manufacturer_id"],
                                    IndustryGroupId = (int)reader["Industry_group_id"],
                                    Description = reader["Description"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting product: " + ex.Message);
            }

            return product;
        }

        public List<Product> GetProductsByManufacturer(int manufacturerId)
        {
            List<Product> products = new List<Product>();

            try
            {
                string query = "SELECT * FROM Product WHERE Manufacturer_id = @ManufacturerId";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ManufacturerId", manufacturerId);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new Product
                                {
                                    ProductId = (int)reader["Product_id"],
                                    Name = reader["Name"].ToString(),
                                    Length = (decimal)reader["Length"],
                                    Width = (decimal)reader["Width"],
                                    Height = (decimal)reader["Height"],
                                    Weight = (decimal)reader["Weight"],
                                    ManufacturerId = (int)reader["Manufacturer_id"],
                                    IndustryGroupId = (int)reader["Industry_group_id"],
                                    Description = reader["Description"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting products by manufacturer: " + ex.Message);
            }

            return products;
        }

        public List<Product> GetProductsByIndustryGroup(int industryGroupId)
        {
            List<Product> products = new List<Product>();

            try
            {
                string query = "SELECT * FROM Product WHERE Industry_group_id = @IndustryGroupId";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IndustryGroupId", industryGroupId);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new Product
                                {
                                    ProductId = (int)reader["Product_id"],
                                    Name = reader["Name"].ToString(),
                                    Length = (decimal)reader["Length"],
                                    Width = (decimal)reader["Width"],
                                    Height = (decimal)reader["Height"],
                                    Weight = (decimal)reader["Weight"],
                                    ManufacturerId = (int)reader["Manufacturer_id"],
                                    IndustryGroupId = (int)reader["Industry_group_id"],
                                    Description = reader["Description"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting products by industry group: " + ex.Message);
            }

            return products;
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                string query = "UPDATE Product SET Name = @Name, Length = @Length, Width = @Width, Height = @Height, " +
                               "Weight = @Weight, Manufacturer_id = @ManufacturerId, Industry_group_id = @IndustryGroupId, " +
                               "Description = @Description WHERE Product_id = @Id";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", product.ProductId);
                        cmd.Parameters.AddWithValue("@Name", product.Name);
                        cmd.Parameters.AddWithValue("@Length", product.Length);
                        cmd.Parameters.AddWithValue("@Width", product.Width);
                        cmd.Parameters.AddWithValue("@Height", product.Height);
                        cmd.Parameters.AddWithValue("@Weight", product.Weight);
                        cmd.Parameters.AddWithValue("@ManufacturerId", product.ManufacturerId);
                        cmd.Parameters.AddWithValue("@IndustryGroupId", product.IndustryGroupId);
                        cmd.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating product: " + ex.Message);
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                string query = "DELETE FROM Product WHERE Product_id = @Id";

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
                Console.WriteLine("Error deleting product: " + ex.Message);
                return false;
            }
        }
    }
}