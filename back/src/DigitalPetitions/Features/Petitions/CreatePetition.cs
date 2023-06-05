using AutoMapper;
using DigitalPetitions.Domain.Petitions;
using DigitalPetitions.Features.Petitions.Dto;
using DigitalPetitions.Infrastructure.Persistence;
using FluentValidation;
using MediatR;

namespace DigitalPetitions.Features.Petitions
{
    public class CreatePetitionCommand : IRequest<PetitionDto>
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required string Title { get; set; }

        public required string Body { get; set; }

        public required bool IsResident { get; set; }
    }

    public class CreatePetitionValidator : AbstractValidator<CreatePetitionCommand>
    {
        public CreatePetitionValidator()
        {
            RuleFor(value => value.FirstName)
                .NotEmpty();

            RuleFor(value => value.LastName)
                .NotEmpty();

            RuleFor(value => value.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(value => value.Title)
                .NotEmpty();

            RuleFor(value => value.Body)
                .NotEmpty();

            RuleFor(value => value.IsResident)
                .IsValidBoolean()
                .Equals(true);
        }
    }

    public class CreatePetitionHandler : IRequestHandler<CreatePetitionCommand, PetitionDto>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreatePetitionHandler(ApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<PetitionDto> Handle(CreatePetitionCommand request, CancellationToken cancellationToken)
        {
            var petition = new Petition
            {
                Title = request.Title,
                Body = request.Body,
                Creator = new Creator
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                },
            };

            await _dbContext.Petitions.AddAsync(petition);
            await _dbContext.SaveChangesAsync();

            Sign ownSign = new Sign
            {
                Petition = petition,
                Signee = new Signee
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                },
            };

            await _dbContext.Signs.AddAsync(ownSign);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PetitionDto>(petition);
        }
    }
}
