using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanBuenaventuraLibraryNowAPI.MODELS;
using System.Collections.Generic;
using System.Linq;

namespace SanBuenaventuraLibraryNowAPI.Controller
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book
            {
                ID = 1,
                Title = "The Hobbit, or There and Back Again",
                Author = "J.R.R. Tolkien",
                Genre = "Fantasy, Adventure, Classic",
                Available = true,
                PublishedYear = 1937
            },
            new Book
            {
                ID = 2,
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
            // FIX: Search for the single book
            var book = books.FirstOrDefault(x => x.ID == id);

            // FIX: Check the 'book' variable, not the 'books' list
            if (book == null)
            {
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found"
                });
            }

            return Ok(new
            {
                status = "success",
                data = book, // FIX: Return only the found book, not the whole list
                message = "Book retrieved"
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            // Simple ID generation (for demo purposes)
            newBook.ID = books.Count > 0 ? books.Max(b => b.ID) + 1 : 1;

            books.Add(newBook);

            return CreatedAtAction(nameof(GetById),
                new { id = newBook.ID },
                new
                {
                    status = "success",
                    data = newBook,
                    message = "Book created",
                });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(x => x.ID == id);

            if (book == null)
            {
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found"
                });
            }

            // Update properties
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
            var book = books.FirstOrDefault(x => x.ID == id);

            if (book == null)
            {
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found"
                });
            }

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