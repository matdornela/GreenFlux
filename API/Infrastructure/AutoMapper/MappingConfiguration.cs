using AutoMapper;

namespace API.Infrastructure.AutoMapper
{
    public static class MappingConfiguration
    {
        public static MapperConfiguration RegisterMaps()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PresentationProfile());
                cfg.AddProfile(new InfrastructureProfile());
            });

            return config;
        }
    }
}