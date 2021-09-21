# GreenFlux Smart Charging

A RESTful API built with .NET Core 5, EF Core, and SQL Server 

## Prerequirements
* .NET 5 SDK
* Visual Studio 2019 or Visual Studio Code
* SQL Server Instance (you can use a Docker image for that)

## Project structure
1. `GreenFlux.API` - .NET Core 5 API 
2. `GreenFlux.Infrastructure` - Related to Data Access Layer, EF Core, Db Context, entities, migrations, and configuration files.
3. `GreenFlux.Domain` - Contains business rules and application logic, as well as domain models.
4. `GreenFlux.Presentation` - Responsible for providing presentation services and object-domain mapping
5. `GreenFlux.IntegrationTest` - Integration Testing
6. `GreenFlux.UnitTest` - Unit Testing

## Setup

Before running the API Project:
* Set up the SQL Server Instance
- Create the database and schema from the migration, using the following command line:
```Update-Database ```  (Visual Studio)
```dotnet ef database update  ```  (Visual Studio Code)

After starting the API Project:
* The API URL will be directly forwarded to the OpenAPI specification url built with Swagger, using as default ``` /swagger ``` after url application, so it can be tested more easily through the browser.

## Software Testing (Unit & Integration)

### The integration testing implemented with help of:
1. [XUnit](https://github.com/xunit/xunit) - xUnit.net is a free, open source, community-focused unit testing tool for the .NET Framework.
2. [TestServer](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1#aspnet-core-integration-tests) - the test web host and in-memory test server, are provided or managed by the [Microsoft.AspNetCore.Mvc.Testing](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing) package. Use of this package streamlines test creation and execution.
3. [FluentAssertions](https://fluentassertions.com/) - A very extensive set of extension methods that allow you to more naturally specify the expected outcome of a TDD or BDD-style unit tests.

### The Unit testing implemented with help of:
1. [XUnit](https://github.com/xunit/xunit)
2. [NSubstitute](https://nsubstitute.github.io/) - Designed as a friendly substitute for .NET mocking libraries.
