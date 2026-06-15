using System;
using System.Data.SqlClient;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 30: Perform CRUD Operations using ADO.NET
    ///
    /// Prerequisite SQL (run once on your SQL Server instance):
    ///
    ///   CREATE TABLE Employees (
    ///       EmployeeId INT PRIMARY KEY IDENTITY(1,1),
    ///       FullName   NVARCHAR(100) NOT NULL,
    ///       Department NVARCHAR(100) NOT NULL,
    ///       Salary     DECIMAL(10,2) NOT NULL
    ///   );
    ///
    /// Update the connection string below to match your local SQL Server instance.
    /// </summary>
    public static class Exercise30
    {
        // Update this for your environment, e.g.:
        // "Server=localhost;Database=CompanyDB;Trusted_Connection=True;TrustServerCertificate=True;"
        private const string ConnectionString =
            "Server=localhost;Database=CompanyDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static void Run()
        {
            try
            {
                using SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                Console.WriteLine("Connected to SQL Server.\n");

                int newId = InsertEmployee(connection, "Alice Johnson", "Engineering", 85000m);
                Console.WriteLine($"Inserted employee with ID: {newId}\n");

                Console.WriteLine("All employees:");
                ReadEmployees(connection);

                UpdateEmployeeSalary(connection, newId, 90000m);
                Console.WriteLine($"\nUpdated salary for employee ID {newId}.\n");

                Console.WriteLine("Employees after update:");
                ReadEmployees(connection);

                DeleteEmployee(connection, newId);
                Console.WriteLine($"\nDeleted employee with ID {newId}.\n");

                Console.WriteLine("Employees after delete:");
                ReadEmployees(connection);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                Console.WriteLine("\nMake sure:");
                Console.WriteLine("  1. SQL Server is running locally.");
                Console.WriteLine("  2. The connection string matches your server/instance name.");
                Console.WriteLine("  3. The 'Employees' table has been created (see SQL comment at top of this file).");
            }
        }

        // CREATE
        private static int InsertEmployee(SqlConnection connection, string fullName, string department, decimal salary)
        {
            const string sql = @"
                INSERT INTO Employees (FullName, Department, Salary)
                OUTPUT INSERTED.EmployeeId
                VALUES (@FullName, @Department, @Salary);";

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@FullName", fullName);
            command.Parameters.AddWithValue("@Department", department);
            command.Parameters.AddWithValue("@Salary", salary);

            return (int)command.ExecuteScalar();
        }

        // READ
        private static void ReadEmployees(SqlConnection connection)
        {
            const string sql = "SELECT EmployeeId, FullName, Department, Salary FROM Employees ORDER BY EmployeeId;";

            using SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string department = reader.GetString(2);
                decimal salary = reader.GetDecimal(3);

                Console.WriteLine($"  [{id}] {name} - {department} - {salary:C}");
            }
        }

        // UPDATE
        private static void UpdateEmployeeSalary(SqlConnection connection, int employeeId, decimal newSalary)
        {
            const string sql = "UPDATE Employees SET Salary = @Salary WHERE EmployeeId = @EmployeeId;";

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Salary", newSalary);
            command.Parameters.AddWithValue("@EmployeeId", employeeId);

            command.ExecuteNonQuery();
        }

        // DELETE
        private static void DeleteEmployee(SqlConnection connection, int employeeId)
        {
            const string sql = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId;";

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EmployeeId", employeeId);

            command.ExecuteNonQuery();
        }
    }
}
