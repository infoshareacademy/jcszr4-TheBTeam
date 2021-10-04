using System.Collections.Generic;

namespace TheBTeam.ConsoleApp
{
    public class Person
    {
        public List<string> listPerson = new List<string>(new string[] { });
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ID { get; set; }
        public bool isActive { get; set; }
        public decimal balance { get; set; }
        public string currency { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public string company { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string registered { get; set; }

    }
}
