using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_18.Data.Services;

namespace WebAPI_18.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly LogsService _logService;
        public LogsController(LogsService logsService)
        {
            _logService = logsService;
        }

        [HttpGet("get-all-logs")]
        public IActionResult GetAllLogs()
        {
            try
            {
                var _allLogs = _logService.GetAllLogs();
                return Ok(_allLogs);
            }
            catch (Exception)
            {

                return BadRequest("Could not fetch logs");
            }
        }
    }
}
