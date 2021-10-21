namespace TheBTeam.BLL
{
    public abstract class Category
    {
        public string Name;

        protected Category(string typeName)
        {
            Name = typeName;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}