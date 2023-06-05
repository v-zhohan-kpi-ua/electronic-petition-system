using AutoMapper;
using DigitalPetitions.Domain.Petitions;
using DigitalPetitions.Features.Petitions.Dto;

namespace DigitalPetitions.Features.Petitions
{
    public class PetitionProfile : Profile 
    {
        public PetitionProfile() {
            CreateMap<Petition, PetitionDto>()
                .ForMember(d => d.SignsCount, options => options.MapFrom(petition => petition.Signs.Count))
                .ForMember(d => d.Answer, options => options.MapFrom(petition => petition.Answer.Body))
                .ForMember(d => d.ModerationResult, options => options.MapFrom(petition => petition.ModerationResult.Reason));

            CreateMap<Creator, CreatorDto>()
                .ForMember(d => d.Email,
                options =>
                {
                    options.PreCondition(bool (ResolutionContext res) =>
                    {
                       try
                        {
                            res.Items.TryGetValue("UserRole", out var userRole);

                            if (userRole != null)
                            {
                                var userRoleAsString = userRole.ToString();
                                if (userRoleAsString == "Admin")
                                    return true;
                            }
                        } catch (Exception ex)
                        {
                            return false;
                        }

                        return false;
                    });
                    options.MapFrom(c => c.Email);
                });
        }
    }
}
