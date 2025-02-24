namespace wypozyczalniaApp
{
    public class Library
    {
        private JsonDatabase _database = new JsonDatabase("books.json");

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
    }
}
