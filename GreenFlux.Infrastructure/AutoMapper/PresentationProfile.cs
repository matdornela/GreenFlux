using AutoMapper;
using GreenFlux.Domain.Models;
using GreenFlux.Presentation.DTO;

namespace GreenFlux.Infrastructure.AutoMapper
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