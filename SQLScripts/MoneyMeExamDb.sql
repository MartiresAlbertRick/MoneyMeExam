DROP DATABASE [money_me_exam]
GO

IF NOT EXISTS(
	SELECT * FROM sys.databases 
	WHERE name = 'money_me_exam')
BEGIN
	CREATE DATABASE [money_me_exam]
END
GO

USE [money_me_exam]
GO

IF NOT EXISTS (
	SELECT 
		* FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[customer]') 
	AND type in (N'U'))
	IF OBJECT_ID('customer', 'T') IS NULL
		CREATE TABLE [dbo].[customer](
			[customer_id] BIGINT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
			[first_name] NVARCHAR(30) NOT NULL,
			[last_name] NVARCHAR(30) NOT NULL,
			[title] NVARCHAR(30) NULL,
			[date_of_birth] DATETIME NULL,
			[mobile] NVARCHAR(30) NOT NULL,
			[email] NVARCHAR(30) NOT NULL
		) ON [PRIMARY]
	GO	
GO

IF NOT EXISTS (
	SELECT 
		* FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[loan]') 
	AND type in (N'U'))
	IF OBJECT_ID('loan', 'T') IS NULL
		CREATE TABLE [dbo].[loan](
			[loan_id] BIGINT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
			[product_id] BIGINT NOT NULL,
			[customer_id] BIGINT NOT NULL,
			[loan_amount] DECIMAL(32, 2) NOT NULL DEFAULT(0),
			[interest_amount] DECIMAL(32, 2) NOT NULL DEFAULT(0),
			[establishment_fee] DECIMAL(32, 2) NOT NULL DEFAULT(0),
			[total_repayments] DECIMAL(32, 2) NOT NULL DEFAULT(0),
			[repayment_terms] INT NOT NULL DEFAULT(1),
			[repayment_frequency] INT NOT NULL,
			[loan_status] INT NOT NULL
		) ON [PRIMARY]
	GO	
GO

IF NOT EXISTS (
	SELECT 
		* FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[loan_detail]') 
	AND type in (N'U'))
	IF OBJECT_ID('loan_detail', 'T') IS NULL
		CREATE TABLE [dbo].[loan_detail](
			[loan_detail_id] BIGINT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
			[loan_id] BIGINT NOT NULL,
			[amount] DECIMAL(32, 2) NOT NULL,
			[due_date] DATETIME NOT NULL DEFAULT(GETDATE())
		) ON [PRIMARY]
	GO	
GO

IF NOT EXISTS (
	SELECT 
		* FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[product]') 
	AND type in (N'U'))
	IF OBJECT_ID('product', 'T') IS NULL
		CREATE TABLE [dbo].[product](
			[product_id] BIGINT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
			[product_name] NVARCHAR(50) NOT NULL,
			[product_type] INT NOT NULL,
			[interest_rate] DECIMAL(32, 8) NOT NULL,
			[establishment_fee] DECIMAL(32, 2) NOT NULL
		) ON [PRIMARY]
	GO	
GO

INSERT INTO product VALUES('ProductA', 1, 0.001, 300)
INSERT INTO product VALUES('ProductB', 0, 0.0000, 300)
INSERT INTO product VALUES('ProductC', 2, 0.0005, 300)
GO

IF NOT EXISTS (
	SELECT 
		* FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[mobile_number]') 
	AND type in (N'U'))
	IF OBJECT_ID('mobile_number', 'T') IS NULL
		CREATE TABLE [dbo].[mobile_number](
			[mobile_number_id] BIGINT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
			[number] NVARCHAR(30) NOT NULL,
			[is_black_listed] BIT NOT NULL DEFAULT(0)
		) ON [PRIMARY]
	GO	
GO

IF NOT EXISTS (
	SELECT 
		* FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[email_domain]') 
	AND type in (N'U'))
	IF OBJECT_ID('email_domain', 'T') IS NULL
		CREATE TABLE [dbo].[email_domain](
			[email_domain_id] BIGINT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
			[email_domain_name] NVARCHAR(30) NOT NULL,
			[is_black_listed] BIT NOT NULL DEFAULT(0)
		) ON [PRIMARY]
	GO	
GO