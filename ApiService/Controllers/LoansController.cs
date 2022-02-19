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
    [Route("api/loans")]
    public class LoansController : ControllerBase
    {
        public LoansController()
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<Loan>), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetLoans()
        {
            return Ok(new List<Loan>());
        }

        [HttpGet("{loanId}")]
        [ProducesResponseType(typeof(Loan), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetLoan(long? loanId)
        {
            return Ok(new Loan());
        }
    }
}
