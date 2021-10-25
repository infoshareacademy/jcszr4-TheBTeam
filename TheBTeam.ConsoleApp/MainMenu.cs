using System;
using TheBTeam.BLL;
using TheBTeam.BLL.Services;
using TheBTeam.BLL.Servises;

namespace TheBTeam.ConsoleApp
{
    public static class MainMenu
    {
        //here you can add new main menu item
        //here you can add new main menu item
        private static readonly string[] mainMenuItem = {
                "Load data from external file",
                "Add new user",
                "View users",
                "Enter transaction",
                "Show transaction all transaction",
                "Show transaction history for the month",
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
                if (mainMenuItem[currentItem].Contains("Load data from external file"))
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]} ... ");
                    tmpListUsers.UsersList = LoadDataFromFile.ReadUserFile();
                }
                else if (mainMenuItem[currentItem].Contains("Add new user"))
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    tmpListUsers.UsersList.Add(ConsoleFactory.CreateNewUser());
                }
                else if (mainMenuItem[currentItem].Contains("View users"))
                {
                    UserViewer view = new UserViewer();
                    Console.WriteLine(view.ViewUsers(tmpListUsers.UsersList));
                }
                else if (mainMenuItem[currentItem].Contains("Enter transaction"))
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    tmpListTransactions.TransactionsList.Add(ConsoleFactory.CreateNewTransaction(tmpListUsers.UsersList));
                }
                else if (mainMenuItem[currentItem].Contains("Show transaction all transaction"))
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    TransactionViewer view = new TransactionViewer();
                    Console.WriteLine(view.ViewTransaction(tmpListTransactions.TransactionsList));
                }
                else if (mainMenuItem[currentItem].Contains("month"))
                {
                    //Add here show transactions
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Exit ...");
                    Environment.Exit(0);
                }
            }
            while (true);
        }
    }
}
