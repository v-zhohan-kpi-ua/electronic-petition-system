using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DigitalPetitions.Domain
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public required string Email { get; set; }

        public required string PasswordHash { get; set; }

        public UserRole Role { get; set; } = UserRole.Regular;
    }
}

public enum UserRole
{
    Regular,
    Admin
}
