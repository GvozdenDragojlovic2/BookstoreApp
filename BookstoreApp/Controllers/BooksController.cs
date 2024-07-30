using BookstoreApp.Data;
using BookstoreApp.Entities;
using BookstoreApp.ListPagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Controllers
{
    [Route("books")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public BooksController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAllBooks(int page = 1, float pageSize = 10f)
        {

            var pageCount = Math.Ceiling(_dataContext.Books.Count() / pageSize);

            var books = await _dataContext.Books.Skip((page - 1) * (int)pageSize).Take((int)pageSize).ToListAsync();

            var pagination = new BookPagination
            {
                Books = books,
                page = page,
                totalPages = (int)pageCount
            };

            return Ok(pagination);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Book>> GetBook(Guid id)
        {
            var book = await _dataContext.Books.FindAsync(id);

            if (book is null)
            {
                return NotFound("Book not found");
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<List<Book>>> AddBook(Book book)
        {
            if (book.Title.Length > 250 || book.Year < -5000 || book.Year > 999999 || book.Year == 0)
            {
                return BadRequest("Title or Year are not valid!");
            }

            _dataContext.Books.Add(book);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Books.ToListAsync());
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<List<Book>>> UpdateBook(Book updateBook)
        {
            var dbBook = await _dataContext.Books.FindAsync(updateBook.Id);

            if (updateBook.Title.Length > 250 || updateBook.Year < -5000 || updateBook.Year > 999999 || updateBook.Year == 0)
            {
                return BadRequest("Title or Year are not valid!");
            }

            if (dbBook is null)
            {
                return NotFound("Book is not found");
            }

            dbBook.Title = updateBook.Title;
            dbBook.Year = updateBook.Year;
            dbBook.Author = updateBook.Author;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Books.ToListAsync());
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<List<Book>>> DeleteBook(Guid id)
        {
            var dbBook = await _dataContext.Books.FindAsync(id);

            if (dbBook is null)
            {
                return NotFound("Book is not found");
            }

            _dataContext.Books.Remove(dbBook);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Books.ToListAsync());
        }

    }
}
