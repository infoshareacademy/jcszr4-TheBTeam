using System;
using TheBTeam.BLL;
using System.Collections.Generic;
using System.Linq;
using TheBTeam.BLL.Servises;

namespace TheBTeam.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }
        static void MainMenu()
        {
            short curItem = 0;
            ConsoleKeyInfo key;
            string[] mainMenuItem = DataBase.MainMenuItems;
            do
            {
                do
                {
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine($"Welcome in the financial planner");
                    Console.WriteLine("------------------------------------------");
                    for (int i = 0; i < mainMenuItem.Length; i++)
                    {
                        if (curItem == i)
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
                    key = Console.ReadKey(true);
                    Console.Clear();
                    if (key.Key.ToString() == "DownArrow")
                    {
                        curItem++;
                        if (curItem > mainMenuItem.Length - 1) curItem = 0;
                    }
                    else if (key.Key.ToString() == "UpArrow")
                    {
                        curItem--;
                        if (curItem < 0) curItem = Convert.ToInt16(mainMenuItem.Length - 1);
                    }
                }
                while (key.KeyChar != 13);//if press enter selected menu
                //Selected mainmenu from loop
                if (mainMenuItem[curItem].Contains("Load"))
                {
                    Console.WriteLine($"{mainMenuItem[curItem]} ... ");
                    DataBase.AllUsers = LoadUserFromFile.ReadUserFile();
                }
                else if (mainMenuItem[curItem].Contains("Add new user"))
                {
                    Console.WriteLine($"{mainMenuItem[curItem]}");
                    UserServices user = new UserServices();
                    user.CreateNewUser();
                }
                else if (mainMenuItem[curItem].Contains("Enter transation"))
                {
                    //Add here eneter transaction (date , category, pay)
                    Console.WriteLine($"{mainMenuItem[curItem]}");
                    Console.ReadKey();
                }
                else if (mainMenuItem[curItem].Contains("View"))
                {
                    UserServices view = new UserServices();
                    Console.WriteLine(view.ViewUsers());
                }
                else if (mainMenuItem[curItem].Contains("month"))
                {
                    //Add here show transactions
                    Console.WriteLine($"{mainMenuItem[curItem]}");
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
