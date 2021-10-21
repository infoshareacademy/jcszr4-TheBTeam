using System.Collections.Generic;

namespace TheBTeam.BLL
{
    public class TypeLists
    {
        public readonly List<CategoryOfTransaction> TransactionsCategories = new List<CategoryOfTransaction>()
        {
            new CategoryOfTransaction("Home"),
            new CategoryOfTransaction("Car"),
            new CategoryOfTransaction("School"),
            new CategoryOfTransaction("Kids"),
            new CategoryOfTransaction("Commute"),
            new CategoryOfTransaction("Food"),
            new CategoryOfTransaction("Entertainment"),
            new CategoryOfTransaction("Eating Out"),
            new CategoryOfTransaction("Medicine"),
            new CategoryOfTransaction("Clothing"),
            new CategoryOfTransaction("Special"),
            new CategoryOfTransaction("Other"),
            new CategoryOfTransaction("Credit"),
            new CategoryOfTransaction("Living Charges")
        };

        public readonly List<Currency> Currencies = new List<Currency>()
        {
            new Currency("PLN"),
            new Currency("USD"),
            new Currency("EUR")
        };

        public readonly List<Gender> Genders = new List<Gender>()
        {
            new Gender("Male"),
            new Gender("Female"),
            new Gender("Want to be private")
        };
    }
}