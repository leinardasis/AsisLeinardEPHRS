using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public int Id { get; set; }                 // Column 1
    public string FirstName { get; set; }       // Column 2
    public string LastName { get; set; }        // Column 3
    public string Department { get; set; }      // Column 4
    public string JobTitle { get; set; }        // Column 5
    public decimal MonthlySalary { get; set; }  // Column 6
    public double TaxRate { get; set; }         // Column 7
    public DateTime HireDate { get; set; }      // Column 8
    public bool IsActive { get; set; }          // Column 9
    public string Email { get; set; }           // Column 10
}

class Program
{
    static List<Employee> employees = new List<Employee>();
    static int nextId = 1001; 

    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== EMPLOYEE PAYROLL & HR MANAGEMENT SYSTEM ===");
            Console.WriteLine("1. Register New Employee");
            Console.WriteLine("1. Add New Employee Record");
            Console.WriteLine("2. Read (View All Employees)");
            Console.WriteLine("3. Search / Filter (LINQ)");
            Console.WriteLine("4. Update Employee");
            Console.WriteLine("5. Delete Employee");
            Console.WriteLine("6. Exit");
            Console.Write("\nSelect an option: ");
            
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1": 
                    CreateEmployee(); 
                    break;
                case "2": 
                    ReadEmployees(); 
                    break;
                case "3": 
                    SearchEmployees(); 
                    break;
                case "4": 
                    UpdateEmployee(); 
                    break;
                case "5": 
                    DeleteEmployee(); 
                    break;
                case "6": 
                    running = false; 
                    break;
                default: 
                    Console.WriteLine("Invalid choice."); 
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
    }

    static void CreateEmployee()
    {
        Console.WriteLine("--- Add New Employee ---");

        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine();

        Console.Write("Enter Department (e.g., IT, HR, Sales): ");
        string department = Console.ReadLine();

        Console.Write("Enter Job Title (e.g., Software Engineer): ");
        string jobTitle = Console.ReadLine();

        Console.Write("Enter Monthly Salary: ");
        decimal.TryParse(Console.ReadLine(), out decimal salary);

        Console.Write("Enter Tax Rate (e.g., 0.12 for 12%): ");
        double.TryParse(Console.ReadLine(), out double taxRate);

        Console.Write("Enter Hire Date (yyyy-mm-dd): ");
        DateTime.TryParse(Console.ReadLine(), out DateTime hireDate);

        Console.Write("Is Active? (true/false): ");
        bool.TryParse(Console.ReadLine(), out bool isActive);

        Console.Write("Enter Email: ");
        string email = Console.ReadLine();

        Employee newEmployee = new Employee
        {
            Id = nextId++,
            FirstName = firstName,
            LastName = lastName,
            Department = department,
            JobTitle = jobTitle,
            MonthlySalary = salary,
            TaxRate = taxRate,
            HireDate = hireDate == DateTime.MinValue ? DateTime.Now : hireDate,
            IsActive = isActive,
            Email = email
        };

        employees.Add(newEmployee);
        Console.WriteLine($"\nEmployee added successfully! Assigned ID: {newEmployee.Id}");
    }

    static void ReadEmployees()
    {
        Console.WriteLine("--- Current Employee Directory ---");
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        Console.WriteLine($"{"ID",-5} {"Name",-20} {"Department",-12} {"Job Title",-18} {"Salary",10} {"Tax",-6} {"Hire Date",-10} {"Active",-6} {"Email",-20}");
        Console.WriteLine(new string('-', 115));

        foreach (Employee emp in employees)
        {
            string fullName = $"{emp.FirstName} {emp.LastName}";
            Console.WriteLine($"{emp.Id,-5} {fullName,-20} {emp.Department,-12} {emp.JobTitle,-18} {emp.MonthlySalary,10:C} {emp.TaxRate,-6:P0} {emp.HireDate,-10:yyyy-MM-dd} {emp.IsActive,-6} {emp.Email,-20}");
        }
    }

    static void SearchEmployees()
    {
        Console.Write("Enter search term (Name, Department, or Job Title): ");
        string searchTerm = (Console.ReadLine() ?? "").Trim().ToLower();

        // Prevent empty searches from returning the entire list
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            Console.WriteLine("\nSearch term cannot be empty.");
            return;
        }

        var results = employees.Where(e => 
            e.FirstName.ToLower().Contains(searchTerm) || 
            e.LastName.ToLower().Contains(searchTerm) || 
            e.Department.ToLower() == searchTerm ||   // Changed to EXACT match for Department
            e.JobTitle.ToLower().Contains(searchTerm)
        ).ToList();

        if (results.Count == 0)
        {
            Console.WriteLine("\nNo matching employees found.");
            return;
        }

        Console.WriteLine($"\n--- Search Results ({results.Count}) ---");
        Console.WriteLine($"{"ID",-5} {"Name",-20} {"Department",-12} {"Job Title",-18} {"Salary",10} {"Tax",-6} {"Hire Date",-10} {"Active",-6} {"Email",-20}");
        Console.WriteLine(new string('-', 115));

        foreach (var emp in results)
        {
            string fullName = $"{emp.FirstName} {emp.LastName}";
            Console.WriteLine($"{emp.Id,-5} {fullName,-20} {emp.Department,-12} {emp.JobTitle,-18} {emp.MonthlySalary,10:C} {emp.TaxRate,-6:P0} {emp.HireDate,-10:yyyy-MM-dd} {emp.IsActive,-6} {emp.Email,-20}");
        }
    }

    static void UpdateEmployee()
    {
        ReadEmployees();
        Console.Write("\nEnter the ID of the employee to update: ");
        
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Employee emp = employees.FirstOrDefault(e => e.Id == id);
            
            if (emp != null)
            {
                Console.Write($"Enter new First Name (current: {emp.FirstName}): ");
                string newFirstName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newFirstName)) emp.FirstName = newFirstName;

                Console.Write($"Enter new Last Name (current: {emp.LastName}): ");
                string newLastName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newLastName)) emp.LastName = newLastName;

                Console.Write($"Enter new Department (current: {emp.Department}): ");
                string newDept = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newDept)) emp.Department = newDept;

                Console.Write($"Enter new Job Title (current: {emp.JobTitle}): ");
                string newTitle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newTitle)) emp.JobTitle = newTitle;

                Console.Write($"Enter new Monthly Salary (current: {emp.MonthlySalary}): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newSalary)) emp.MonthlySalary = newSalary;

                Console.Write($"Enter new Tax Rate (current: {emp.TaxRate}): ");
                if (double.TryParse(Console.ReadLine(), out double newTax)) emp.TaxRate = newTax;

                Console.Write($"Enter new Hire Date (current: {emp.HireDate:yyyy-MM-dd}): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime newDate)) emp.HireDate = newDate;

                Console.Write($"Is Active? (current: {emp.IsActive}) (true/false): ");
                if (bool.TryParse(Console.ReadLine(), out bool newStatus)) emp.IsActive = newStatus;

                Console.Write($"Enter new Email (current: {emp.Email}): ");
                string newEmail = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newEmail)) emp.Email = newEmail;

                Console.WriteLine("\nEmployee updated successfully!");
            }
            else
            {
                Console.WriteLine("\nEmployee not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid ID format.");
        }
    }

    static void DeleteEmployee()
    {
        ReadEmployees();
        Console.Write("\nEnter the ID of the employee to delete: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            int removedCount = employees.RemoveAll(e => e.Id == id);

            if (removedCount > 0)
                Console.WriteLine("\nEmployee deleted successfully.");
            else
                Console.WriteLine("\nEmployee not found.");
        }
        else
        {
            Console.WriteLine("\nInvalid ID format.");
        }
    }
}