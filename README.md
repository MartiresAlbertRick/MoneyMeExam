# Albert Rick Martires
##  Money Me Exam

## Tech
- Angular for Front-End
- .NET 5 for Back-End
- Entity Framework Core (ORM)
- SQL Server for Database

## Required Tools
VS Code - requirements for Angular and .NET development
SQL Server - requirements for Database
Postman/Curl - to test API
Install latest version of Node.JS

## Setting up SQL Database
Run sql database script 
```sh
MoneyMeExam\SQLScripts\MoneyMeExamDb.sql
```

## Setup configurations
Create a copy of the following configuration files
```sh
MoneyMeExam\ApiService\appsettings - Sample,json -> appsettings.json
MoneyMeExam\ApiService\appsettings.Development - Sample,json -> appsettings.Development.json
MoneyMeExam\ApiService\nlog - Sample,config -> nlog.config
```
Change database connection string configuration in `appsettings.Development.json`
```
"ConnectionStrings": {
    "DefaultConnection": "Server=[servername];Database=[dbname];Trusted_Connection=True;"
}
```

## Run the codes
In VSCode Terminal, change the directory to `MoneyMeExam\ApiService` then type the following command to build and run the application
```
dotnet run
```

## Setting up the Development Environment
In VSCode Terminal, change the directory `MoneyMeExam\ApiService` then type the following command to restore nuget packages
```
dotnet restore
```
Then go to `MoneyMeExam\ApiService\ClientApp` run the following command to install npm packages
```
npm install
```

## Build and Deployment
In VSCode Terminal, change the directory to `MoneyMeExam\ApiService\ClientApp` then type following command build the Angular app to production
```
ng build --prod
```
Then change the directory to `MoneyMeExam\ApiService` then type the following command to build and deploy the ApiService project along with the published Angular app
```
dotnet publish --configuration Release
```

## Usage
In order to test the loan application, should call first the following API
```
POST api/loans/third-party
body in application/json
{
    "loanAmount": 5000,
    "repaymentTerms": 24,
    "firstName": "Alrik",
    "lastName": "Martires",
    "title": "Mr.",
    "dateOfBirth": "1992-01-20T18:26:20.673Z",
    "mobile": "09567850348",
    "email": "martires.albert@outlook.com"
}
```
Once the POST is successful it will return a response with message and url
```
{
    "message": "Loan application successfully submitted",
    "redirectUrl": "https://localhost:5001/loan-application-step-1/34"
}
```
Open the URL in order to proceed on loan application

**Done!**
