using AutoMapper;
using Bill_system_API.DTOs;
using Bill_system_API.Models;

namespace Bill_system_API.MappinigProfiles
{
    public class ClientProfile:Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientDTO, Client>().ReverseMap();
        }
    }
}
