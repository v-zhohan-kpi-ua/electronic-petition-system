using AutoMapper;
using DigitalPetitions.Domain;
using DigitalPetitions.Features.Auth.Dto;

namespace DigitalPetitions.Features.Auth
{
    public class AuthProfile : Profile
    {
        public AuthProfile() 
        { 
            CreateMap<User, UserDto>();
        }
    }
}
