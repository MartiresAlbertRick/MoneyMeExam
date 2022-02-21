using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyMeExam.Entities;
using MoneyMeExam.Repository;

namespace MoneyMeExam.ApiService.Controllers
{
    [ApiController]
    [Route("api/email-domains")]
    public class EmailDomainsController : MoneyMeBaseController
    {
        public EmailDomainsController(MoneyMeExamDbContext dbContext) : base(dbContext)
        { }

        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<EmailDomain>), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetEmailDomains() => Get<EmailDomain>(nameof(GetEmailDomains), DbContext.EmailDomains.AsNoTracking());

        [HttpGet("{emailDomainId}")]
        [ProducesResponseType(typeof(EmailDomain), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult GetEmailDomain([FromRoute] long? emailDomainId) => Get<EmailDomain>(nameof(GetEmailDomains), DbContext.EmailDomains.AsNoTracking(), emailDomainId, (t => t.EmailDomainId == emailDomainId));

        [HttpPost]
        [ProducesResponseType(typeof(EmailDomain), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateEmailDomainAsync([FromBody] EmailDomain emailDomain) => await CreateAsync<EmailDomain>(nameof(CreateEmailDomainAsync), emailDomain).ConfigureAwait(false);

        [HttpPut]
        [ProducesResponseType(typeof(EmailDomain), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateEmailDomainAsync([FromBody] EmailDomain emailDomain) => await UpdateAsync<EmailDomain>(nameof(UpdateEmailDomainAsync), (await DbContext.EmailDomains.AsNoTracking().FirstOrDefaultAsync(t => t.EmailDomainId == emailDomain.EmailDomainId).ConfigureAwait(false) is null), emailDomain).ConfigureAwait(false);
    }
}
