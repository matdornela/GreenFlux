using API.Domain.Models;
using API.Presentation.DTO;
using AutoMapper;

namespace API.Infrastructure.AutoMapper
{
    public class PresentationProfile : Profile

    {
        public PresentationProfile()
        {
            CreateMap<ConnectorDTO, ConnectorModel>().ReverseMap();
            CreateMap<ChargeStationDTO, ChargeStationModel>().ReverseMap();
            CreateMap<GroupDTO, GroupModel>().ReverseMap();
        }
    }
}