using AutoMapper;
using Bill_system_API.DTOs;
using Bill_system_API.Models;

namespace Bill_system_API.MappinigProfiles
{
    public class ItemProfile: Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDto>()
            .ForMember(dest => dest.Company, opt => opt.Ignore()) // Ignore manual mapping for Company
            .ForMember(dest => dest.Unit, opt => opt.Ignore())   // Ignore manual mapping for Unit
            .ForMember(dest => dest.Type, opt => opt.Ignore());

            CreateMap<ItemDto, Item>()
                .ForMember(dest => dest.Company, opt => opt.Ignore()) // Ignore manual mapping for Company
                .ForMember(dest => dest.Unit, opt => opt.Ignore())   // Ignore manual mapping for Unit
                .ForMember(dest => dest.Type, opt => opt.Ignore());
        }
    }
}
