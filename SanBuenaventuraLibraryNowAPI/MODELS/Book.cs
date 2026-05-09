namespace SanBuenaventuraLibraryNowAPI.Models
{
    public class book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public bool Available { get; set; }
        public int PublishedYear { get; set; }
    }
}
