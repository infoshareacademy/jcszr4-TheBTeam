using System;
using TheBTeam.BLL;
using TheBTeam.BLL.Model;
using TheBTeam.BLL.Services;
using TheBTeam.BLL.Servises;
using System.Linq;


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
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine($"Welcome in the financial planner");
                    Console.WriteLine("------------------------------------------");
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
                if (mainMenuItems[currentItem]== "Load data from external file")//thing it is better way
                {
                    Console.WriteLine($"{mainMenuItems[currentItem]} ...");
                    tmpListUsers.UsersList = LoadDataFromFile.ReadUserFile();
                }
                else if (mainMenuItems[currentItem]== "Add new user")
                {
                    Console.WriteLine($"{mainMenuItems[currentItem]}");
                    var user = ConsoleFactory.CreateNewUser();
                    if (user != null)
                    {
                        tmpListUsers.UsersList.Add(user);
                    }
                }
                else if (mainMenuItems[currentItem] == ("View users"))
                {
                    UserViewer view = new UserViewer();
                    Console.WriteLine(view.ViewUsers(tmpListUsers.UsersList));
                }
                else if (mainMenuItems[currentItem] == ("Enter transaction"))
                {
                    Console.WriteLine($"{mainMenuItems[currentItem]}");
                    tmpListTransactions.TransactionsList.Add(ConsoleFactory.CreateNewTransaction(tmpListUsers.UsersList));
                }
                else if (mainMenuItems[currentItem] == ("Show all transaction"))
                {
                    Console.WriteLine($"{mainMenuItems[currentItem]}");
                    TransactionViewer view = new TransactionViewer();
                    Console.WriteLine(view.ViewTransaction(tmpListTransactions.TransactionsList));
                }
                else if (mainMenuItems[currentItem] == ("Edit existing user"))
                {
                    Console.WriteLine("input email of the user you'd like to edit");
                    while (true)
                    {
                        string email = ConsoleFactory.GetEmail();
                        var user=tmpListUsers.UsersList.Where(User => User.Email == email).FirstOrDefault();
                        if (user != null) 
                        {
                            ConsoleFactory.EditUser(user);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Email not found, try again");
                        }
                    }

                }
                else if (mainMenuItems[currentItem] == ("Edit transaction"))
                {
                    Console.WriteLine("input title of the transaction you'd like to edit");
                    while (true)
                    {
                        string transactionTitle = ConsoleFactory.GetStringInput("title", 5);
                        var transaction = tmpListUsers.TransactionsList.Where(Transaction => Transaction.TransactionTitle == transactionTitle).FirstOrDefault();
                        if (transaction != null)
                        {
                            ConsoleFactory.EditTransaction(transaction);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Title not found, try again");
                        }
                    }

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