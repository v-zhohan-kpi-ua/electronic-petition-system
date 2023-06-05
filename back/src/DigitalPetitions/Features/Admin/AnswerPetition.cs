using AutoMapper;
using DigitalPetitions.Domain.Petitions;
using DigitalPetitions.Features.Admin.Dto;
using DigitalPetitions.Infrastructure.Persistence;
using FluentValidation;
using MediatR;

namespace DigitalPetitions.Features.Admin
{
    public class AnswerPetitionCommand : IRequest<AnswerDto?>
    {
        public int Id { get; set; }

        public required string Answer { get; set; }

        public int PublisherAdminId { get; set; }
    }

    public class AnswerPetitionValidator : AbstractValidator<AnswerPetitionCommand>
    {
        public AnswerPetitionValidator()
        {
            RuleFor(value => value.Id)
                .GreaterThanOrEqualTo(1);


            RuleFor(value => value.Answer)
                .NotEmpty();

            RuleFor(value => value.PublisherAdminId)
                .GreaterThanOrEqualTo(1);
        }
    }
    public class AnswerPetitionHandler : IRequestHandler<AnswerPetitionCommand, AnswerDto?>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AnswerPetitionHandler(ApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<AnswerDto?> Handle(AnswerPetitionCommand request, CancellationToken cancellationToken)
        {
            var petition = await _dbContext.Petitions
                .FindAsync(request.Id);

            if (petition == null)
                return null;

            if (petition.Status == PetitionStatus.WaitingForAnswer)
            {
                Answer answer = new Answer
                {
                    PetitionId = petition.Id,
                    Body = request.Answer,
                    PublishedById = request.PublisherAdminId
                };

                petition.Status = PetitionStatus.Answered;

                await _dbContext.Answers.AddAsync(answer);
                await _dbContext.SaveChangesAsync();

                return _mapper.Map<AnswerDto>(answer);
            }

            return null;
        }
    }
}
