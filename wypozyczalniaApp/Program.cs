using wypozyczalniaApp;

public class Wypozyczalnia {

       public static Library _library = new Library();

    public static void Main(string[] args)
    {

        _library.AddReader(100001, "Jan", "Kowalski", 97102002154, "jan.kowalski@example.com", "+48 500 500 500");
        _library.AddReader(100002, "Jan", "Kowalski", 99090900111, "piotr.nowak@example.com", "+48 400 400 400");


        //Dodanie przykładowych książek 
        _library.AddBook("Władca Pierścieni", "J.R.R. Tolkien", "9788377582558", new DateTime(2012, 1, 1), "", "Fantastyka");
        _library.AddBook("Diune", "Frank Herbert", "9788383381602", new DateTime(2024, 1, 1), "", "Science Fiction");

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== BIBLIOTEKA ===");
            Console.WriteLine("1. Zarządzanie książkami");
            Console.WriteLine("2. Zarządzanie użytkownikami");
            Console.WriteLine("3. Zakończ");
            Console.Write("Wybierz opcję: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ManageBooks();
                    break;
                case "2":
                    ManageReaders();
                    break;

                case "3":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Nieprawidłowa opcja! Naciśnij dowolny klawisz...");
                    Console.ReadKey();
                    break;
            }
        }

    }

    private static void ManageBooks()
    {
        


        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Zarządzanie książkami ===");
            Console.WriteLine("1. Dodaj książkę");
            Console.WriteLine("2. Usuń książkę");
            Console.WriteLine("3. Wyświetl książki");
            Console.WriteLine("4. Powrót do głównego menu");
            Console.WriteLine("5. Zakończ");
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

                    _library.AddBook(title, author, isbn, releaseDate, description, genre);
                    Console.ReadKey();
                    break;

                case "2":
                    Console.Write("Podaj ISBN książki do usunięcia: ");
                    string? isbnToRemove = Console.ReadLine();
                    if (!string.IsNullOrEmpty(isbnToRemove))
                    {
                        _library.RemoveBook(isbnToRemove);
                    }
                    else
                    {
                        Console.WriteLine("ISBN nie może być pusty!");
                    }
                    Console.ReadKey();
                    break;

                case "3":
                    _library.DisplayBooks();
                    Console.ReadKey();
                    break;
                case "4":
                    Main([]);
                    Console.ReadKey();
                    break; ;
                case "5":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Nieprawidłowa opcja! Naciśnij dowolny klawisz...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void ManageReaders()
    { 

        Random generator = new Random();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ZARZĄDZANIE UŻYTKOWNIKAMI ===");
            Console.WriteLine("1. Dodaj użytkownika");
            Console.WriteLine("2. Usuń użytkownika");
            Console.WriteLine("3. Wyświetl użytkowników");
            Console.WriteLine("4. Powrót");
            Console.Write("Wybierz opcję: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    long libraryID = long.Parse(generator.Next(0, 1000000).ToString("D4"));
                    Console.Write($"Wygenerowany numer czytelnika: {libraryID} \n");
                    Console.Write("Podaj imię: ");
                    string? firstName = Console.ReadLine();
                    Console.Write("Podaj nazwisko: ");
                    string? lastName = Console.ReadLine();
                    Console.Write("Podaj PESEL: ");
                    long pesel = long.Parse(Console.ReadLine());
                    Console.Write("Podaj adres e-mail: ");
                    string? email = Console.ReadLine();
                    Console.Write("Podaj numer telefonu: ");
                    string? phoneNumber = Console.ReadLine();
                    _library.AddReader(libraryID,firstName, lastName, pesel, email, phoneNumber);
                    Console.ReadKey();
                    break;
          
                case "2":
                    Console.Write("Podaj PESEL użytkownika do usunięcia: ");
                    long peselToRemove = long.Parse(Console.ReadLine());
                    if (peselToRemove == 0)
                    {
                        Console.WriteLine("PESEL nie może być pusty!");
                    }
                    else
                    {
                        _library.RemoveReader(peselToRemove);
                    }
                    Console.ReadKey();
                    break;  
                case "3":
                    _library.DisplayReaders();
                    Console.ReadKey();
                    break;

                case "4":
                    Main([]);
                    Console.ReadKey();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja! Naciśnij dowolny klawisz...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}

