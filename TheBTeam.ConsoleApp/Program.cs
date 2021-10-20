using System;
using TheBTeam.BLL;
<<<<<<< HEAD
using TheBTeam.BLL.Model;
=======
using System.Collections.Generic;
using System.Linq;
>>>>>>> 43419d397c607dfef892582f841604d2e78cb31d

namespace TheBTeam.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            Console.WriteLine("Hello World!");
            Console.WriteLine("615b4be6281be025752c8800");
            Console.WriteLine(GenerateId());
=======
            MainMenu();
        }
        static void MainMenu()
        {
            short currentItem = 0;
            ConsoleKeyInfo keyPressed;
            //here you can add new main menu item
            string[] mainMenuItem = {
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
                    for (int i = 0; i < mainMenuItem.Length; i++)
                    {
                        if (currentItem == i)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(">>");
                            Console.WriteLine(mainMenuItem[i] + "<<");
                        }
                        else
                        {
                            Console.WriteLine(mainMenuItem[i]);
                        }
                        Console.ResetColor();
                    }
                    Console.WriteLine("-----------------------------------------------");
                    Console.Write("Select your choice with the arrow keys and click (ENTER) key");
                    keyPressed = Console.ReadKey(true);
                    Console.Clear();
                    if (keyPressed.Key.ToString() == "DownArrow")
                    {
                        currentItem++;
                        if (currentItem > mainMenuItem.Length - 1) currentItem = 0;
                    }
                    else if (keyPressed.Key.ToString() == "UpArrow")
                    {
                        currentItem--;
                        if (currentItem < 0) currentItem = Convert.ToInt16(mainMenuItem.Length - 1);
                    }
                }
                while (keyPressed.KeyChar != 13);//if press enter selected menu
                //Selected mainmenu from loop
                if (mainMenuItem[currentItem].Contains("Load"))
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]} ...");
                    LoadUserFromFile.ReadUserFile();
                    Console.ReadKey();
                }
                else if (mainMenuItem[currentItem].Contains("Add new user"))
                {
                    //Add here methody add new use
                    AddNewUser(mainMenuItem[currentItem]);
                }
                else if (mainMenuItem[currentItem].Contains("Enter transation"))
                {
                    //Add here eneter transaction (date , category, pay)
                    EnterTransation(mainMenuItem[currentItem]);

                }
                else if (mainMenuItem[currentItem].Contains("month"))
                {
                    //Add here show transactions
                    EnterTransationPerMonth(mainMenuItem[currentItem]);
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
>>>>>>> 43419d397c607dfef892582f841604d2e78cb31d
        }
        
    }

}
