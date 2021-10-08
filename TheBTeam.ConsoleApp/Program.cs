using System;
using TheBTeam.BLL;

namespace TheBTeam.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();          
            do
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine($"Welcome in the financial planner");
                Console.WriteLine("------------------------------------------");
                menu.ShowMenuOption();
                Console.WriteLine("-----------------------------------------------");
            }
            while (true);
        }
    }
}
