﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace T7
{
    public class EmployeeManager : BaseManager
    {
        //Varriable
        public Employee[] employees;
        private readonly string filePath = @"T7.dat";
        public string empNoLogin = "";
        // Contructor
        public EmployeeManager() 
        {
            // Create template file if not exist
            if (!File.Exists(filePath)) { File.WriteAllText(filePath, "EmpNo,EmpName,EmpEmail,true,EmpPassword,false"); }

            // Read file thisPath
            string[] content = File.ReadAllLines(this.filePath);

            // Create template file for first item 
            this.employees = new Employee[]
            {
                new Employee("EmpNo", "EmpName", "EmpEmail",true,"EmpPassword",false)
            };

            // Set data from file to cache data. 
            if (content.Length > 1)
            {
                for (int i = 1; i < content.Length; i++)
                {
                    string[] cell = content[i].Split(',');
                    employees = employees.Concat(new Employee[] { new Employee(cell[0], cell[1], cell[2], Convert.ToBoolean(cell[3]), cell[4], Convert.ToBoolean(cell[5])) }).ToArray();
                }
            }
            else
            {
                // Set admin user for first time or no base data
                Console.WriteLine("No base data!");
                Console.WriteLine("Creating base data!");
                employees = employees.Concat(new Employee[] { new Employee("admin", "admin", "admin", false, "admin", true) }).ToArray();
                PrintToFile(this.filePath);
            }
        }



        // Void        
        // Module Add new a employee
        public override void AddNew()
        {
            Console.Write("Enter EmpNo:");
            string empNo = (Console.ReadLine() + "");
            Console.Write("Enter EmpName:");
            string empName = (Console.ReadLine() + "");
            Console.Write("Enter EmpEmail:");
            string empEmail = (Console.ReadLine() + "");
            Console.Write("Enter EmpPassword:");
            string empPassword = (Console.ReadLine() + "");
            Console.Write("Enter EmpIsManager (True/Any else):");
            bool empIsManager;
            if (Console.ReadLine() == "True")
            {
                empIsManager = true;
            }
            else
            {
                empIsManager = false;
            }

            // Check not duplicate Emp No
            if (!this.IsValid(empNo))
            {
                employees = employees.Concat(new Employee[] { new Employee(empNo, empName, empEmail, false, empPassword, empIsManager) }).ToArray();
                WriteLogFile(empNoLogin + " add new " + empNo + "," + empName + "," + empEmail);
            }
            else
            {
                Console.Write("Valid EmpNo! ");
                Console.ReadLine();
            }
            PrintToFile(this.filePath);
            PrintList(employees);
        }

        // Module Update a employee
        public override void Update()
        {
            Console.Write("Enter EmpNo or EmpName for update: ");
            string searchKey = (Console.ReadLine() + "");
            foreach (Employee emp in employees)
            {
                if (((emp.GetNo().Equals(searchKey) || emp.GetName().Equals(searchKey)) && emp.GetDeleted().Equals(false)))
                {
                    Console.WriteLine("Found a Employee have EmpNo or Emp Name is:" + searchKey);
                    Console.WriteLine(emp.GetNo() + "," + emp.GetName() + "," + emp.GetEmail());
                    Console.WriteLine("Ready for update!");
                    Console.Write("Enter EmpNo: ");
                    emp.SetNo((Console.ReadLine() + ""));
                    Console.Write("Enter EmpName: ");
                    emp.SetName((Console.ReadLine() + ""));
                    Console.Write("Enter EmpEmail: ");
                    emp.SetEmail((Console.ReadLine() + ""));
                    WriteLogFile(empNoLogin + " update " + searchKey + "," + emp.GetNo() + "," + emp.GetName() + "," + emp.GetEmail());
                }
            }
            PrintToFile(this.filePath);
            PrintList(employees);
        }

        // Module Delete a employee
        public override void Delete()
        {
            Console.Write("Enter EmpNo or EmpName for delete: ");
            String searchKey = (Console.ReadLine() + "");
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(searchKey) || emp.GetName().Equals(searchKey))
                {
                    Console.WriteLine("Found a Employee have EmpNo or Emp Name is:" + searchKey);
                    Console.WriteLine(emp.GetNo() + "," + emp.GetName() + "," + emp.GetEmail());
                    Console.WriteLine("Ready for delete");
                    Console.Write("Yes(Y) or No(any other):");
                    if ((Console.ReadLine() + "").ToUpper() == "Y")
                    {
                        emp.SetDeleted(true);
                        WriteLogFile(empNoLogin + " delete " + searchKey);
                    }
                    else { Console.WriteLine("Nothing happen!"); }
                }
            }
            PrintToFile(this.filePath);
            PrintList(employees);
        }

        // Module Find employee
        public override void Find()
        {
            Console.Write("Enter EmpNo or Name: ");
            String searchKey = (Console.ReadLine() + "");
            // search            
            Employee[] result = new Employee[this.GetDataLength()];
            int count = 0;
            foreach (Employee emp in employees)
            {
                if ((emp.GetNo().Equals(searchKey) || emp.GetName().Equals(searchKey)) && emp.GetDeleted().Equals(0))
                {
                    result[count++] = emp;
                }
            }
            // print
            if (count > 0)
            {
                PrintList(result);
            }
            else
            {
                Console.WriteLine("Not Found!");
            }
        }

        // Module Show List of employee
        public override void PrintList(Employee[] arr)
        {
            Console.Clear();
            foreach (Employee item in arr)
            {
                if (item != null && item.GetDeleted().Equals(false))
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine("---------------------------------");
        }

        // Module Write to file filePath all data 
        public override void PrintToFile(string filePath)
        {
            string[] content = new string[this.GetDataLength()];
            int i = 0;
            foreach (Employee item in employees)
            {
                content[i++] = (item.ToStringForFile() + "");
            }
            File.WriteAllLines(filePath, content);
            Console.Write("Data is saved!");
            Console.ReadLine();
        }

        // Module Login
        

        // Module Write log file
        public static void WriteLogFile(string content)
        {
            File.AppendAllTextAsync("T7.log", DateTime.Now + " : " + content + "\n");
        }


        //Checker
        // Check how much item of data 
        private int GetDataLength()
        {
            int employeeLenght = 0;
            foreach (Employee emp in employees) employeeLenght++;
            return employeeLenght;
        }

        // Check empNo valid in data
        public bool IsValid(string empNo)
        {
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(empNo))
                { return true; }
            }
            return false;
        }

        // Check correct passwword
        public bool IsPassword(string empNo, string empPassword)
        {
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(empNo) && emp.GetPassword().Equals(empPassword))
                { return true; }
            }
            return false;
        }

        // Check user is manager
        public bool IsManager(string empNo)
        {
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(empNo) && emp.GetIsManager().Equals(true))
                { return true; }
            }
            return false;
        }

        // Check empNo is deleted
        public bool IsDeleted(string empNo)
        {
            bool value = false;
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(empNo) && emp.GetDeleted().Equals(true))
                { value = true; }
            }
            return value;
        }
    }
}

