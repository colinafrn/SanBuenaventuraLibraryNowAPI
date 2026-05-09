using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanBuenaventuraLibraryNowAPI.Controller;
using SanBuenaventuraLibraryNowAPI.MODELS;


namespace SanBuenaventuraLibraryNowAPI.Controller   
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<book> books = new List<book>
        {
            new book
            {
                Id = 1,
                Title = "The Hobbit, or There and Back Again",
                Author = "J.R.R. Tolkien",
                Genre = "Fantasy, Adventure, Classic",
                Available = true,
                PublishedYear = 1937

            },
            new book
             {
                Id = 2,
                Title = "The Harry Potter",
                Author = "J.K. Rowling",
                Genre = "Fantasy, drama, coming-of-age fiction, mystery, and adventure.",
                Available = true,
                PublishedYear = 2007

            }
        };
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                status = "success",
                data = books,
                message = "Books Retrieved.",
            });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (books == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found"
                });
            return Ok(new
            {
                status = "success",
                data = books,
                message = "Book retrieved"
            });
        }
        [HttpPost]
        public IActionResult Create([FromBody] book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById),
                new { id = newBook.Id },
                new
                {
                    status = "success",
                    data = newBook,
                    message = "Book created",

                });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,
            [FromBody] book updateBook)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "book not found"
                });
            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublishedYear = updateBook.PublishedYear;

            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book Updated"
            });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "book not found"
                });
            books.Remove(book);

            return Ok(new
            {
                status = "success",
                data = (object?)null,
                message = "Book removed."
            });
           }
         }
        }
    
