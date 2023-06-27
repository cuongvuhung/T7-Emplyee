using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Xml.Linq;
namespace T7;

class Program
{
    static void Main()
    {
        //Variable            
        Screen scr = new();

        //Mainstream
        do
        {
            scr.Login();
            switch (scr.GetRole())
            {
                case "manager":
                    scr.ManagerScreen();
                    break;
                case "user":
                    scr.UserScreen();
                    break;
            }
        }
        while (!scr.GetLoged());
        Console.ReadLine();
    }

}

