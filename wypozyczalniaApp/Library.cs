using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace wypozyczalniaApp
{
    public class Library
    {
        private List<Book> books = new List<Book>();

        public void AddBook(string title, string author, string isbn, DateTime releaseDate, string? description, string genre)
        {
            if (books.Any(b => b.ISBN == isbn))
            {
                Console.WriteLine("Książka o tym ISBN już istnieje!");
                return;
            }

            books.Add(new Book(title, author, isbn, releaseDate, description, true, genre));
            Console.WriteLine("Książka dodana!");
        }

        public void RemoveBook(string isbn)
        {
            var bookToRemove = books.FirstOrDefault(b => b.ISBN == isbn);
            if (bookToRemove != null)
            {
                books.Remove(bookToRemove);
                Console.WriteLine("Książka usunięta!");
            }
            else
            {
                Console.WriteLine("Nie znaleziono książki!");
            }
        }

        public void DisplayBooks()
        {
            Console.WriteLine("\n=== Lista Książek ===");
            if (books.Count == 0)
            {
                Console.WriteLine("Brak książek.");
            }
            else
            {
                foreach (var book in books)
                {
                    Console.WriteLine(book.ToString());
                }
            }
        }

    }
}
