using AutoMapper;
using DeFiPulse.Project;
namespace DeFiPulse
{
    public class DeFiPulseApplicationAutoMapperProfile : Profile
    {
        public DeFiPulseApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
      
            CreateMap<SendTokenInfo, SendTokenInfoDto>();
            CreateMap<SendTokenInfoDto, SendTokenInfo>();
        }
    }
}
