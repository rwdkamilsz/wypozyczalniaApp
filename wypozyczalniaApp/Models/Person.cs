using System.Security.Cryptography;

namespace wypozyczalniaApp.Models
{
    public class Person
    {
        private static int _PersonId = 0;
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public long? Pesel { get; set; }

        public Person(string firstName, string lastName, long pesel)
        {
            Id = _PersonId++;
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
        }


        public override string ToString()
        {
            return $"Imię: {FirstName}, Nazwisko: {LastName}, PESEL: {Pesel}";
        }
    }
}
