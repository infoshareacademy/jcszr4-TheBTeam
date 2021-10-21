//using System;
//using System.Collections.Generic;
//using TheBTeam.BLL;
//using TheBTeam.BLL.Model;

//namespace TheBTeam.ConsoleApp;

//{
//    public class ConsoleFactory
//{
//    public static User CreateNewUser(List<User> userList)
//    {
//        const int MinNameLength = 2;
//        int age;
//        Console.Clear();
//        Console.WriteLine("                       CREATING NEW USER                        ");
//        Console.WriteLine("=================================================================");

//        var firstName = GetStringInput("First Name", MinNameLength);
//        var lastName = GetStringInput("Last Name", MinNameLength);
//        var email = GetEmail();




//        Console.WriteLine("=================================================================");
//        return new User(decimal balance, Currency currency, int age, string firstName, string lastName, string gender, string company, string email, string phone, string address);
//    }
//    private static string GetStringInput(string name, int minLength)
//    {
//        while (true)
//        {
//            Console.Write($"{name}: ");
//            var input = Console.ReadLine()?.Trim();
//            if (input == null || input.Length < minLength)
//                Console.WriteLine($"Invalid data. Input should be at least {minLength} char long. Retry!");
//            else
//            {
//                return input;
//            }
//        }
//    }

//    private static string GetEmail()
//    {
//        while (true)
//        {
//            Console.Write("email: ");
//            var input = Console.ReadLine()?.Trim();
//            if (input == null)
//                Console.WriteLine("input is empty, retry!");
//            else if (!input.Contains('@') | !input.Contains('.'))
//                Console.WriteLine("Email have to contain @ and .***, retry!");
//            else
//                return input;
//        }
//    }
//}