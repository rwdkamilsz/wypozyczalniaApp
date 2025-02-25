using wypozyczalniaApp.Models;

namespace wypozyczalniaApp
{
    public class Library
    {
        private JsonDatabase _database;


        public Library()
        {
            _database = new JsonDatabase();
        }

        public void AddBook(string title, string author, string isbn, DateTime releaseDate, string? description, string genre)
        {
            if (_database.FindBookByISBN(isbn) != null)
            {
                Console.WriteLine("Książka o tym ISBN już istnieje!");
                return;
            }

            var book = new Book(title, author, isbn, releaseDate, description, true, genre);
            _database.AddBook(book);
            Console.WriteLine("Książka dodana!");
        }

         public void AddReader(long libraryID, string firstName, string lastName, long pesel, string? email, string? phoneNumber)
        {
            if (_database.FindReader(libraryID, pesel) != null)
            {
                Console.WriteLine("Osoba z tym numerem lub PESELem już istnieje!");
                return;
            } 

            var reader = new Reader( firstName, lastName, pesel, phoneNumber, email);
            _database.AddReader(reader);
            Console.WriteLine("Czytelnik dodany!");

        }
        public void UpdateBook(string isbn, string? newTitle, string? newAuthor, string? newReleaseDate, string? newDescription, string? newGenre, bool isAvailable =true)
        {
            var book = _database.FindBookByISBN(isbn);
            if (book == null)
            {
                Console.WriteLine("Nie znaleziono książki o podanym ISBN.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                book.Title = newTitle;
            }

            if (!string.IsNullOrWhiteSpace(newAuthor))
            {
                book.Author = newAuthor;
            }

            if (!string.IsNullOrWhiteSpace(newReleaseDate))
            {
                if (DateTime.TryParse(newReleaseDate, out DateTime parsedDate))
                {
                    book.ReleaseDate = parsedDate;
                }
                else
                {
                    Console.WriteLine("Podany format daty jest nieprawidłowy. Data nie zostanie zaktualizowana.");
                }
            }

            if (!string.IsNullOrWhiteSpace(newDescription))
            {
                book.Description = newDescription;
            }

            if (!string.IsNullOrWhiteSpace(newGenre))
            {
                book.Genre = newGenre;
            }
            book.IsAvailable = isAvailable;
            _database.SaveBooks();

            Console.WriteLine("Dane książki zostały zaktualizowane.");
        }

        public void RemoveBook(string isbn)
        {
            if (_database.RemoveBookByISBN(isbn))
            {
                Console.WriteLine("Książka usunięta!");
            }
            else
            {
                Console.WriteLine("Nie znaleziono książki!");
            }
        }
        public void RemoveReader(long libraryID)
        {
            if (_database.RemoveReaderByLibraryID(libraryID))
            {
                Console.WriteLine("Książka usunięta!");
            }
            else
            {
                Console.WriteLine("Nie znaleziono książki!");
            }
        }

        public void DisplayBooks()
        {
            List<Book> books = _database.GetAllBooks();
            Console.WriteLine("\n=== Lista Książek ===");
            if (books.Count == 0)
            {
                Console.WriteLine("Brak książek.");
            }
            else
            {
                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
            }
        }

        public void DisplayReaders()
        {
            List<Reader> readers = _database.GetAllReaders();
            Console.WriteLine("\n=== Lista czytelników ===");
            if (readers.Count == 0)
            {
                Console.WriteLine("Brak czytelników.");
            }
            else
            {
                foreach (var reader in readers)
                {
                    Console.WriteLine(reader);
                }
            }
        }
        public void BorrowBook(string isbn, long readerID, DateTime? borrowedDate = null, int loanDays = 14)
        {
            var book = _database.FindBookByISBN(isbn);
            if (book == null)
            {
                Console.WriteLine("Nie znaleziono książki!");
                return;
            }

            if (!book.IsAvailable)
            {
                Console.WriteLine("Książka nie jest dostępna!");
                return;
            }

            var reader = _database.FindReader(readerID, null);
            if (reader == null)
            {
                Console.WriteLine("Nie znaleziono czytelnika!");
                return;
            }

            var borrowDate = borrowedDate ?? DateTime.Now;
            var dueDate = borrowDate.AddDays(loanDays);
            var borrowing = new Borrowing(isbn, readerID, borrowDate, dueDate);

            _database.AddBorrowing(borrowing);

            book.IsAvailable = false;

            Console.WriteLine($"Wypożyczono książkę! Termin zwrotu: {dueDate:yyyy-MM-dd}");
        }

        public void ReturnBook(string isbn, long readerID)
        {
            if (_database.ReturnBook(isbn, readerID))
            {
                Console.WriteLine("Książka zwrócona!");
            }
            else
            {
                Console.WriteLine("Nie znaleziono wypożyczenia!");
            }
        }

        public void DisplayBorrowings(long? readerID = null, string mode = "all")
        {
            List<Borrowing> borrowings;

            if (readerID.HasValue)
            {
                borrowings = _database.GetBorrowingsByReaderID(readerID.Value);
                Console.WriteLine($"\n=== Lista wypożyczeń czytelnika ID {readerID} ===");
            }
            else
            {
                borrowings = _database.GetAllBorrowings();
                Console.WriteLine("\n=== Lista wszystkich wypożyczeń ===");
            }

            if (borrowings.Count == 0)
            {
                Console.WriteLine("Brak wypożyczeń.");
            }

            if (mode != "all")
            {
                foreach (var borrowing in borrowings.Where(b => b.Returned == false))
                {

                    var book = _database.FindBookByISBN(borrowing.ISBN);
                    var reader = _database.FindReader(borrowing.ReaderID, null);
                    Console.WriteLine($"{book?.Title} - {reader?.FirstName} {reader?.LastName}: {borrowing}");

                }
            }
            else
            {

                foreach (var borrowing in borrowings)
                {
                    var book = _database.FindBookByISBN(borrowing.ISBN);
                    var reader = _database.FindReader(borrowing.ReaderID, null);
                    Console.WriteLine($"{book?.Title} - {reader?.FirstName} {reader?.LastName}: {borrowing}");
                }
            }
            }
    }

}
