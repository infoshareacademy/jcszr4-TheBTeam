using System;
using TheBTeam.BLL;
using TheBTeam.BLL.Services;
using TheBTeam.BLL.Servises;
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
                "Exit" };//Guess should be made somehow else- have to change whole text in code every time sth is changed 
        public static void ShowMainMenu()
        {
            short currentItem = 0;
            var usersAndTransaction = new TmpDatabase();
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
                    if (usersAndTransaction.UsersList.Count == 0)
                    {
                        usersAndTransaction.UsersList.AddRange(LoadDataFromFile.ReadUserFile());
                    }
                    if (usersAndTransaction.UsersList.Count > 0)
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
                        usersAndTransaction.UsersList.Add(user);
                    }
                }
                else if (mainMenuItem[currentItem] == "View users")
                {
                    UserViewer.ViewUsers(usersAndTransaction.UsersList);
                }
                else if (mainMenuItem[currentItem].Contains("Enter transaction"))
                {
                    var transaction = ConsoleFactory.AddNewTransaction(usersAndTransaction.UsersList);
                    if (transaction != null)
                    {
                        usersAndTransaction.TransactionsList.Add(transaction);
                    }
                    if (transaction == null)
                        continue;
                }
                else if (mainMenuItem[currentItem] == ("Show all transaction"))
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    TransactionViewer.ViewTransaction(usersAndTransaction.TransactionsList);
                }
                else if (mainMenuItem[currentItem] == ("Show transaction by Category"))
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    TransactionViewer.ViewTransactionAccordingCategory(usersAndTransaction.TransactionsList);
                }
                else if (mainMenuItem[currentItem] == "Show Users transaction by Type")
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    TransactionViewer.UserAndTypeTransaction(usersAndTransaction.UsersList, usersAndTransaction.TransactionsList);
                    Console.ReadKey();
                }
                else if (mainMenuItem[currentItem] == "Show transaction according User")
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    TransactionViewer.UserTransaction(usersAndTransaction.UsersList, usersAndTransaction.TransactionsList);
                    Console.ReadKey();
                }
                else if (mainMenuItem[currentItem] == ("Edit existing user"))
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    string selectedUserEmail = ConsoleFactory.SelectUserEmail(usersAndTransaction.UsersList);
                    if (selectedUserEmail != null)
                    {
                        EditExistingData.EditUser(usersAndTransaction.UsersList.FirstOrDefault(user => user.Email == selectedUserEmail));
                    }

                }
                else if (mainMenuItem[currentItem] == ("Edit transaction"))
                {
                    int indexOfTransaction = TransactionViewer.ViewTransactionEdit(usersAndTransaction.TransactionsList);
                    if (indexOfTransaction != -1)
                    {
                        EditExistingData.EditTransaction(usersAndTransaction.TransactionsList[indexOfTransaction]);
                        Console.ReadKey();
                    }
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