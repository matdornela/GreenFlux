using System;
using System.Collections.Generic;

namespace API.Presentation.DTO
{
    public class GroupDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Capacity { get; set; }
        public ICollection<ChargeStationDTO> ChargeStations { get; set; }
    }
}