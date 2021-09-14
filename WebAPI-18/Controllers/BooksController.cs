using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_18.Data.Models;
using WebAPI_18.Data.Services;
using WebAPI_18.Data.ViewModels;

namespace WebAPI_18.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BookService _bookService;
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("get-books")]
        public IActionResult GetAllBooks(string sortBy, string searchString, int page)
        {
            var allBooks = _bookService.GetAllBooks(sortBy, searchString, page);
            return Ok(allBooks);
        }

        [HttpPost("add-book-with-autors")]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            _bookService.AddBookWithAuthors(book);
            return Ok();
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            var _book = _bookService.GetBookById(id);
            if(_book != null)
            {
                return Ok(_book);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("update-book/{id}")]
        public IActionResult UpdateBookById(int id, BookVM book)
        {
            try
            {
                _bookService.UpdateBook(id, book);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-book/{id}")]
        public IActionResult DeleteBook(int id)
        {
            _bookService.DeleteBook(id);
            return Ok();
        }
    }
}