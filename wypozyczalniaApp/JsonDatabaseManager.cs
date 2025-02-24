using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace wypozyczalniaApp
{
    public class JsonDatabase
    {
        private readonly string _filePath;
        private List<Book> _books;

        public JsonDatabase(string filePath)
        {
            _filePath = filePath;
            _books = LoadFromFile();
        }

        private List<Book> LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Book>();
            }

            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd odczytu JSON: {ex.Message}");
                return new List<Book>();
            }
        }

        private void SaveToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(_books, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd zapisu JSON: {ex.Message}");
            }
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
            SaveToFile();
        }


        public bool RemoveBookByISBN(string isbn)
        {
            Book bookToRemove = FindBookByISBN(isbn);
            if (bookToRemove != null)
            {
                _books.Remove(bookToRemove);
                SaveToFile();
                return true;
            }
            return false;
        }


        public Book FindBookByISBN(string isbn)
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
    }
}
