using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBTeam.BLL;
using TheBTeam.BLL.Servises;

namespace TheBTeam.ConsoleApp
{
    public static class MainMenu
    {
        //here you can add new main menu item
        private static readonly string[] mainMenuItem = DataBase.MainMenuItems;
        public static void ShowMainMenu()
        {
            short currentItem = 0;
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
                if (mainMenuItem[currentItem].Contains("Load"))
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]} ... ");
                    DataBase.AllUsers = LoadUserFromFile.ReadUserFile();
                }
                else if (mainMenuItem[currentItem].Contains("Add new user"))
                {
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    //ConsoleFactory.CreateNewUser(DataBase.AllUsers);
                    DataBase.AllUsers.Add(ConsoleFactory.CreateNewUser(DataBase.AllUsers));
                    //DataBase user = new DataBase();
                    
                }
                else if (mainMenuItem[currentItem].Contains("Enter transation"))
                {
                    //Add here eneter transaction (date , category, pay)
                    Console.WriteLine($"{mainMenuItem[currentItem]}");
                    TypeLists typeLists = new TypeLists();
                    foreach (var item in typeLists.TransactionsCategories)
                    {
                        Console.WriteLine(item.Name);
                    }
                    Console.ReadKey();
                }
                else if (mainMenuItem[currentItem].Contains("View"))
                {
                    UserViewer view = new UserViewer();
                    Console.WriteLine(view.ViewUsers());
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
