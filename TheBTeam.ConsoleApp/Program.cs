using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using TheBTeam.BLL;

namespace TheBTeam.ConsoleApp
{
    class Program
    {

        static List<User> ReadUserFile()
        {
            string fileName = "users.json";
            string jsonstring = File.ReadAllText(fileName);
            var userData = JsonSerializer.Deserialize<List<User>>(jsonstring);
            return userData;
        }

        static void Main(string[] args)
        {
            Menu menu = new Menu();
            User user = new User();
            Category category = new Category();
            List<User> userDataList = ReadUserFile();

            do
            {
                Console.WriteLine($"Welcome {user.firstName} in the financial planner");
                int selectedOption = menu.SelectMenuOption();
                Console.WriteLine("------------------------------------------");
                switch (selectedOption)
                {
                    case 1:
                        Console.WriteLine($"Enter ID user");
                        break;
                    case 2:
                        Console.WriteLine($"Add new user");
                        Console.WriteLine($"Enter FirstName");
                        user.firstName = Console.ReadLine();
                        Console.WriteLine($"Enter LastName");
                        user.lastName = Console.ReadLine();
                        Console.WriteLine($"Welcome {user.firstName} {user.lastName}");
                        break;
                    case 3:
                        Console.WriteLine($"Enter transation");
                        category.SelectCategory();
                        break;
                    case 4:
                        Console.WriteLine($"Transactiosn history for the month");
                        break;
                    case 5:
                        Console.WriteLine($"Transactiosn history for the period");
                        break;
                    case 6:
                        Console.WriteLine($"Exit");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Select again");
                        break;
                }
                Console.WriteLine("-----------------------------------------------");
            }
            while (true);
        }
    }
}
