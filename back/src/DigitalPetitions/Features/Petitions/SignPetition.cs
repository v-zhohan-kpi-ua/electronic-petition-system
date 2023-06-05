using DigitalPetitions.Domain.Petitions;
using DigitalPetitions.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalPetitions.Features.Petitions
{
    public class SignPetitionCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        private string email { get; set; } = null!;

        public required string Email
        {
            get { return email; }
            set
            {
                email = value.ToLower();
            }
        }

        public required bool IsResident { get; set; }
    }

    public class SignPetitionValidator : AbstractValidator<SignPetitionCommand>
    {
        public SignPetitionValidator()
        {
            RuleFor(value => value.Id)
                .GreaterThanOrEqualTo(1);

            RuleFor(value => value.FirstName)
                .NotEmpty();

            RuleFor(value => value.LastName)
                .NotEmpty();

            RuleFor(value => value.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(value => value.IsResident)
                .IsValidBoolean()
                .Equals(true);
        }
    }
    public class SignPetitionHandler : IRequestHandler<SignPetitionCommand, bool>
    {
        private readonly ApplicationDbContext _dbContext;

        public SignPetitionHandler(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Handle(SignPetitionCommand request, CancellationToken cancellationToken)
        {
            var petition = await _dbContext.Petitions
                .Include(p => p.Signs)
                .ThenInclude(s => s.Signee)
                .Where(p => p.Id == request.Id)
                .FirstAsync();

            if (petition == null)
                return false;

            if (petition.Status == PetitionStatus.Signing || petition.Status == PetitionStatus.WaitingForAnswer)
            {
                var signExist = petition.Signs
                    .Where(s => s.Signee.Email == request.Email).Count() != 0;

                if (signExist)
                    return false;

                Sign sign = new Sign
                {
                    Petition = petition,
                    Signee = new Signee
                    {
                        Email = request.Email,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                    },
                };

                if (petition.Signs.Count() + 1 >= Petition.MinimumSignsToGetAnswer)
                {
                    petition.Status = PetitionStatus.WaitingForAnswer;
                }

                await _dbContext.Signs.AddAsync(sign);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
