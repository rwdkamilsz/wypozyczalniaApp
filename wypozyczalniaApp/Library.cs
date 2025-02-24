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

            var reader = new Reader(libraryID, firstName, lastName, pesel, phoneNumber, email);
            _database.AddReader(reader);
            Console.WriteLine("Czytelnik dodany!");

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

       
    }
}
