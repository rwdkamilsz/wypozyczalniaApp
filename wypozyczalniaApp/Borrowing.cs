namespace wypozyczalniaApp.Models
{
    public class Borrowing
    {
        public string ISBN { get; set; }
        public long ReaderID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool Returned { get; set; }

        public Borrowing(string isbn, long readerID, DateTime borrowDate, DateTime dueDate, bool returned = false)
        {
            ISBN = isbn;
            ReaderID = readerID;
            BorrowDate = borrowDate;
            DueDate = dueDate;
            ReturnDate = null;
            Returned = returned;
        }

        public override string ToString()
        {
            string status = ReturnDate.HasValue ? $"Zwrócono: {ReturnDate.Value:yyyy-MM-dd}" : $"Do zwrotu: {DueDate:yyyy-MM-dd}";
            return $"ISBN: {ISBN}, ID Czytelnika: {ReaderID}, Wypożyczono: {BorrowDate:yyyy-MM-dd}, Zwrócona: { (Returned ? "TAK" : "NIE")} ";
            
        }
    }
}