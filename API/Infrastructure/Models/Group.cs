using System;
using System.Collections.Generic;

namespace API.Infrastructure.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Capacity { get; set; }
        public ICollection<ChargeStation> ChargeStations { get; set; }
    }
}