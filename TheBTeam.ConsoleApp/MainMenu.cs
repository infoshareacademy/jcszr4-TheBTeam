﻿using System;
using TheBTeam.BLL;
using TheBTeam.BLL.Services;
using System.Linq;

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
                "Show transaction by Category",
                "Show transaction according User",
                "Show Users transaction by Type",
                "Edit existing user",
                "Edit transaction",
                "Search transactions by date",
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
                    Console.WriteLine("========================================================");
                    Console.WriteLine("========================================================");
                    Console.WriteLine("||    Welcome in the financial planner by TheBTeam    ||");
                    Console.WriteLine("========================================================");
                    Console.WriteLine("========================================================");
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
                    if (tmpListUsers.UsersList.Count > 0)
                    {
                        Console.WriteLine($"The Users were loaded successful");
                    }
                    else
                    {
                        Console.WriteLine($"The users have not been loaded!");
                    }
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
                else if (mainMenuItem[currentItem] == "View users")
                {
                    UserViewer.ViewUsers(tmpListUsers.UsersList);
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
                    TransactionViewer.ViewTransaction(tmpListTransactions.TransactionsList);
                }
                else if (mainMenuItem[currentItem] == ("Show transaction by Category"))
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    TransactionViewer.ViewTransactionAccordingCategory(tmpListTransactions.TransactionsList);
                }
                else if (mainMenuItem[currentItem] == "Show Users transaction by Type")
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    TransactionViewer.UserAndTypeTransaction(tmpListUsers.UsersList, tmpListTransactions.TransactionsList);
                    Console.ReadKey();
                }
                else if (mainMenuItem[currentItem] == "Show Users transaction by Type")
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    TransactionViewer.UserAndTypeTransaction(tmpListUsers.UsersList, tmpListTransactions.TransactionsList);
                    Console.ReadKey();
                }
                else if (mainMenuItem[currentItem] == ("Edit existing user"))//there used to be like 3 paragraphs here regarding my reasoning, but fuck that, basically edit user wont work once we pass a tmplist
                                                                             //to it directly its messy due to the way references are handled
                                                                             //or maybe more the way i think they are handled - lists being copied, and objects referred
                                                                             //bottom line is it works but there definitely is to be a nicer way to do this, ill ask patryk during refinements
                                                                             //Actually nvm im dumb, since below works i jsut understood sb wrong, ill get below to look decent when im done with homework
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    string selectedUserEmail = ConsoleFactory.SelectUserEmail(tmpListUsers.UsersList); //modified SelecUser returns a valid email string
                    if (selectedUserEmail == null)
                        continue;
                    ConsoleFactory.EditUser(tmpListUsers.UsersList.FirstOrDefault(user => user.Email == selectedUserEmail));//we pass the first user object with the specified email since theyre supposed to be unique anyways
                    Console.ReadKey();
                }
                else if (mainMenuItem[currentItem] == ("Edit transaction"))
                {
                    int indexOfTransaction = TransactionViewer.ViewTransactionEdit(tmpListTransactions.TransactionsList);
                    ConsoleFactory.EditTransaction(tmpListTransactions.TransactionsList[indexOfTransaction]);
                    Console.ReadKey();
                }
                else if (mainMenuItem[currentItem] == ("Search transactions by date"))
                {
                    string selectedUserEmail = ConsoleFactory.SelectUserEmail(tmpListUsers.UsersList);
                    ConsoleFactory.SelectDate(selectedUserEmail, tmpListTransactions);
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