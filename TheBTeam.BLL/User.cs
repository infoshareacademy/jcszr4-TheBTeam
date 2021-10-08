namespace TheBTeam.BLL
{
    public class User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string iD { get; set; }
        public bool isActive { get; set; }
        public decimal balance { get; set; }
        public string currency { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public string company { get; set; }
        public string email { get; set; }
        private string phone { get; set; }
        private string address { get; set; }
        private string registered { get; set; }
        public User(string firstname, string lastname,string id)
        {
            firstName = firstname;
            lastName = lastname;
            iD = id;
        }
    }


}
