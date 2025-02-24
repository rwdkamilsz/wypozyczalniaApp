namespace wypozyczalniaApp.Models
{
    public class Reader : Person
    {
        private static int _ReaderId = 0;
        public long LibraryID { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        //public int BooksBorrowedQuantity { get; set; }

        public Reader() : base("", "", 0)
        {
        }
        public Reader(string firstName, string lastName, long pesel, string phone, string email) : base(firstName, lastName, pesel)
        {
            LibraryID = ++_ReaderId;
            Phone = phone;
            Email = email;
        }

        public override string ToString()
        {
            return base.ToString() + $", Numer czytelnika: {LibraryID}, Telefon: {Phone}, Email: {Email}";
        }
    }
}
