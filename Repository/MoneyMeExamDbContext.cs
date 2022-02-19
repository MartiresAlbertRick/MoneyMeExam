using MoneyMeExam.Entities;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMeExam.Repository
{
    public class MoneyMeExamDbContext : DbContext
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public MoneyMeExamDbContext(DbContextOptions<MoneyMeExamDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Logger.Info("Started configuring entities");
            
            Logger.Info("Completed configuring entities");
        }
    }
}