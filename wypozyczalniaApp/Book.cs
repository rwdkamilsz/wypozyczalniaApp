namespace wypozyczalniaApp
{
    public class Book : Item
    {
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }
        public string Genre { get; set; }

        public Book(string title, string author, string isbn, DateTime releaseDate, string? description, bool isAvailable, string genre)
            : base(title, isAvailable)
        {
            
            Author = author;
            ISBN = isbn;
            ReleaseDate = releaseDate;
            Description = description;
            Genre = genre;
        }

        public override string ToString()
        {
            return base.ToString() + $", Autor: {Author}, ISBN: {ISBN}";
        }
    }
}
