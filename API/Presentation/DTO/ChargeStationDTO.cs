using System;
using System.Collections.Generic;

namespace API.Presentation.DTO
{
    public class ChargeStationDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ConnectorDTO> Connectors { get; set; }
        public Guid GroupId { get; set; }
    }
}