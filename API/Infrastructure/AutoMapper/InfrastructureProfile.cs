using API.Domain.Models;
using API.Infrastructure.Models;
using AutoMapper;

namespace API.Infrastructure.AutoMapper
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