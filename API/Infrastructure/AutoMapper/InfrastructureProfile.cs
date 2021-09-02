using API.Domain.Models;
using API.Infrastructure.Models;
using AutoMapper;

namespace API.Infrastructure.AutoMapper
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<ChargeStation, ChargeStationModel>().ReverseMap();
            CreateMap<Connector, ConnectorModel>().ReverseMap();
            CreateMap<Group, GroupModel>().ReverseMap();
        }
    }
}