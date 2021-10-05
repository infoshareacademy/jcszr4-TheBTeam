using System;
using System.Collections.Generic;
using System.Linq;
using TheBTeam.BLL;

namespace TheBTeam.ConsoleApp
{
    class Program
    {
        enum Category
        {
            Home,
            Education,
        }
        static void Main(string[] args)
        {
            Person person = new Person();

            var persons = new Dictionary<int, Person>()//default values
            {
                 { 1, new Person { FirstName="Santiago", LastName="Castillo", ID="615b4be6281be025752c8800"
                 ,balance = 2354.13m,currency="PLN",age=32,gender="male",company="RETRACK"
                 ,email="santiagocastillo@retrack.com",phone="+48 (949) 435-2718"
                 ,address="490 Poplar Avenue, Skyland, Washington, 3188",registered="2017-03-11T07:27:26 -01:00"} }
            };
            foreach (var index in Enumerable.Range(1, 1))
            {
                //Console.WriteLine($"Person {index} is {persons[index].FirstName} {persons[index].LastName}");
            }

            ConsoleKeyInfo keyClick;
            do
            {
                int num;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Welcome {persons[1].FirstName} {persons[1].LastName} in the financial planner");
                Console.WriteLine("1. Enter ID user");
                Console.WriteLine("2. Add new user");
                Console.WriteLine("3. Enter transation");
                Console.WriteLine("4. Show transactiosn history for the month");
                Console.WriteLine("5. Show transaction history for the period");
                Console.WriteLine("Press key (Esc) to quit!");
                var keyboardKey = Console.ReadKey(intercept: true);
                if (keyboardKey.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                while (!char.IsDigit(keyboardKey.KeyChar))//exceptions
                {
                    Console.WriteLine("This is not a number!");
                    keyboardKey = Console.ReadKey();
                }
                Console.WriteLine("------------------------------------------");
                num = Convert.ToInt32(keyboardKey.KeyChar);
                if (num == '1')
                {
                    Console.WriteLine($"Enter ID user");
                    person.ID = Console.ReadLine();
                    Console.WriteLine($"Welcome {person.ID}");
                }
                else if (num == '2')
                {
                    Console.WriteLine($"Add new user");
                    Console.WriteLine($"Enter FirstName");
                    person.FirstName = Console.ReadLine();
                    Console.WriteLine($"Enter LastName");
                    person.LastName = Console.ReadLine();
                    Console.WriteLine($"Welcome {person.FirstName} {person.LastName}");

                }
                else if (num == '3')
                {
                    //uzytkownik, tutaj takze podkategorie do wyboru,jedzenie, czynsz, kredyt, dom, edukacja
                    //oraz tutaj wczytac date tranzakcji oraz kwote?
                    Console.WriteLine($"Enter transation");
                    int i = 1;
                    foreach (Category category in Enum.GetValues(typeof(Category)))//dzieki temu mozna dodawac kolejne kategorie w enum 
                    {
                        Console.WriteLine($"{i++} {category}");
                    }
                    keyboardKey = Console.ReadKey(intercept: true);
                    while (!char.IsDigit(keyboardKey.KeyChar))//exceptions
                    {
                        Console.WriteLine("This is not a number!");
                        keyboardKey = Console.ReadKey();
                    }
                    num = Convert.ToInt32(keyboardKey.KeyChar);
                    if (num == '1')
                    {
                        Console.WriteLine($"{Category.Home} , Enter expenditure");//enter here also day
                    }
                    else if (num == '2')
                    {
                        Console.WriteLine($"{Category.Education} , Enter expenditure");//enter here also day
                    }
                }
                else if (num == '4')
                {
                    Console.WriteLine($"Transactiosn history for the month");
                }
                else if (num == '5')
                {
                    Console.WriteLine($"Transactiosn history for the period");
                }
                else
                {
                    Console.WriteLine("Select again");
                }
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("Do you want to close the application, if yes press Y, press any key to continue!");
                keyClick = Console.ReadKey();
                Console.Clear();
            }
            while (keyClick.Key != ConsoleKey.Y);

        }
    }
}
