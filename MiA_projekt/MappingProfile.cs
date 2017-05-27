using AutoMapper;
using MiA_projekt.Dto;
using MiA_projekt.Models;

namespace MiA_projekt
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, AppUserDto>();

            CreateMap<AppUserDto, AppUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Address, AddressDto>();

            CreateMap<AddressDto, Address>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}