using System.Collections.Generic;
using System.Data.SqlClient;
using DepartmentsEmployees.Models;

namespace DepartmentsEmployees.Data
{
    public class EmployeeRepository
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DepartmentsEmployees;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        public List<Employee> GetAllEmployees()
        {
            using (SqlConnection conn = Connection)
            {

                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, FirstName, LastName FROM Employee";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Employee> employees = new List<Employee>();

                    while (reader.Read())
                    {

                        int idColumnPosition = reader.GetOrdinal("Id");

                        int idValue = reader.GetInt32(idColumnPosition);

                        int FirstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string FirstNameValue = reader.GetString(FirstNameColumnPosition);

                        int LastNameColumnPosition = reader.GetOrdinal("LastName");
                        string LastNameValue = reader.GetString(LastNameColumnPosition);

                        Employee employee = new Employee
                        {
                            Id = idValue,
                            FirstName = FirstNameValue,
                            LastName = LastNameValue

                        };


                        employees.Add(employee);
                    }

                    reader.Close();

                    return employees;
                }
            }
        }

        public Employee GetEmployeeById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT FirstName, LastName FROM Employee WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Employee employee = null;

                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            Id = id,
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName"))
                        };
                    }

                    reader.Close();

                    return employee;
                }
            }
        }

        public void AddEmployee(Employee employee)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = "INSERT INTO Emplyee (FirstName) OUTPUT INSERTED.Id Values (@FirstName)";
                    cmd.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
                    cmd.CommandText = "INSERT INTO Emplyee (LastName) OUTPUT INSERTED.Id Values (@LastName)";
                    cmd.Parameters.Add(new SqlParameter("@LastName", employee.LastName));
                    int id = (int)cmd.ExecuteScalar();

                    employee.Id = id;
                }
            }

        }

        public void UpdateEmployee(int id, Employee employee)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Employee
                                     SET FirstName = @FirstName
                                     SET LastName = @LastName
                                     WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", employee.LastName));
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEmployee(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Employee WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}