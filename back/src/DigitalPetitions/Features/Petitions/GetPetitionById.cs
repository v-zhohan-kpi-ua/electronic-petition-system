using AutoMapper;
using Azure;
using DigitalPetitions.Domain;
using DigitalPetitions.Domain.Petitions;
using DigitalPetitions.Features.Petitions.Dto;
using DigitalPetitions.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace DigitalPetitions.Features.Petitions
{

    public class GetPetitionByIdQuery : IRequest<PetitionDto?>
    {
        public required int Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string Role { get; set; } = string.Empty;
    }

    public class GetPetitionByIdValidator : AbstractValidator<GetPetitionByIdQuery>
    {
        public GetPetitionByIdValidator()
        {
            RuleFor(v => v.Id)
                .GreaterThanOrEqualTo(1);
        }
    }

    internal sealed class GetPetitionByIdHandler : IRequestHandler<GetPetitionByIdQuery, PetitionDto?>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPetitionByIdHandler(ApplicationDbContext context, IMapper mapper) 
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<PetitionDto?> Handle(GetPetitionByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _dbContext.Petitions
                .Include(p => p.Signs)
                .Include(p => p.Answer)
                .Include(p => p.Creator)
                .Include(p => p.ModerationResult)
                .Where(p => p.Id == request.Id)
            .FirstOrDefaultAsync();

            return _mapper.Map<PetitionDto>(
                    response,
                    options => options.Items["UserRole"] = request.Role);
        }
    }
}
