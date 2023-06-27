using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T7
{
    public class Screen : EmployeeManager
    {
        public Screen() : base()
        { 
        }
        private bool loged = false;
        private string role = " ";
        
        //Getter
        public string GetRole()
        {
            return role;
        }
        public bool GetLoged()
        {
            return loged;
        }
        public void Login()
        {
            string empPassword;
            Console.WriteLine("***EMPLOYEE MANAGER***");
            Console.WriteLine("***  LOGIN SCREEN  ***");
            do
            {
                Console.Write("EmpNo:");
                empNoLogin = (Console.ReadLine() + "");
                Console.Write("Password:");
                empPassword = (Console.ReadLine() + "");
                if (!this.IsValid(empNoLogin))
                {
                    Console.WriteLine("Invalid username");
                    Console.ReadLine();
                    continue;
                }
                if (!this.IsPassword(empNoLogin, empPassword))
                {
                    Console.WriteLine("Wrong password");
                    Console.ReadLine();
                    continue;
                }
                if (this.IsDeleted(empNoLogin))
                {
                    Console.WriteLine("Employee deleted!");
                    Console.ReadLine();
                    continue;
                }
                this.loged = true;
                WriteLogFile(empNoLogin + " log in ");

                if (IsManager(empNoLogin)) role = "manager";
                if (!IsManager(empNoLogin)) role = "user";
                Console.WriteLine("Login succesful");
                Console.ReadLine();
                Console.Clear();
            }
            while (!loged);
        }

        // Module Manager Screen
        public void ManagerScreen()
        {
            int selected;
            do
            {
                Console.WriteLine("***EMPLOYEE MANAGER***");
                Console.WriteLine("EmpNo: {0}", this.empNoLogin);
                Console.WriteLine("Role = Manager");
                Console.WriteLine("1. Search Employee by Name or EmpNo");
                Console.WriteLine("2. Add New Employee");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Show all Employee");
                Console.WriteLine("6. Logout");
                Console.WriteLine("7. Exit");
                Console.Write("   Select (1-7): ");
                selected = Convert.ToInt16(Console.ReadLine());
                switch (selected)
                {
                    case 1:
                        this.Find();
                        break;
                    case 2:
                        this.AddNew();
                        break;
                    case 3:
                        this.Update();
                        break;
                    case 4:
                        this.Delete();
                        break;
                    case 5:
                        this.PrintList(employees);
                        break;
                    case 6:
                        loged = false;
                        Console.WriteLine("Logging out");
                        WriteLogFile(empNoLogin + " log out");
                        break;
                    case 7:
                        WriteLogFile(empNoLogin + " log out");
                        Console.WriteLine("-------- END ---------");
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            } while (selected != 7 && loged);
        }

        // Module User Screen
        public void UserScreen()
        {
            int selected;
            do
            {
                Console.WriteLine("***EMPLOYEE MANAGER***");
                Console.WriteLine("EmpNo: {0}", empNoLogin);
                Console.WriteLine("Role = User");
                Console.WriteLine("1. Search Employee by Name or EmpNo");
                Console.WriteLine("2. Show all Employee");
                Console.WriteLine("3. Log out");
                Console.WriteLine("4. Exit");
                Console.Write("   Select (1-4): ");
                selected = Convert.ToInt16(Console.ReadLine());
                switch (selected)
                {
                    case 1:
                        this.Find();
                        break;
                    case 2:
                        this.PrintList(employees);
                        break;
                    case 3:
                        loged = false;
                        Console.WriteLine("Logging out");
                        WriteLogFile(empNoLogin + " log out");
                        break;
                    case 4:
                        Console.WriteLine("-------- END ---------");
                        WriteLogFile(empNoLogin + " log out");
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            } while (selected != 4 && loged);
        }

    }
}
