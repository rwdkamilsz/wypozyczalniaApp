namespace wypozyczalniaApp.Models
{
    public abstract class Item
    {
        private static int _ItemId = 0;
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsAvailable { get; set; }

        public Item( string title, bool isAvailable)
        {
            Id = _ItemId++;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            IsAvailable = isAvailable;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Tytuł: {Title}, Dostępność: {(IsAvailable ? "Tak" : "Nie")}";
        }
    }
}
