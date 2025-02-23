namespace wypozyczalniaApp
{
    public abstract class Item
    {
        private static int _id = 0;
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsAvailable { get; set; }

        public Item(string title, bool isAvailable)
        {
            Id = _id++;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            IsAvailable = isAvailable;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Tytuł: {Title}, Dostępność: {(IsAvailable ? "Tak" : "Nie")}";
        }
    }
}
