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

            var persons = new Dictionary<int, Person>()//wypelnianie slownika
            {
                 { 1, new Person { FirstName="Santiago", LastName="Castillo", ID="615b4be6281be025752c8800"
                 ,balance = 2354.13m,currency="PLN",age=32,gender="male",company="RETRACK"
                 ,email="santiagocastillo@retrack.com",phone="+48 (949) 435-2718"
                 ,address="490 Poplar Avenue, Skyland, Washington, 3188",registered="2017-03-11T07:27:26 -01:00"} }
            };
            foreach (var index in Enumerable.Range(1, 1))
            {
                Console.WriteLine($"Person {index} is {persons[index].FirstName} {persons[index].LastName}");
            }

            //tutaj wczytujemy z pliku i czytamy imie,nazwisko,kategoria,planowane wydatki,rzeczywiste wydatki, data tranzakcji
            //moze jakas lista np: albo cos lepszego aby czytac wszystkie dane trzeba wczytac je do jednej bazy
            //ponizej ustawilem jakies dane defult
            List<string> planerList = new List<string>(new string[] { });
            person.FirstName = "Gal";
            person.LastName = "Anonim";
            person.listPerson.Add($"{ person.FirstName}  {person.LastName}");
            decimal actualExpansees = 1000;
            decimal plannedExpansees = 1500;
            planerList.Add(person.FirstName);
            planerList.Add(person.LastName);
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
                Console.WriteLine($"Witaj {person.FirstName} {person.LastName} w aplikacji planowania finansow");
                Console.WriteLine("1. Podaj ID uzytkownika");
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
                    Console.WriteLine($"Podaj nazwe uzytkownika Id:");
                    person.ID = Console.ReadLine();
                    Console.WriteLine($"Witaj {person.ID}");
                }
                else if (num == '2')
                {
                    Console.WriteLine($"Podaj nazwe uzytkownika");
                    Console.WriteLine($"Wpisz imie");
                    person.FirstName = Console.ReadLine();
                    Console.WriteLine($"Wpisz Nazwisko");
                    person.LastName = Console.ReadLine();
                    Console.WriteLine($"Witaj {person.FirstName} {person.LastName}");
                    person.listPerson.Add($"{ person.FirstName}  {person.LastName}");
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
                Console.WriteLine($"Pokaz liste wejsciowa");
                Console.WriteLine("-----------------------------------------------");


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
