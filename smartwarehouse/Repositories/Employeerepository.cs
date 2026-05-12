using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Data;

namespace LogisticsWarehouse.Repositories
{
    public interface IEmployeeRepository
    {
        bool AddEmployee(Employee employee);
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(int id);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private DatabaseConnection _db = new DatabaseConnection();

        public bool AddEmployee(Employee employee)
        {
            try
            {
                string query = "INSERT INTO Employee (Name, Department, Position, Hire_date, Contact_info) " +
                               "VALUES (@Name, @Department, @Position, @HireDate, @ContactInfo)";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", employee.Name);
                        cmd.Parameters.AddWithValue("@Department", employee.Department ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Position", employee.Position ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@HireDate", employee.HireDate);
                        cmd.Parameters.AddWithValue("@ContactInfo", employee.ContactInfo ?? (object)DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding employee: " + ex.Message);
                return false;
            }
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                string query = "SELECT * FROM Employee";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employees.Add(new Employee
                                {
                                    EmployeeId = (int)reader["Employee_id"],
                                    Name = reader["Name"].ToString(),
                                    Department = reader["Department"].ToString(),
                                    Position = reader["Position"].ToString(),
                                    HireDate = (DateTime)reader["Hire_date"],
                                    ContactInfo = reader["Contact_info"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting employees: " + ex.Message);
            }

            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = null;

            try
            {
                string query = "SELECT * FROM Employee WHERE Employee_id = @Id";

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
                                employee = new Employee
                                {
                                    EmployeeId = (int)reader["Employee_id"],
                                    Name = reader["Name"].ToString(),
                                    Department = reader["Department"].ToString(),
                                    Position = reader["Position"].ToString(),
                                    HireDate = (DateTime)reader["Hire_date"],
                                    ContactInfo = reader["Contact_info"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting employee: " + ex.Message);
            }

            return employee;
        }

        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                string query = "UPDATE Employee SET Name = @Name, Department = @Department, Position = @Position, " +
                               "Hire_date = @HireDate, Contact_info = @ContactInfo WHERE Employee_id = @Id";

                using (SqlConnection conn = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", employee.EmployeeId);
                        cmd.Parameters.AddWithValue("@Name", employee.Name);
                        cmd.Parameters.AddWithValue("@Department", employee.Department ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Position", employee.Position ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@HireDate", employee.HireDate);
                        cmd.Parameters.AddWithValue("@ContactInfo", employee.ContactInfo ?? (object)DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating employee: " + ex.Message);
                return false;
            }
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                string query = "DELETE FROM Employee WHERE Employee_id = @Id";

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
                Console.WriteLine("Error deleting employee: " + ex.Message);
                return false;
            }
        }
    }
}