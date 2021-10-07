using System;
using System.Collections.Generic;

namespace TheBTeam.BLL
{
    public class Menu
    {
        private Dictionary<int, string> MenuOption = new Dictionary<int, string>();
        private int numerOfOption;
        private void AddMenuOption(string textmenu)
        {
            MenuOption.Add(numerOfOption++, textmenu);
        }
        private void ShowMenuOption()
        {
            MenuOption.Clear(); numerOfOption = 1;
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
        }
        public int SelectMenuOption()
        {
            int numberFromConsole = 0;
            ShowMenuOption();
            while (!int.TryParse(Console.ReadLine(), out numberFromConsole))
            {
                Console.WriteLine("This is not a number!");
            }
            return numberFromConsole;
        }
    }
}
