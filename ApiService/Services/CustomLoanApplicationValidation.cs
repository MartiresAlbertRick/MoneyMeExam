using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using NLog;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace MoneyMeExam.ApiService.Services
{
    public class CustomLoanApplicationValidation : ActionFilterAttribute
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly JsonSerializerSettings jsonSerializerSettings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public CustomLoanApplicationValidation()
        { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try 
            {
                bool validateLoanApplication = context.HttpContext.Request.Headers["x-validate-appliation"] == "true";
                if (validateLoanApplication)
                {
                    using var reader = new StreamReader(context.HttpContext.Request.Body);
                    string content = reader.ReadToEndAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    Entities.Loan loan = JsonConvert.DeserializeObject<Entities.Loan>(content);
                    var result = context.Result as ObjectResult;
                    var dbContext = context.HttpContext.RequestServices.GetService(typeof(Repository.MoneyMeExamDbContext)) as Repository.MoneyMeExamDbContext;
                    Entities.Customer customer = dbContext.Customers.AsNoTracking().FirstOrDefaultAsync(t => t.CustomerId == loan.CustomerId).ConfigureAwait(false).GetAwaiter().GetResult();
                    var mail = new MailAddress(customer.Email);
                    if (dbContext.EmailDomains.AsNoTracking().Where(t => t.EmailDomainName == mail.Host && t.IsBlackListed == true).Count() >= 1)
                    {
                        context.HttpContext.Response.StatusCode = 400;
                        string responseContent = JsonConvert.SerializeObject(new { message = "Email domain is blacklisted" });
                        context.HttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(responseContent));
                    }
                    if (dbContext.MobileNumbers.AsNoTracking().Where(t => t.Number == customer.Mobile && t.IsBlackListed == true).Count() >= 1)
                    {
                        context.HttpContext.Response.StatusCode = 400;
                        string responseContent = JsonConvert.SerializeObject(new { message = "Number is blacklisted" });
                        context.HttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(responseContent));
                    }
                    int age =  DateTime.Today.Year - customer.DateOfBirth.GetValueOrDefault().Year;
                    if (customer.DateOfBirth.GetValueOrDefault().Date > DateTime.Today.AddYears(-age)) age--;
                    if (age <= 18)
                    {
                        context.HttpContext.Response.StatusCode = 400;
                        string responseContent = JsonConvert.SerializeObject(new { message = "Age is below 18 years old" });
                        context.HttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(responseContent));
                    }
                }
                base.OnActionExecuting(context);
            }
            catch (Exception e)
            {
                Logger.Error(() => e.ToString());
            }
        }
    }
}