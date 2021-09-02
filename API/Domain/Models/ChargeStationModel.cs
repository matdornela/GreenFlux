using System;
using System.Collections.Generic;

namespace API.Domain.Models
{
    public class ChargeStationModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ConnectorModel> Connectors { get; set; }
        public Guid GroupId { get; set; }
    }
}