using Microsoft.AspNetCore.Mvc;
using StartupDemo.Data.Dtos.Request;
using StartupDemoCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IReportServices _reportServices;

        public UserController(IUserService userService, IReportServices reportServices)
        {
            _userService = userService;
           _reportServices = reportServices;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> CreateUserAsync([FromBody]RegisterationRequestDto userRequest)
        {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
               var registeredUser =  await _userService.RegisterAsync((userRequest));
            return StatusCode(200, "Registeration was successful");
        }

        [HttpGet("ReportByDateRange")]
        public async Task<IActionResult> GeTReportByDateRange(DateTime startDate, DateTime endDate, int salesId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var report = await _reportServices.GetReportByDateRange(startDate, endDate, salesId);
            return StatusCode((int)report.ResponseCode, report);
        }

        [HttpGet("GetAllReports")]
        public async Task<IActionResult> GetAllReport()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var listOfReport = await _reportServices.GetAllReportAsync();
            return StatusCode((int)listOfReport.ResponseCode, listOfReport);
        }

        [HttpPost("CreateReport")]
        public  async Task<IActionResult> CreateReport(CreateReportRequestDto report)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newReport = await _reportServices.CreateReportAsync(report);
            return StatusCode((int)newReport.ResponseCode, newReport);
        }

        [HttpPost("CreateReports")]
        public async Task<IActionResult> CreateMultipleReports(IEnumerable<CreateReportRequestDto> reports)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newReports = await _reportServices.CreateMultipleReports(reports);
            return StatusCode((int)newReports.ResponseCode, newReports);
        }
    }
}