using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyMeExam.Entities;

namespace MoneyMeExam.ApiService.Controllers
{
    [ApiController]
    [Route("api/loan-applications")]
    public class LoanApplicationsController : ControllerBase
    {
        private readonly ILogger<LoanApplicationsController> _logger;

        public LoanApplicationsController(ILogger<LoanApplicationsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<LoanApplication>), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetLoanApplications()
        {
            return Ok(new List<LoanApplication>());
        }

        [HttpGet("{loanApplicationId}")]
        [ProducesResponseType(typeof(IQueryable<LoanApplication>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetLoanApplication(long? loanApplicationId)
        {
            return Ok(new LoanApplication());
        }
    }
}
