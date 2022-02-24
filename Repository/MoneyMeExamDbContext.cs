using MoneyMeExam.Entities;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace MoneyMeExam.Repository
{
    public class MoneyMeExamDbContext : DbContext
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public MoneyMeExamDbContext(DbContextOptions<MoneyMeExamDbContext> options) : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<EmailDomain> EmailDomains { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanDetail> LoanDetails { get; set; }
        public DbSet<MobileNumber> MobileNumbers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Logger.Info("Started configuring entities");
            ConfigureCustomer(builder);
            ConfigureLoan(builder);
            ConfigureLoanDetails(builder);
            ConfigureProduct(builder);
            ConfigureMobileNumber(builder);
            ConfigureEmailDomain(builder);
            Logger.Info("Completed configuring entities");
        }

        public static void ConfigureCustomer(ModelBuilder builder) 
        {
            builder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");
                entity.HasKey(e => new { e.CustomerId });
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
                entity.Property(e => e.Mobile).HasColumnName("mobile");
                entity.Property(e => e.Email).HasColumnName("email");
            });
        }

        public static void ConfigureLoan(ModelBuilder builder) 
        {
            builder.Entity<Loan>(entity =>
            {
                entity.ToTable("loan");
                entity.HasKey(e => new { e.LoanId });
                entity.Property(e => e.LoanId).HasColumnName("loan_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.LoanAmount).HasColumnName("loan_amount").HasColumnType("DECIMAL(32, 2)");
                entity.Property(e => e.RepaymentTerms).HasColumnName("repayment_terms");
                entity.Property(e => e.InterestAmount).HasColumnName("interest_amount").HasColumnType("DECIMAL(32, 2)");
                entity.Property(e => e.EstablishmentFee).HasColumnName("establishment_fee").HasColumnType("DECIMAL(32, 2)");
                entity.Property(e => e.TotalRepayments).HasColumnName("total_repayments").HasColumnType("DECIMAL(32, 2)");
                entity.Property(e => e.RepaymentFrequency).HasColumnName("repayment_frequency");
                entity.Property(e => e.LoanStatus).HasColumnName("loan_status");
            });
        }

        public static void ConfigureLoanDetails(ModelBuilder builder) 
        {
            builder.Entity<LoanDetail>(entity =>
            {
                entity.ToTable("loan_detail");
                entity.HasKey(e => new { e.LoanDetailId });
                entity.Property(e => e.LoanDetailId).HasColumnName("loan_detail_id");
                entity.Property(e => e.LoanId).HasColumnName("loan_id");
                entity.Property(e => e.Amount).HasColumnName("amount").HasColumnType("DECIMAL(32, 2)");
                entity.Property(e => e.DueDate).HasColumnName("due_date");
            });
        }

        public static void ConfigureProduct(ModelBuilder builder) 
        {
            builder.Entity<Product>(entity =>
            {
                entity.ToTable("product");
                entity.HasKey(e => new { e.ProductId });
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.ProductName).HasColumnName("product_name");
                entity.Property(e => e.ProductType).HasColumnName("product_type");
                entity.Property(e => e.InterestRate).HasColumnName("interest_rate").HasColumnType("DECIMAL(32, 2)");
                entity.Property(e => e.EstablishmentFee).HasColumnName("establishment_fee").HasColumnType("DECIMAL(32, 2)");
            });
        }

        public static void ConfigureMobileNumber(ModelBuilder builder) 
        {
            builder.Entity<MobileNumber>(entity =>
            {
                entity.ToTable("mobile_number");
                entity.HasKey(e => new { e.MobileNumberId });
                entity.Property(e => e.MobileNumberId).HasColumnName("mobile_number_id");
                entity.Property(e => e.Number).HasColumnName("number");
                entity.Property(e => e.IsBlackListed).HasColumnName("is_black_listed");
            });
        }

        public static void ConfigureEmailDomain(ModelBuilder builder) 
        {
            builder.Entity<EmailDomain>(entity =>
            {
                entity.ToTable("email_domain");
                entity.HasKey(e => new { e.EmailDomainId });
                entity.Property(e => e.EmailDomainId).HasColumnName("email_domain_id");
                entity.Property(e => e.EmailDomainName).HasColumnName("email_domain_name");
                entity.Property(e => e.IsBlackListed).HasColumnName("is_black_listed");
            });
        }
    }
}