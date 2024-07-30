using BookstoreApp.Entities;

namespace BookstoreApp.ListPagination
{
    public class BookPagination
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public int totalPages { get; set; }
        public int page { get; set; }

    }
}
