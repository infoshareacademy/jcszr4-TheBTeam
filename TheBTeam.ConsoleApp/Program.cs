using System;
using System.Collections.Generic;
using System.Linq;

namespace TheBTeam.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
        {
            short curItem = 0;
            ConsoleKeyInfo key;
            //here you can add new main menu item
            string[] MainMenuItem = {
                "Load default setting",
                "Add new user",
                "Enter transation",
                "Show transaction history for the month",
                "Exit" };
            do
            {
                    Console.WriteLine("------------------------------------------");
                    for (int i = 0; i < MainMenuItem.Length; i++)
                    Console.Write("Select your choice with the arrow keys and click (ENTER) key");
                    key = Console.ReadKey(true);
                    Console.Clear();
                    if (key.Key.ToString() == "DownArrow")
                    {
                        curItem++;
                        if (curItem > MainMenuItem.Length - 1) curItem = 0;
                    }
                    else if (key.Key.ToString() == "UpArrow")
                    {
                        curItem--;
                        if (curItem < 0) curItem = Convert.ToInt16(MainMenuItem.Length - 1);
                    }
                }
                while (key.KeyChar != 13);//if press enter selected menu
                //Selected mainmenu from loop
                if (MainMenuItem[curItem].Contains("Load"))
                {
                    //Load here Json file!
                    LoadDefaultSetting(MainMenuItem[curItem]);
                }
                else if (MainMenuItem[curItem].Contains("Add new user"))
                {
                    //Add here methody add new use
                    AddNewUser(MainMenuItem[curItem]);
                }
                else if (MainMenuItem[curItem].Contains("Enter transation"))
                {
                    //Add here eneter transaction (date , category, pay)
                    EnterTransation(MainMenuItem[curItem]);

                }
                else if (MainMenuItem[curItem].Contains("month"))
                {
                    //Add here show transactions
                    EnterTransationPerMonth(MainMenuItem[curItem]);
                }
                else
                {
                    Console.WriteLine("Exit ...");
                    Environment.Exit(0);
                }
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
    }
}
