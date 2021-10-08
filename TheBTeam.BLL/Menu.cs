using System;
using System.Collections.Generic;

namespace TheBTeam.BLL
{
    public class Menu
    {
        private Dictionary<int, string> MenuOption = new Dictionary<int, string>();
        private int numberOfOption;
        User user = new User("Ja", "admin", "2121554544");
        Category category = new Category();
        private void AddMenuOption(string textmenu)
        {
            MenuOption.Add(numberOfOption++, textmenu);
        }
        public void ShowMenuOption()
        {
            MenuOption.Clear(); numberOfOption = 1;
            Console.WriteLine($"Welcome {user.firstName} {user.lastName}");
            //Here You can add, delete menu options
            AddMenuOption("Enter ID user");
            AddMenuOption("Add new user");
            AddMenuOption("Enter transation");
            AddMenuOption("Show transaction history for the month");
            AddMenuOption("Show transaction history for the period");
            AddMenuOption("Exit");
            foreach (var (key, value) in MenuOption)
            {
                Console.WriteLine($"{key}. {value}");
            }
            SelectMenuOption();
        }
        private void SelectMenuOption()
        {
            int numberFromConsole = 0;
            while (!int.TryParse(Console.ReadLine(), out numberFromConsole))
            {
                Console.WriteLine("This is not a number!");
            }
            switch (numberFromConsole)
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
        }
    }
}
