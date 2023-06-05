using AutoMapper;
using DigitalPetitions.Domain.AdminNS;
using DigitalPetitions.Domain.Petitions;
using DigitalPetitions.Features.Admin.Dto;

namespace DigitalPetitions.Features.Admin
{
    public class AdminProfile : Profile
    {
        public AdminProfile() 
        {
            CreateMap<ModerationResult, ModerationResultDto>();
            CreateMap<Answer, AnswerDto>();
        }
    }
}
