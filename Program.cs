using System;
using System.Collections.Generic;

namespace PolymorphismDemo
{
    // Interface defining the contract for quittable objects
    public interface IQuittable
    {
        void Quit();
    }

    // Employee class implementing IQuittable interface with interactive resignation process
    public class Employee : IQuittable
    {
        // Private fields for encapsulation
        private int _id;
        private string _firstName;
        private string _lastName;
        private DateTime _hireDate;
        private static readonly List<string> _validResignationReasons = new List<string>
        {
            "Career Change",
            "Relocation",
            "Personal Reasons",
            "Better Opportunity",
            "Health Reasons",
            "Retirement"
        };

        // Properties with validation
        public int Id
        {
            get => _id;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Employee ID must be positive.");
                _id = value;
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("First name cannot be empty.");
                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Last name cannot be empty.");
                _lastName = value;
            }
        }

        public DateTime HireDate
        {
            get => _hireDate;
            set => _hireDate = value;
        }

        // Constructor initializing employee data
        public Employee(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            HireDate = DateTime.Now;
        }

        // Display available resignation reasons
        private void DisplayResignationReasons()
        {
            Console.WriteLine("\nAvailable Resignation Reasons:");
            for (int i = 0; i < _validResignationReasons.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_validResignationReasons[i]}");
            }
        }

        // Get valid resignation reason from user
        private string GetResignationReason()
        {
            while (true)
            {
                DisplayResignationReasons();
                Console.Write("\nEnter the number of your resignation reason (or 0 to cancel): ");
                
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                        throw new OperationCanceledException("Resignation process cancelled.");
                    
                    if (choice > 0 && choice <= _validResignationReasons.Count)
                        return _validResignationReasons[choice - 1];
                }
                
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

        // Implementation of IQuittable interface
        public void Quit()
        {
            Console.WriteLine($"\nProcessing resignation for {FirstName} {LastName}");
            Console.WriteLine($"Employment Duration: {(DateTime.Now - HireDate).Days} days");

            try
            {
                string reason = GetResignationReason();
                
                // Display resignation confirmation
                Console.WriteLine("\nResignation Summary:");
                Console.WriteLine($"Employee: {FirstName} {LastName} (ID: {Id})");
                Console.WriteLine($"Reason: {reason}");
                Console.WriteLine($"Resignation Date: {DateTime.Now:yyyy-MM-dd}");
                Console.WriteLine("Status: Processed Successfully");
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"\n{ex.Message}");
            }
        }

        // Override ToString for better object representation
        public override string ToString()
        {
            return $"Employee(ID: {Id}, Name: {FirstName} {LastName}, Hired: {HireDate:d})";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Interactive Employee Resignation System Demo\n");

                // Create an employee
                Employee employee = new Employee(1, "Emma", "Smith");
                Console.WriteLine($"Created: {employee}");

                // Demonstrate polymorphism by using interface type
                IQuittable quittableEmployee = employee;
                
                // Process resignation through the interface reference
                quittableEmployee.Quit();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}