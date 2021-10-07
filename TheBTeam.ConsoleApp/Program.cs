using System;
using TheBTeam.BLL;

namespace TheBTeam.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            User user = new User();
            Category category = new Category();
            do
            {
                Console.WriteLine($"Welcome {user.FirstName} in the financial planner");
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
                        user.FirstName = Console.ReadLine();
                        Console.WriteLine($"Enter LastName");
                        user.LastName = Console.ReadLine();
                        Console.WriteLine($"Welcome {user.FirstName} {user.LastName}");
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
