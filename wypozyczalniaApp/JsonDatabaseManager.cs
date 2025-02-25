using System.Diagnostics;
using System.Text.Json;
using wypozyczalniaApp.Models;

namespace wypozyczalniaApp
{

    public class JsonDatabase
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true  // Changed to true
        };

        private List<Book> _books;
        private List<Reader> _readers;
        private List<Borrowing> _borrowings;

        public JsonDatabase()
        {
            _books = LoadFromFile<Book>("books.json");
            _readers = LoadFromFile<Reader>("readers.json");
            _borrowings = LoadFromFile<Borrowing>("borrowings.json");
        }

        private List<T> LoadFromFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            } 
            try
            {
                string json = File.ReadAllText(filePath);

                return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd odczytu JSON: {ex.Message}");
                return new List<T>();
            }
        }

        private void SaveToFile<T>(List<T> items, string? filePath)
        {
            try
            {
                string json = JsonSerializer.Serialize(items, _options);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd zapisu JSON: {ex.Message}");
            }
        }

        public List<Book> GetAllBooks()
        {
            return _books;
        }
        public List<Reader> GetAllReaders()
        {
            return _readers;
        }
        public List<Borrowing> GetAllBorrowings()
        {
            return _borrowings;
        }

        public void SaveBooks()
        {
            SaveToFile(_books, "books.json");
        }
        public void SaveReaders()
        {
            SaveToFile(_readers, "readers.json");
        }
        public void SaveBorrowings()
        {
            SaveToFile(_borrowings, "borrowings.json");
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
            SaveBooks();
        }
        public void AddReader(Reader reader)
        {
            _readers.Add(reader);
            SaveReaders();

        }
        public void AddBorrowing(Borrowing borrowing)
        {
            _borrowings.Add(borrowing);
            SaveBorrowings();
        }
      
        public bool RemoveBookByISBN(string isbn)
        {
            Book? bookToRemove = FindBookByISBN(isbn);
            if (bookToRemove != null)
            {
                _books.Remove(bookToRemove);
                SaveBooks();
                return true;
            }
            return false;
        }

        public bool RemoveReaderByLibraryID(long libraryID)
        {
            Reader? readerToRemove = FindReader(libraryID, null);
            if (readerToRemove != null)
            {
                _readers.Remove(readerToRemove);
                SaveReaders();
                return true;
            }
            return false;
        }
        public Book? FindBookByISBN(string isbn)
        {
            foreach (var book in _books)
            {
                if (book.ISBN == isbn)
                {
                    return book;
                }
            }
            return null;
        }

        
        public Reader? FindReader(long? libraryID, long? pesel)
        {
            foreach (var reader in _readers)
            {
                if (reader.LibraryID == libraryID || reader.Pesel == pesel)
                {
                    return reader;
                } 
            }

            return null;
        }
        public List<Borrowing> GetBorrowingsByReaderID(long libraryID)
        {
            return _borrowings.Where(b => b.ReaderID == libraryID && !b.ReturnDate.HasValue).ToList();
        }

        public bool ReturnBook(string isbn, long readerID)
        {
            var borrowing = _borrowings.FirstOrDefault(b =>
                b.ISBN == isbn &&
                b.ReaderID == readerID &&
                !b.ReturnDate.HasValue);

            if (borrowing != null)
            {
                borrowing.ReturnDate = DateTime.Now;
                borrowing.Returned = true;
                SaveBorrowings();

                var book = FindBookByISBN(isbn);
                if (book != null)
                {
                    book.IsAvailable = true;
                    SaveBooks();
                }
                return true;
            }
            return false;
        }
    }
}
