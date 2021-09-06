using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class PublishersController : ControllerBase
    {
        private readonly PublisherService _publisherService;
        private ILogger<PublishersController> _logger;

        public PublishersController(PublisherService publisherService, ILogger<PublishersController> logger)
        {
            _publisherService = publisherService;
            _logger = logger;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int page)
        {
            //_logger.LogError($"sortBy: {sortBy}. This is very dangerius worning");
            var allPublishers = _publisherService.GetAllPublishers(sortBy, searchString, page);
            return Ok(allPublishers);
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _publisher = _publisherService.GetPublisherById(id);
            if(_publisher != null)
            {
                return Ok(_publisher);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _publisherData = _publisherService.GetPublisherData(id);
            if(_publisherData != null)
            {
                return Ok(_publisherData);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher(PublisherVM  publisher)
        {
            var newPublisher = _publisherService.AddPublisher(publisher);
            return Created(nameof(AddPublisher), newPublisher);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publisherService.DeletePublisherById(id);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
