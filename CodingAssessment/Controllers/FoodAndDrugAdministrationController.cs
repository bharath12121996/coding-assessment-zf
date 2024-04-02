using CodingAssessment.Application.Interfaces;
using CodingAssessment.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CodingAssessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(ExceptionFilter))]
    public class FoodAndDrugAdministrationController : ControllerBase
    {
        private readonly IFoodAndDrugEnforcementService _fdeService;
        private readonly ILogger<FoodAndDrugAdministrationController> _logger;


        public FoodAndDrugAdministrationController(ILogger<FoodAndDrugAdministrationController> logger, IFoodAndDrugEnforcementService fdeService)
        {
            _logger = logger;
            _fdeService = fdeService;
        }

        [Route("SendEmail")]
        [HttpGet]
        public async Task<IActionResult> Getlistoffoodenforcementandsendemail()
        {

            _logger.LogInformation("Calling Get Getlistoffoodenforcementandsendemail");

            await _fdeService.GetFoodAndDrugEnforcementSendEmailAsync();

            return Ok();
        }

        [Route("Foodenforcement")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetlistoffoodenforcementAsync([FromQuery] Domain.Model.PagedRequest request)
        {
            _logger.LogInformation("Calling Get GetlistoffoodenforcementAsync");

            return Ok(await _fdeService.GetFoodAndDrugEnforcementAsync(request.pageNumber, request.pageSize));
        }
    }
}