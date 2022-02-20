using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyMeExam.Entities;
using MoneyMeExam.Repository;
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
                return StatusCode(500, "Caught an exception but the exception argument is null");
            }
            string errorMessage = Startup.IsDevelopment ? exception.ToString() : exception.Message;
            Logger.Error(errorMessage);
            return StatusCode(500, errorMessage);
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
                    return BadRequest($"Invalid reference id for {methodName}");
                }
                T data = dataSet.FirstOrDefault(predicate ?? (s => true));
                if (data is null)
                {
                    return NotFound($"No data found while getting data from {methodName}");
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
            Logger.Info(() => $"Received request to create data at {methodName} with payload{data.ToString()}");
            try
            {
                if (data is null) 
                {
                    return BadRequest($"Invalid payload");
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
        public async Task<IActionResult> UpdateAsync<T>(string methodName, bool isDataNull, T data)
        {
            Logger.Info(() => $"Received request to update data at {methodName} with payload{data.ToString()}");
            try
            {
                if (data is null) 
                {
                    return BadRequest($"Invalid payload");
                }
                if (isDataNull) 
                {
                    return NotFound($"Could not update the data {Type.GetType(nameof(T))}");
                }
                DbContext.Update(data);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                return Ok(data);
            }
            catch (Exception e)
            {
                return ConvertExceptionToHttpStatusCode(e, methodName);
            }
        }
    }
}
