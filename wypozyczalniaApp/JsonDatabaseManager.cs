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

        public JsonDatabase()
        {
            _books = LoadFromFile<Book>("books.json");
            _readers = LoadFromFile<Reader>("readers.json");
        }

        private List<T> LoadFromFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }
            var options = new JsonSerializerOptions
            {
                IncludeFields = false
            };
            try
            {
                string json = File.ReadAllText(filePath);
                Debug.WriteLine($"Z try: {filePath}");

                return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Z catch \n {ex.Message}");
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

        public void AddBook(Book book)
        {
            _books.Add(book);
            SaveToFile(_books, "books.json");
        }
        public void AddReader(Reader reader)
        {
            _readers.Add(reader);
            SaveToFile(_readers, "readers.json");

        }

        public void AddItem<T>(T item, List<T> items, string filePath)
        {
            items.Add(item);
            SaveToFile(items, filePath);
        }

        public bool RemoveBookByISBN(string isbn)
        {
            Book? bookToRemove = FindBookByISBN(isbn);
            if (bookToRemove != null)
            {
                _books.Remove(bookToRemove);
                SaveToFile(_books, "books.json");
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
                SaveToFile(_readers, "readers.json");
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

        public List<Book> GetAllBooks()
        {
            return _books;
        }
        public List<Reader> GetAllReaders()
        {
            return _readers;
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

    }
}
