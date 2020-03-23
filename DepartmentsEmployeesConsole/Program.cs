using System;

namespace DepartmentsEmployeesConsole
{
    class Program
    {
        static void Main(string[] args)
        {


            // Below this point is the start of the departments


            DepartmentRepository departmentRepo = new DepartmentRepository();
            Console.WriteLine("Getting All Departments:");;

            Console.WriteLine();

            List<Department> allDepartments = departmentRepo.GetAllDepartments();

            foreach (Department dept in allDepartments)
            {
                Console.WriteLine($"{dept.Id} {dept.DeptName}");

                Console.WriteLine("----------------------------");
                Console.WriteLine("Getting Department with Id 1");

                Department singleDepartment = departmentRepo.GetDepartmentById(1);

                Console.WriteLine($"{singleDepartment.Id} {singleDepartment.DeptName}");
            }

            Department legalDept = new Department
            {
                DeptName = "Legal"
            };

            departmentRepo.AddDepartment(legalDept);

            Console.WriteLine("-------------------------------");
            Console.WriteLine("Added the new Legal Department!");


            // Below this point is the start of the employees


            EmployeeRepository employeeRepo = new EmployeeRepository();
            Console.WriteLine("Getting All Employees:"); ;

            Console.WriteLine();

            List<Employee> allEmployees = employeeRepo.GetAllEmployees();

            foreach (Employee emp in allEmployees)
            {
                Console.WriteLine($"{emp.Id} {emp.FirstName} {emp.LastName}");

                Console.WriteLine("----------------------------");
                Console.WriteLine("Getting Employee with Id 1");

                Employee singleEmployee = employeeRepo.GetEmployeeById(1);

                Console.WriteLine($"{singleEmployee.Id} {singleEmployee.FirstName} {singleEmployee.FirstName}");
            }

            Employee samSmith = new Employee
            {
                FirstName = "Sam",
                LastName = "Smith"
            };

            employeeRepo.AddEmployee(samSmith);

            Console.WriteLine("-------------------------------");
            Console.WriteLine("Added the new Employee!");
        }
    }
}