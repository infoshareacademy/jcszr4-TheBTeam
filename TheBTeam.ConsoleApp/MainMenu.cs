using System;
using System.Collections.Generic;
using TheBTeam.BLL;
using TheBTeam.BLL.Model;
using TheBTeam.BLL.Services;

namespace TheBTeam.ConsoleApp
{


    public static class MainMenu
    {
        //here you can add new main menu item
        private static readonly string[] MainMenuItem = {
                "Load data from external file",
                "Add new user",
                "Enter transaction",
                "Show transaction history for the month",
                "Edit User",
                "Edit Transaction",
                "Exit" };//Guess should be made somehow else- have to change whole text in code every time sth is changed
       
        public static void ShowMainMenu()
        {
            short currentItem = 0;
            var mainDatabase = new List<User>();
            do
            {
                ConsoleKeyInfo keyPressed;
                do
                {
                    Console.Clear();
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine($"Welcome in the financial planner");
                    Console.WriteLine("------------------------------------------");
                    for (int i = 0; i < MainMenuItem.Length; i++)
                    {
                        if (currentItem == i)
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
                    keyPressed = Console.ReadKey(true);
                    Console.Clear();
                    if (keyPressed.Key.ToString() == "DownArrow")
                    {
                        currentItem++;
                        if (currentItem > MainMenuItem.Length - 1) currentItem = 0;
                    }
                    else if (keyPressed.Key.ToString() == "UpArrow")
                    {
                        currentItem--;
                        if (currentItem < 0) currentItem = Convert.ToInt16(MainMenuItem.Length - 1);
                    }
                } while (keyPressed.KeyChar != 13);//if press enter selected menu

                
                //Selected mainmenu from loop
                if (MainMenuItem[currentItem]== "Load data from external file")//thing it is better way
                {

                    Console.WriteLine($"{MainMenuItem[currentItem]} ...");
                    
                    mainDatabase=LoadDataFromFile.ReadUserFile();
                    
                    Console.ReadKey();
                }
                else if (MainMenuItem[currentItem]== "Add new user")
                {
                    //Add here methody add new use
                    var user= ConsoleFactory.CreateNewUser();
                    if (user == null)
                        continue;
                    AddNewUser(MainMenuItem[currentItem]);
                    
                }
                else if (MainMenuItem[currentItem].Contains("Enter transaction"))
                {
                    //Add here eneter transaction (date , category, pay)
                   var transaction = ConsoleFactory.AddNewTransaction(mainDatabase);
                   if(transaction==null)
                       continue;
                   EnterTransaction(MainMenuItem[currentItem]);

                }
                else if (MainMenuItem[currentItem].Contains("Show transaction history for the month"))
                {
                    //Add here show transactions
                    EnterTransationPerMonth(MainMenuItem[currentItem]);
                }
                else if (MainMenuItem[currentItem].Contains("Edit User"))
                {
                    Console.WriteLine("input email of the user you'd like to edit");
                    ConsoleFactory.EditUser(mainDatabase);
                }
                else if (MainMenuItem[currentItem].Contains("Edit Transaction"))
                {
                    //Add here show transactions
                    //ConsoleFactory.EditTransaction(mainDatabase);
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
            string firstName = Console.ReadLine();
            Console.WriteLine($"Welcome {firstName}");
        }
        static void EnterTransaction(string selectedMenu)//here add enter new transaction
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