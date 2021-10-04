using System;
using System.Collections.Generic;

namespace TheBTeam.ConsoleApp
{
    class Program
   {
        enum Category
        {
            Dom,
            Edukacja,
            Kredyt,
        }
        enum Days
        {
            Poniedzialek,
            Wtorek,
            Sroda,
            Czwartek,
            Piatek,
            Sobota,
            Niedziela,
        }
        static void Main(string[] args)
        {
            Person person = new Person();
            //tutaj wczytujemy z pliku i czytamy imie,nazwisko,kategoria,planowane wydatki,rzeczywiste wydatki, data tranzakcji
            //moze jakas lista np: albo cos lepszego aby czytac wszystkie dane trzeba wczytac je do jednej bazy
            //ponizej ustawilem jakies dane defult
            List<string> planerList = new List<string>(new string[] { });
            person.Name = "Gal";
            person.SurName = "Anonim";
            person.listPerson.Add($"{ person.Name}  {person.SurName}");
            decimal actualExpansees = 1000;
            decimal plannedExpansees = 1500;
            planerList.Add(person.Name);
            planerList.Add(person.SurName);
            planerList.Add(Category.Dom.ToString());
            planerList.Add(plannedExpansees.ToString());//planowane wydatki
            planerList.Add(actualExpansees.ToString());//rzeczywiste wydatki
            DateTime thisDay = DateTime.Today;
            planerList.Add(thisDay.ToString("D"));

            ConsoleKeyInfo keyClick;
            do
            {
                int num;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Witaj {person.Name} {person.SurName} w aplikacji planowania finansow");
                Console.WriteLine("1. Podaj nazwe uzytkownika");
                Console.WriteLine("2. Wprowadz nowego uzytkownika");
                Console.WriteLine("3. Wprowadz tranzakcje");
                Console.WriteLine("4. Pokaz historie tranzakcji z danego miesiaca");
                Console.WriteLine("5. Pokaz historie tranzakcji z okresu");
                Console.WriteLine("Wcisnij klawisz (Esc) jesli chcesz przerwac!");
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
                    Console.WriteLine($"Podaj nazwe uzytkownika");
                    Console.WriteLine($"Wpisz imie");
                    person.Name = Console.ReadLine();
                    Console.WriteLine($"Wpisz Nazwisko");
                    person.SurName = Console.ReadLine();
                    Console.WriteLine($"Witaj {person.Name} {person.SurName}");
                    person.listPerson.Add($"{ person.Name}  {person.SurName}");
                }
                else if (num == '2')
                {
                    Console.WriteLine($"Podaj nazwe uzytkownika");
                    Console.WriteLine($"Wpisz imie");
                    person.Name = Console.ReadLine();
                    Console.WriteLine($"Wpisz Nazwisko");
                    person.SurName = Console.ReadLine();
                    Console.WriteLine($"Witaj {person.Name} {person.SurName}");
                    person.listPerson.Add($"{ person.Name}  {person.SurName}");
                }
                else if (num == '3')
                {
                    //uzytkownik, tutaj takze podkategorie do wyboru,jedzenie, czynsz, kredyt, dom, edukacja
                    //oraz tutaj wczytac date tranzakcji oraz kwote?
                    Console.WriteLine($"Wprowadz tranzakcje, wybierz kategorie");
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
                        Console.WriteLine($"{Category.Dom} , wpisz kwote i dzien");
                    }
                    else if (num == '2')
                    {
                        Console.WriteLine($"{Category.Edukacja} , wpisz kwote i dzien");
                    }
                }
                else if (num == '4')
                {
                    Console.WriteLine($"Pokaz historie tranzakcji z danego miesiaca");
                }
                else if (num == '5')
                {
                    Console.WriteLine($"Pokaz historie tranzakcji z okresu");
                }
                else
                {
                    Console.WriteLine("Wybierz jeszcze raz");
                }
                Console.WriteLine($"Pokaz liste osob");
                Console.WriteLine("-----------------------------------------------");
                foreach (string item in person.listPerson)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine($"Pokaz liste wyjsciowa");
                Console.WriteLine("-----------------------------------------------");
                foreach (string item in planerList)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("Czy chcesz zamknac aplikacje, jesli tak nacisnij Y , nacisnij dowolny klawisz aby kontunuowac!");
                keyClick = Console.ReadKey();
                Console.Clear();
            }
            while (keyClick.Key != ConsoleKey.Y);

        }
    }
}
