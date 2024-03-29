﻿using GreenFlux.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Infrastructure.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ChargeStation> ChargeStations { get; set; }
        public DbSet<Connector> Connectors { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}