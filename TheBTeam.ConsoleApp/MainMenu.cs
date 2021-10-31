﻿using System;
using TheBTeam.BLL;
using TheBTeam.BLL.Services;
using TheBTeam.BLL.Servises;

namespace TheBTeam.ConsoleApp
{
    public static class MainMenu
    {
        //here you can add new main menu item
        private static readonly string[] mainMenuItem = {
                "Load data from external file",
                "Add new user",
                "View users",
                "Enter transaction",
                "Show all transaction",
                "Show transaction according Category",
                "Show transaction according User",
                "Edit existing user",
                "Edit transaction",
                "Exit" };//Guess should be made somehow else- have to change whole text in code every time sth is changed 
        public static void ShowMainMenu()
        {
            short currentItem = 0;
            TmpDatabase tmpListUsers = new TmpDatabase();
            TmpDatabase tmpListTransactions = new TmpDatabase();
            do
            {
                ConsoleKeyInfo keyPressed;
                do
                {
                    Console.Clear();
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine($"Welcome in the financial planner         ");
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("---------------TheBTeam-------------------");
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
                } while (keyPressed.KeyChar != 13);//if press enter selected menu
                //Selected mainmenu from loop
                if (mainMenuItem[currentItem] == "Load data from external file")//thing it is better way
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]} ...");
                    tmpListUsers.UsersList = LoadDataFromFile.ReadUserFile();
                    Console.WriteLine($"Press any key to continue");
                    Console.ReadKey();
                }
                else if (mainMenuItem[currentItem] == "Add new user")
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    var user = ConsoleFactory.CreateNewUser();
                    if (user != null)
                    {
                        tmpListUsers.UsersList.Add(user);
                    }
                }
                else if (mainMenuItem[currentItem] == ("View users"))
                {
                    if (tmpListUsers.UsersList.Count > 0)
                    {
                        UserViewer.ViewUsers(tmpListUsers.UsersList);
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"No data user, Please add new user");
                        Console.WriteLine($"Press any key to continue");
                        Console.ReadKey();
                    }
                }
                else if (mainMenuItem[currentItem].Contains("Enter transaction"))
                {
                    var transaction = ConsoleFactory.AddNewTransaction(tmpListUsers.UsersList);
                    if (transaction != null)
                    {
                        tmpListTransactions.TransactionsList.Add(transaction);
                    }
                    if (transaction == null)
                        continue;
                }
                else if (mainMenuItem[currentItem] == ("Show all transaction"))
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    if (tmpListTransactions.TransactionsList.Count != 0)
                    {
                        TransactionViewer.ViewTransaction(tmpListTransactions.TransactionsList);
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"No data transaction, Please enter transaction");
                        Console.WriteLine($"Press any key to continue");
                        Console.ReadKey();
                    }
                }
                else if (mainMenuItem[currentItem] == ("Show transaction according Category"))
                {
                    //TODO  wywal sprawdzanie mailem tylko kategoria
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    if (tmpListTransactions.TransactionsList.Count != 0)
                    {
                        TransactionViewer.ViewTransactionAccordingCategory(tmpListTransactions.TransactionsList);
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"No data transaction, Please enter transaction");
                        Console.WriteLine($"Press any key to continue");
                        Console.ReadKey();
                    }
                }
                else if (mainMenuItem[currentItem] == ("Edit existing user"))
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    //TODO add here method
                    Console.ReadKey();
                }
                else if (mainMenuItem[currentItem] == ("Edit transaction"))
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    //TODO add here method
                    Console.ReadKey();
                }
                else if (mainMenuItem[currentItem] == ("Exit"))
                {
                    Console.WriteLine("Exit ...");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Will be soon!");
                    Console.ReadKey();
                }
            }
            while (true);
        }
    }
}