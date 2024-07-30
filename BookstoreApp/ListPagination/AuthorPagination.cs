using BookstoreApp.Entities;

namespace BookstoreApp.ListPagination
{
    public class AuthorPagination
    {
        public List<Author> Authors { get; set; } = new List<Author>();
        public int totalPages { get; set; }
        public int page {  get; set; }
    }
}
