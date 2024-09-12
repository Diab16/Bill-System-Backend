using AutoMapper;
using Bill_system_API.DTOs;

namespace Bill_system_API.MappinigProfiles
{
    public class TypesProfile:Profile
    {
        public TypesProfile()
        {
            CreateMap<TypeDTO ,Type>().ReverseMap();

        }
    }
}
