using DigitalPetitions.Domain;

namespace DigitalPetitions.Features.Auth.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public required string Email { get; set; }

        public required UserRole Role { get; set; }
    }
}
