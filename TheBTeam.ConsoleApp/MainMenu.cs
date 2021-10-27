using System;
using TheBTeam.BLL;
using TheBTeam.BLL.Services;
using TheBTeam.BLL.Servises;

namespace TheBTeam.ConsoleApp
{
    public static class MainMenu
    {
        //here you can add new main menu item
        private static readonly string[] mainMenuItems = {
                "Load data from external file",
                "Add new user",
                "View users",
                "Enter transaction",
                "Show all transaction",
                "Show transaction according Category",
                "Show transaction according User",
                "Exit" };//Guess should be made somehow else- have to change whole text in code every time sth is changed
        public static void ShowMainMenu()
        {
            short currentItem = 0;
            //TmpDatabase tmpListUsers = new TmpDatabase();
            //TmpDatabase tmpListTransactions = new TmpDatabase();
            do
            {
                ConsoleKeyInfo keyPressed;
                do
                {
                    Console.WriteLine("||================================================||");
                    Console.WriteLine($"||     Welcome in the financial planner           ||");
                    Console.WriteLine("||================================================||");
                    Console.WriteLine("");
                    for (int i = 0; i < mainMenuItems.Length; i++)
                    {
                        if (currentItem == i)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(">>");
                            Console.WriteLine(mainMenuItems[i] + "<<");
                        }
                        else
                        {
                            Console.WriteLine(mainMenuItems[i]);
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
                        if (currentItem > mainMenuItems.Length - 1) currentItem = 0;
                    }
                    else if (keyPressed.Key.ToString() == "UpArrow")
                    {
                        currentItem--;
                        if (currentItem < 0) currentItem = Convert.ToInt16(mainMenuItems.Length - 1);
                    }
                } while (keyPressed.KeyChar != 13);//if press enter selected menu

                //Selected mainmenu from loop
                if (mainMenuItems[currentItem] == "Load data from external file")//thing it is better way
                {
                    Console.WriteLine($"{mainMenuItems[currentItem]} ...");
                    TmpDatabase.UsersList = LoadDataFromFile.ReadUserFile();
                }
                else if (mainMenuItems[currentItem] == "Add new user")
                {
                    Console.WriteLine($"{mainMenuItems[currentItem]}");
                    var user = ConsoleFactory.CreateNewUser();
                    if (user != null)
                    {
                        TmpDatabase.UsersList.Add(user);
                    }
                }
                else if (mainMenuItems[currentItem] == ("View users"))
                {
                    UserViewer.ViewUsers(TmpDatabase.UsersList);
                }
                else if (mainMenuItems[currentItem] == ("Enter transaction"))
                {
                    Console.WriteLine($"{mainMenuItems[currentItem]}");
                    var transaction = ConsoleFactory.CreateNewTransaction(TmpDatabase.UsersList);
                    if (transaction != null)
                    {
                        TmpDatabase.TransactionsList.Add(transaction);
                    }
                }
                else if (mainMenuItems[currentItem] == ("Show all transaction"))
                {
                    Console.WriteLine($"{mainMenuItems[currentItem]}");
                    TransactionViewer.ViewTransaction(TmpDatabase.TransactionsList);
                }
                else if (mainMenuItems[currentItem] == ("Show transaction according Category"))
                {
                    Console.WriteLine($"{mainMenuItems[currentItem]}");
                    TransactionViewer.ViewTransactionAccordingCategory(TmpDatabase.TransactionsList);
                }
                else if (mainMenuItems[currentItem] == ("Exit"))
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