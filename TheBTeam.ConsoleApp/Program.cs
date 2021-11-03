using System;
using System.Runtime.InteropServices;

namespace TheBTeam.ConsoleApp
{
    class Program
    {
        private const int MinimizeSizeConsoleWindow = 170;
        static void Main(string[] args)
        {
            if (Console.BufferWidth < MinimizeSizeConsoleWindow)
            {
                Console.SetWindowSize(MinimizeSizeConsoleWindow, 40);
            }
            MainMenu.ShowMainMenu();
        }
    }
}
