namespace TheBTeam.BLL
{
    public class CategoryOfTransaction
    {
        public string Name { get; set; }

        public CategoryOfTransaction(string category)
        {
            Name = category;
        }
    }
}