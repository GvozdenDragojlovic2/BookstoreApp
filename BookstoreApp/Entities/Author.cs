namespace BookstoreApp.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int BookCount { get; set; } = 0;
        public static Book[] books { get; set; } //= new Book[10];
    }
}
