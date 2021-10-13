using System;
using System.Collections.Generic;
using System.Linq;

namespace TheBTeam.ConsoleApp
{
    class Program
    {       
        static void Main(string[] args)
        {
            MainMenu();
        }
        static void MainMenu()
        {
            //here you can add new main menu item
            var menu = new Menu(new string[] {
                "Load default setting",
                "Add new user",
                "Enter transation",
                "Show transaction history for the month",
                "Exit" });
            bool done = false;
            var menuPainter = new ConsoleMenuPaint(menu);
            do
            {
                done = false;
                do
                { 
                    menuPainter.Paint(0, menu.Items.Count);//here you can shift position on menu
                    Console.WriteLine("-----------------------------------------------");
                    Console.Write("Select your choice with the arrow keys and click (ENTER) key");
                    Console.SetCursorPosition(0, 0);
                    var keyInfo = Console.ReadKey();
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow: menu.MoveUp(); break;
                        case ConsoleKey.DownArrow: menu.MoveDown(); break;
                        case ConsoleKey.Enter: done = true; break;
                        default: break;
                    }
                }
                while (!done);
                //Console.WriteLine((menu.SelectedOption ?? "You nothing selected use arrow key!" +
                //    ""));
                if (menu.SelectedOption != null && done == true)
                {                  
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Clear();
                    if (menu.SelectedOption.Contains("Load"))
                    {
                        LoadDefaultSetting(menu.SelectedOption);
                    }
                    if (menu.SelectedOption.Contains("Add"))
                    {
                        AddNewUser(menu.SelectedOption);
                    }
                    if (menu.SelectedOption.Contains("Enter transation"))
                    {
                        EnterTransation(menu.SelectedOption);
                    }
                    if (menu.SelectedOption.Contains("month"))
                    {
                        EnterTransationPerMonth(menu.SelectedOption);
                    }
                    if (menu.SelectedOption.Contains("Exit"))
                    {
                        Environment.Exit(0);
                    }
                }
            }
            while (true);
        }
        static void LoadDefaultSetting(string selectedMenu)//here add json reader
        {
            Console.WriteLine($"{selectedMenu}");
            Console.ReadKey();
        }
        static void AddNewUser(string selectedMenu) // here add new user
        {
            Console.WriteLine($"{selectedMenu}");
            string FirstName = Console.ReadLine();
            Console.WriteLine($"Welcome {FirstName}");
        }
        static void EnterTransation(string selectedMenu)//here add enter new transaction
        {
            Console.WriteLine($"{selectedMenu}");
            Console.ReadKey();
        }
        static void EnterTransationPerMonth(string selectedMenu)//here add view transaction
        {
            Console.WriteLine($"{selectedMenu}");
            Console.ReadKey();
        }
        public class Menu
        {
            public Menu(IEnumerable<string> items)
            {
                Items = items.ToArray();
            }
            public IReadOnlyList<string> Items { get; }
            public int SelectedIndex { get; private set; } = -1; // nothing selected
            public string SelectedOption => SelectedIndex != -1 ? Items[SelectedIndex] : null;
            public void MoveUp() => SelectedIndex = Math.Max(SelectedIndex - 1, 0);
            public void MoveDown() => SelectedIndex = Math.Min(SelectedIndex + 1, Items.Count - 1);
            //public void MoveDown() => SelectedIndex = SelectedIndex > Items.Count - 1 ? 0 : SelectedIndex+1;
            //public void MoveUp() => SelectedIndex = SelectedIndex < 0 ? Items.Count-1 : SelectedIndex-1;
        }
        public class ConsoleMenuPaint
        {
            readonly Menu menu;
            public ConsoleMenuPaint(Menu menu)
            {
                this.menu = menu;
            }
            public void Paint(int x, int y)
            {
                for (int i = 0; i < menu.Items.Count; i++)
                {
                    Console.SetCursorPosition(x, y + i);
                    var color = menu.SelectedIndex == i ? ConsoleColor.Yellow : ConsoleColor.Gray;
                    Console.ForegroundColor = color;
                    Console.WriteLine(menu.Items[i]);
                }
                Console.ResetColor();
            }
        }
    }
}
