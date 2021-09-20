using API.Infrastructure.Models;
using GreenFlux.API.DbContexts;
using System;

namespace IntegrationTesting
{
    public static class SeedData
    {
        public static void PopulateTestData(ApplicationDbContext dbContext)
        {
            dbContext.Groups.Add(new Group{ Id = new Guid("be3604d8-2410-4c06-96d5-db5a966e2a90"), Name = "IT Group", Capacity = 500 });
            dbContext.SaveChanges();
            dbContext.ChargeStations.Add(new ChargeStation{ Id = new Guid("5451119c-28af-41ba-a8f6-db43a4072edc"), Name = "IT Charge Station", GroupId = new Guid("be3604d8-2410-4c06-96d5-db5a966e2a90") });
            dbContext.SaveChanges();
            dbContext.Connectors.Add(new Connector{ Id = new Guid("0bc574a3-0184-4ecb-b3b9-81119af4aba7"), ChargeStationId = new Guid("5451119c-28af-41ba-a8f6-db43a4072edc"), MaxCurrent = 70 });
            dbContext.SaveChanges();
        }
    }
}