using System;
using TheBTeam.BLL;
using TheBTeam.BLL.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyModel;
using System.Runtime.InteropServices;

namespace TheBTeam.ConsoleApp
{
    class Program
    {
        //Show full screen console
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;
        static void Main(string[] args)
        {
            ShowWindow(ThisConsole, MAXIMIZE);
            MainMenu.ShowMainMenu();
        }
        

    }

}
