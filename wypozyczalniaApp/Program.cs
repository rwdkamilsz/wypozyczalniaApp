using wypozyczalniaApp;

public class Wypozyczalnia {

    
    public static void Main(string[] args)
    {
        Library library = new Library();

        //Dodanie przykładowych książek 
        library.AddBook("Władca Pierścieni", "J.R.R. Tolkien", "9788377582558", new DateTime(2012, 1, 1), "", "Fantastyka");
        library.AddBook("Diune","Frank Herbert","9788383381602", new DateTime(2024, 1, 1),"","Science Fiction");


        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== BIBLIOTEKA ===");
            Console.WriteLine("1. Dodaj książkę");
            Console.WriteLine("2. Usuń książkę");
            Console.WriteLine("3. Wyświetl książki");
            Console.WriteLine("4. Zakończ");
            Console.Write("Wybierz opcję: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Podaj tytuł: ");
                    string? title = Console.ReadLine();
                    Console.Write("Podaj autora: ");
                    string? author = Console.ReadLine();
                    Console.Write("Podaj ISBN: ");
                    string? isbn = Console.ReadLine();
                    Console.Write("Podaj rok wydania (YYYY-MM-DD): ");
                    DateTime releaseDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Podaj gatunek: ");
                    string? genre = Console.ReadLine();
                    Console.Write("Podaj opis (opcjonalnie): ");
                    string? description = Console.ReadLine();

                    library.AddBook(title, author, isbn, releaseDate, description, genre);
                    Console.ReadKey();
                    break;
 
                case "2":
                    Console.Write("Podaj ISBN książki do usunięcia: ");
                    string? isbnToRemove = Console.ReadLine();
                    if (!string.IsNullOrEmpty(isbnToRemove))
                    {
                        library.RemoveBook(isbnToRemove);
                    }
                    else
                    {
                        Console.WriteLine("ISBN nie może być pusty!");
                    }
                    Console.ReadKey();
                    break;
                    
                case "3":
                    library.DisplayBooks();
                    Console.ReadKey();
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Nieprawidłowa opcja! Naciśnij dowolny klawisz...");
                    Console.ReadKey();
                    break;
            }
        }

    }
}

