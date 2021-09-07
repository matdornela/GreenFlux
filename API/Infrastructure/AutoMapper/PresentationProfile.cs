using API.Domain.Models;
using API.Presentation.DTO;
using AutoMapper;

namespace API.Infrastructure.AutoMapper
{
    public class PresentationProfile : Profile

    {
        public PresentationProfile()
        {
            CreateMap<ChargeStationDTO, ChargeStationModel>().ReverseMap();
            CreateMap<ConnectorDTO, ConnectorModel>().ReverseMap();
            CreateMap<GroupDTO, GroupModel>();
            CreateMap<GroupModel, GroupDTO>();
        }
    }
}