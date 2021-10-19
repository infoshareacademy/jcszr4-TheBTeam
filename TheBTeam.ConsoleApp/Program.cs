using System;
using TheBTeam.BLL;
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
            short CurrentItem = 0;
            ConsoleKeyInfo KeyPressed;
            //here you can add new main menu item
            string[] MainMenuItem = {
                "Load default setting",
                "Add new user",
                "Enter transation",
                "Show transaction history for the month",
                "Exit" };
            do
            {
                do
                {
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine($"Welcome in the financial planner");
                    Console.WriteLine("------------------------------------------");
                    for (int i = 0; i < MainMenuItem.Length; i++)
                    {
                        if (CurrentItem == i)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(">>");
                            Console.WriteLine(MainMenuItem[i] + "<<");
                        }
                        else
                        {
                            Console.WriteLine(MainMenuItem[i]);
                        }
                        Console.ResetColor();
                    }
                    Console.WriteLine("-----------------------------------------------");
                    Console.Write("Select your choice with the arrow keys and click (ENTER) key");
                    KeyPressed = Console.ReadKey(true);
                    Console.Clear();
                    if (KeyPressed.Key.ToString() == "DownArrow")
                    {
                        CurrentItem++;
                        if (CurrentItem > MainMenuItem.Length - 1) CurrentItem = 0;
                    }
                    else if (KeyPressed.Key.ToString() == "UpArrow")
                    {
                        CurrentItem--;
                        if (CurrentItem < 0) CurrentItem = Convert.ToInt16(MainMenuItem.Length - 1);
                    }
                }
                while (KeyPressed.KeyChar != 13);//if press enter selected menu
                //Selected mainmenu from loop
                if (MainMenuItem[CurrentItem].Contains("Load"))
                {
                    Console.WriteLine($"{MainMenuItem[CurrentItem]} ...");
                    LoadUserFromFile.ReadUserFile();
                    Console.ReadKey();
                }
                else if (MainMenuItem[CurrentItem].Contains("Add new user"))
                {
                    //Add here methody add new use
                    AddNewUser(MainMenuItem[CurrentItem]);
                }
                else if (MainMenuItem[CurrentItem].Contains("Enter transation"))
                {
                    //Add here eneter transaction (date , category, pay)
                    EnterTransation(MainMenuItem[CurrentItem]);

                }
                else if (MainMenuItem[CurrentItem].Contains("month"))
                {
                    //Add here show transactions
                    EnterTransationPerMonth(MainMenuItem[CurrentItem]);
                }
                else
                {
                    Console.WriteLine("Exit ...");
                    Environment.Exit(0);
                }
            }
            while (true);
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
    }
}
