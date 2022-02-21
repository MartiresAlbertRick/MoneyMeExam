using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyMeExam.Entities;
using MoneyMeExam.Repository;

namespace MoneyMeExam.ApiService.Controllers
{
    [ApiController]
    [Route("api/common")]
    public class CommonController : MoneyMeBaseController
    {
        public CommonController(MoneyMeExamDbContext dbContext) : base(dbContext)
        { }

        [HttpGet("loan-statuses")]
        [ProducesResponseType(typeof(List<IDictionary<string, object>>), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetLoanStatuses() => WriteEnumAsListDictionary<Entities.Enums.LoanStatus>();

        [HttpGet("product-types")]
        [ProducesResponseType(typeof(List<IDictionary<string, object>>), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetProductTypes() => WriteEnumAsListDictionary<Entities.Enums.ProductType>();

        [HttpGet("repayment-frequencies")]
        [ProducesResponseType(typeof(List<IDictionary<string, object>>), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetRepaymentFrequencies() => WriteEnumAsListDictionary<Entities.Enums.RepaymentFrequency>();

        [NonAction]
        public IActionResult WriteEnumAsListDictionary<T>()
        {
            var data = new List<IDictionary<string, object>>();
            foreach(T item in System.Enum.GetValues(typeof(T)))
            {
                var dictionary = new Dictionary<string, object>();
                dictionary.Add("id", item);
                dictionary.Add("description", item.ToString());
                data.Add(dictionary);
            }
            return Ok(data);
        }
    }
}
