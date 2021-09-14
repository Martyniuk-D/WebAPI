using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_18.Data.Services;
using WebAPI_18.Data.ViewModels;

namespace WebAPI_18.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;
        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("get-all-authors")]
        public IActionResult GetAllAuthors(string sortBy, string searchString, int page)
        {
            //_logger.LogError($"sortBy: {sortBy}. This is very dangerius worning");
            var allAuthors = _authorService.GetAllAuthors(sortBy, searchString, page);
            return Ok(allAuthors);
        }

        [HttpGet("get-authors-by-id/{id}")]
        public IActionResult GetAuthorsById(int id)
        {
            var _authors = _authorService.GetAuthorsById(id);
            if (_authors != null)
            {
                return Ok(_authors);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            var newAuthor = _authorService.AddAuthor(author);
            return Created(nameof(AddAuthor), newAuthor);
        }

        [HttpDelete("delete-authors-by-id/{id}")]
        public IActionResult DeleteAuthorsById(int id)
        {
            try
            {
                _authorService.DeleteAuthorsById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-authors-by-id/{id}")]
        public IActionResult UpdateAuthorsById(int id, AuthorVM authorVM)
        {
            try
            {
                _authorService.UpdateAuthors(id, authorVM);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
