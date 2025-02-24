namespace wypozyczalniaApp.Models
{
    public class Reader : Person
    {
        public long LibraryID { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        //public int BooksBorrowedQuantity { get; set; }

        public Reader() : base("", "", 0)
        {
        }
        public Reader(long libraryID, string firstName, string lastName, long pesel, string phone, string email) : base(firstName, lastName, pesel)
        {
            LibraryID = libraryID;
            Phone = phone;
            Email = email;
        }

        public override string ToString()
        {
            return base.ToString() + $",Numer czytelnika: {LibraryID}, Telefon: {Phone}, Email: {Email}";
        }
    }
}
