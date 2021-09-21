using System;
using GreenFlux.Infrastructure.DbContexts;
using GreenFlux.Infrastructure.Models;

namespace GreenFlux.IntegrationTest
{
    public static class SeedData
    {
        public static void PopulateTestData(ApplicationDbContext dbContext)
        {
            dbContext.Groups.Add(new Group { Id = new Guid("a0a66824-5513-421b-aec2-e1f84c0096a4"), Name = "IT Group", Capacity = 500 });
            dbContext.ChargeStations.Add(new ChargeStation { Id = new Guid("1a466d73-1df5-4027-8dce-6bdde4e98304"), Name = "IT Charge Station", GroupId = new Guid("a0a66824-5513-421b-aec2-e1f84c0096a4") });
            dbContext.Connectors.Add(new Connector { Id = new Guid("3fc00054-05d1-4721-af42-432210978aa9"), ChargeStationId = new Guid("1a466d73-1df5-4027-8dce-6bdde4e98304"), MaxCurrent = 70 });
            dbContext.SaveChanges();
        }
    }
}