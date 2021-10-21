namespace TheBTeam.BLL
{
    public class Currency
    {
        public string Name;
        public Currency(string currency)
        {
            Name = currency;
        }

        public string ToString()
        {
            return Name;
        }
    }
}