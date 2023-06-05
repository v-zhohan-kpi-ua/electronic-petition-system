using BC = BCrypt.Net.BCrypt;
using DigitalPetitions.Features.Auth.Dto;
using DigitalPetitions.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace DigitalPetitions.Features.Auth
{
    public class LoginCommand : IRequest<UserDto?>
    {
        public required string Email { get; set; }

        public required string Password { get; set; }
    }

    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(value => value.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(value => value.Password)
                .NotEmpty();
        }
    }

    public class LoginHandler : IRequestHandler<LoginCommand, UserDto?>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public LoginHandler(ApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<UserDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Where(u => u.Email == request.Email)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                var isPasswordValid = BC.Verify(request.Password, user.PasswordHash);

                if (isPasswordValid)
                {
                    return _mapper.Map<UserDto>(user);
                }
            }

            return null;
        }
    }
}
