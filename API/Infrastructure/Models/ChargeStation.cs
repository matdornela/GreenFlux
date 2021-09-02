using System;
using System.Collections.Generic;

namespace API.Infrastructure.Models
{
    public class ChargeStation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Connector> Connectors { get; set; }
        public Guid GroupId { get; set; }
    }
}