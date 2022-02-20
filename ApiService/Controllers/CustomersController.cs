using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyMeExam.Entities;
using MoneyMeExam.Repository;

namespace MoneyMeExam.ApiService.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : MoneyMeBaseController
    {
        public CustomersController(MoneyMeExamDbContext dbContext) : base(dbContext)
        { }

        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<Customer>), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetCustomers() => Get<Customer>(nameof(GetCustomers), DbContext.Customers.AsNoTracking());

        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult GetCustomer([FromRoute] long? customerId) => Get<Customer>(nameof(GetCustomers), DbContext.Customers.AsNoTracking(), customerId, (t => t.CustomerId == customerId));

        [HttpPost]
        [ProducesResponseType(typeof(Customer), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] Customer customer) => await CreateAsync<Customer>(nameof(CreateCustomerAsync), customer).ConfigureAwait(false);

        [HttpPut]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] Customer customer) => await UpdateAsync<Customer>(nameof(UpdateCustomerAsync), (await DbContext.Customers.AsNoTracking().FirstOrDefaultAsync(t => t.CustomerId == customer.CustomerId).ConfigureAwait(false) is null), customer).ConfigureAwait(false);
    }
}
