using BookstoreApp.Data;
using BookstoreApp.Entities;
using BookstoreApp.ListPagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Controllers
{
    [Route("authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public AuthorsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAllAuthors(int page = 1,float pageSize = 10f)
        {

            var pageCount = Math.Ceiling(_dataContext.Authors.Count() / pageSize);

            var authors = await _dataContext.Authors.Skip((page - 1) * (int)pageSize).Take((int)pageSize).ToListAsync();

            var pagination = new AuthorPagination
            {
                Authors = authors,
                page = page,
                totalPages = (int)pageCount
            };


            return Ok(pagination);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Author>> GetAuthor(Guid id)
        {
            var author = await _dataContext.Authors.FindAsync(id);

            if(author is null)
            {
                return NotFound("Author not found");
            }

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<List<Author>>> AddAuthor(Author author)
        {
            if (author.FirstName.Length > 100 || author.LastName.Length > 100)
            {
                return BadRequest("First or Last Name are too long!!");
            }

            _dataContext.Authors.Add(author);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Authors.ToListAsync());
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<List<Author>>> UpdateAuthor(Author updateAuthor)
        {
            var dbAuthor = await _dataContext.Authors.FindAsync(updateAuthor.Id);

            if (updateAuthor.FirstName.Length > 100 || updateAuthor.LastName.Length > 100)
            {
                return BadRequest("First or Last Name are too long!!");
            }

            if (dbAuthor is null )
            {
                return NotFound("Author is not found");
            }

            dbAuthor.FirstName = updateAuthor.FirstName;
            dbAuthor.LastName = updateAuthor.LastName;
            dbAuthor.BookCount = updateAuthor.BookCount;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Authors.ToListAsync());
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<List<Author>>> DeleteAuthor(Guid id)
        {
            var dbAuthor = await _dataContext.Authors.FindAsync(id);

            if (dbAuthor is null)
            {
                return NotFound("Author is not found");
            }

            _dataContext.Authors.Remove(dbAuthor);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Authors.ToListAsync());
        }

    }
}
