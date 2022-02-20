using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyMeExam.Entities;
using MoneyMeExam.Repository;

namespace MoneyMeExam.ApiService.Controllers
{
    [ApiController]
    [Route("api/mobile-numbers")]
    public class MobileNumbersController : MoneyMeBaseController
    {
        public MobileNumbersController(MoneyMeExamDbContext dbContext) : base(dbContext)
        { }

        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<MobileNumber>), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetMobileNumbers() => Get<MobileNumber>(nameof(GetMobileNumbers), DbContext.MobileNumbers.AsNoTracking());

        [HttpGet("{mobileNumberId}")]
        [ProducesResponseType(typeof(MobileNumber), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult GetMobileNumber([FromRoute] long? mobileNumberId) => Get<MobileNumber>(nameof(GetMobileNumbers), DbContext.MobileNumbers.AsNoTracking(), mobileNumberId, (t => t.MobileNumberId == mobileNumberId));

        [HttpPost]
        [ProducesResponseType(typeof(MobileNumber), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateMobileNumberAsync([FromBody] MobileNumber mobileNumber) => await CreateAsync<MobileNumber>(nameof(CreateMobileNumberAsync), mobileNumber).ConfigureAwait(false);

        [HttpPut]
        [ProducesResponseType(typeof(MobileNumber), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateMobileNumberAsync([FromBody] MobileNumber mobileNumber) => await UpdateAsync<MobileNumber>(nameof(UpdateMobileNumberAsync), (await DbContext.MobileNumbers.AsNoTracking().FirstOrDefaultAsync(t => t.MobileNumberId == mobileNumber.MobileNumberId).ConfigureAwait(false) is null), mobileNumber).ConfigureAwait(false);
    }
}
