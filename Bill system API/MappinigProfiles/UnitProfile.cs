using AutoMapper;
using Bill_system_API.DTOs;
using Bill_system_API.Models;

namespace Bill_system_API.MappinigProfiles
{
    public class UnitProfile:Profile
    {
        public UnitProfile()
        {
            CreateMap<UnitDTO,Unit>().ReverseMap();
        }
    }
}
