using System;

namespace GreenFlux.Infrastructure.Models
{
    public class Connector
    {
        public Guid Id { get; set; }
        public decimal MaxCurrent { get; set; }
        public Guid ChargeStationId { get; set; }
    }
}