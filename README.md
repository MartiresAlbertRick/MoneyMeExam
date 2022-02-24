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

##Setting up the Development Environment
In VSCode Terminal Go to `MoneyMeExam\ApiService` run the following command to restore nuget packages
```
dotnet restore
```
Then go to `MoneyMeExam\ApiService\ClientApp` run the following command to install npm packages
```
npm install
```

##Build and Deployment
In VSCode Terminal Go to `MoneyMeExam\ApiService\ClientApp` run the following command build the Angular app to production
```
ng build --prod
```
Then go to `MoneyMeExam\ApiService` run the following command to build and deploy the ApiService project along with the built Angular app
```
dotnet publish --configuration Release
```

**Done!**
