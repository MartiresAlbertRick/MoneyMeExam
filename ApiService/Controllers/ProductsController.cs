using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyMeExam.Entities;
using MoneyMeExam.Repository;

namespace MoneyMeExam.ApiService.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : MoneyMeBaseController
    {
        public ProductsController(MoneyMeExamDbContext dbContext) : base(dbContext)
        { }

        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<Product>), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetProducts() => Get<Product>(nameof(GetProducts), DbContext.Products.AsNoTracking());

        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult GetProduct([FromRoute] long? productId) => Get<Product>(nameof(GetProducts), DbContext.Products.AsNoTracking(), productId, (t => t.ProductId == productId));

        [HttpPost]
        [ProducesResponseType(typeof(Product), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateProductAsync([FromBody] Product product) => await CreateAsync<Product>(nameof(CreateProductAsync), product).ConfigureAwait(false);

        [HttpPut]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateProductAsync([FromBody] Product product) => await UpdateAsync<Product>(nameof(UpdateProductAsync), (await DbContext.Products.AsNoTracking().FirstOrDefaultAsync(t => t.ProductId == product.ProductId).ConfigureAwait(false) is null), product).ConfigureAwait(false);
    }
}
