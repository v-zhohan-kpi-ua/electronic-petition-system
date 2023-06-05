using AutoMapper;
using DigitalPetitions.Domain.AdminNS;
using DigitalPetitions.Features.Admin.Dto;
using DigitalPetitions.Infrastructure.Persistence;
using FluentValidation;
using MediatR;

namespace DigitalPetitions.Features.Admin
{
    public class ModeratePetitionCommand : IRequest<ModerationResultDto?>
    {
        public int Id { get; set; }

        public string? Reason { get; set; }

        public int ModeratorId { get; set; }

        public ModerationStatus Status { get; set; }
    }

    public class ModeratePetitionValidator : AbstractValidator<ModeratePetitionCommand>
    {
        public ModeratePetitionValidator()
        {
            RuleFor(value => value.Id)
                .GreaterThanOrEqualTo(1);

            RuleFor(value => value.Reason)
                .NotEmpty()
                .When(value => value.Status == ModerationStatus.Declined);

            RuleFor(value => value.ModeratorId)
                .GreaterThanOrEqualTo(1);

            RuleFor(value => value.Status)
                .NotNull();
        }
    }
    public class ModeratePetitionHandler : IRequestHandler<ModeratePetitionCommand, ModerationResultDto?>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ModeratePetitionHandler(ApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<ModerationResultDto?> Handle(ModeratePetitionCommand request, CancellationToken cancellationToken)
        {
            var petition = await _dbContext.Petitions
                .FindAsync(request.Id);

            if (petition == null)
                return null;

            if (petition.Status == PetitionStatus.Created)
            {
                ModerationResult moderationResult = new ModerationResult
                {
                    ModeratorId = request.ModeratorId,
                    Petition = petition,
                    Reason = request.Reason,
                    Status = request.Status
                };

                switch (moderationResult.Status)
                {
                    case ModerationStatus.Accepted:
                        petition.Status = PetitionStatus.Signing;
                        break;
                    case ModerationStatus.Declined:
                        petition.Status = PetitionStatus.Declined;
                        break;
                }

                await _dbContext.ModerationResults.AddAsync(moderationResult);
                await _dbContext.SaveChangesAsync();

                petition.ModerationResult = moderationResult;
                await _dbContext.SaveChangesAsync();

                return _mapper.Map<ModerationResultDto>(moderationResult);
            }

            return null;
        }
    }
}
