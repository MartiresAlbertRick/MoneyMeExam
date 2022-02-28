using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using MoneyMeExam.Repository;
using Newtonsoft.Json;
using NLog;

namespace MoneyMeExam.ApiService.Controllers
{
    public class MoneyMeBaseController : ControllerBase
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        protected internal readonly MoneyMeExamDbContext DbContext;
        
        public MoneyMeBaseController(MoneyMeExamDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [NonAction]
        public IActionResult ConvertExceptionToHttpStatusCode(Exception exception, string methodName)
        {
            if (exception == null)
            {
                return StatusCode(500, new { message = "Caught an exception but the exception argument is null" });
            }
            string errorMessage = Startup.IsDevelopment ? exception.ToString() : exception.Message;
            Logger.Error(errorMessage);
            return StatusCode(500, new { message = errorMessage });
        }

        [NonAction]
        public IActionResult Get<T>(string methodName, IQueryable<T> dataSet)
        {
            Logger.Info(() => $"Received request to get data at {methodName}");
            try
            {
                return Ok(dataSet);
            }
            catch (Exception e)
            {
                return ConvertExceptionToHttpStatusCode(e, methodName);
            }
        }

        [NonAction]
        public IActionResult Get<T>(string methodName, IQueryable<T> dataSet, long? referenceId, Func<T, bool> predicate = null)
        {
            Logger.Info(() => $"Received request to get data at {methodName} with referenceId {referenceId}");
            try
            {
                if (referenceId is null || referenceId < 1) 
                {
                    return BadRequest(new { message = $"Invalid reference id for {methodName}" });
                }
                T data = dataSet.FirstOrDefault(predicate ?? (s => true));
                if (data is null)
                {
                    return NotFound(new { message = $"No data found while getting data from {methodName}" });
                }
                return Ok(data);
            }
            catch (Exception e)
            {
                return ConvertExceptionToHttpStatusCode(e, methodName);
            }
        }

        [NonAction]
        public async Task<IActionResult> CreateAsync<T>(string methodName, T data)
        {
            Logger.Info(() => $"Received request to create data at {methodName} with payload: {JsonConvert.SerializeObject(data)}");
            try
            {
                if (data is null) 
                {
                    return BadRequest(new { message = $"Invalid payload" });
                }
                await DbContext.AddAsync(data).ConfigureAwait(false);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                return StatusCode(201, data);
            }
            catch (Exception e)
            {
                return ConvertExceptionToHttpStatusCode(e, methodName);
            }
        }

        [NonAction]
        public async Task<IActionResult> UpdateAsync<T>(string methodName, bool isDataNotExist, T data, Func<T, Task> callbackDelegate = null)
        {
            Logger.Info(() => $"Received request to update data at {methodName} with payload: {JsonConvert.SerializeObject(data)}");
            IDbContextTransaction transaction = BeginTransaction();
            try
            {
                if (data is null) 
                {
                    return BadRequest(new { message = $"Invalid payload" });
                }
                if (isDataNotExist) 
                {
                    return NotFound(new { message = $"Could not update the data from {methodName}" });
                }
                if (!(callbackDelegate is null))
                {
                    await callbackDelegate(data).ConfigureAwait(false);
                }
                DbContext.Update(data);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);
                return Ok(data);
            }
            catch (Exception e)
            {
                await RollbackTransactionAsync(transaction).ConfigureAwait(false);
                return ConvertExceptionToHttpStatusCode(e, methodName);
            }
        }

        [NonAction]
        public IDbContextTransaction BeginTransaction() => DbContext.Database.BeginTransaction();

        [NonAction]
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction is null)
                throw new ArgumentNullException(nameof(transaction));
            await transaction.CommitAsync();
        }

        [NonAction]
        public async Task RollbackTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction is null)
                throw new ArgumentNullException(nameof(transaction));
            await transaction.RollbackAsync();
        }
    }
}
