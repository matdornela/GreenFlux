using AutoMapper;
using GreenFlux.Domain.Models;
using GreenFlux.Infrastructure.Models;

namespace GreenFlux.Infrastructure.AutoMapper
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<Connector, ConnectorModel>().ReverseMap();
            CreateMap<ChargeStation, ChargeStationModel>().ReverseMap();
            CreateMap<Group, GroupModel>().ReverseMap();
        }
    }
}