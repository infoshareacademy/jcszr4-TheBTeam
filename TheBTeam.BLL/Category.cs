using System;

namespace TheBTeam.BLL
{
    public class Category
    {
        enum Categories//here you can add new category
        {
            Home,
            Education,
        }
        public void SelectCategory()
        {
            int i = 0; int numberFromConsole;
            foreach (Categories categories in Enum.GetValues(typeof(Categories)))
            {
                Console.WriteLine($"{i++}. {categories}");
            }
            while (!int.TryParse(Console.ReadLine(), out numberFromConsole))
            {
                Console.WriteLine("This is not a number!");
            }
            switch ((Categories)Enum.ToObject(typeof(Categories), numberFromConsole))
            {
                case Categories.Home:
                    Console.WriteLine($"Enter expenses for {Categories.Home}");
                    break;
                case Categories.Education:
                    Console.WriteLine($"Enter expenses for {Categories.Education}");
                    break;
                default:
                    Console.WriteLine("Select again");
                    break;
            }
        }
    }
}
