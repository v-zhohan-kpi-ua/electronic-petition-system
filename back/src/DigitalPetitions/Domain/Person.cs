using System.ComponentModel.DataAnnotations;

namespace DigitalPetitions.Domain
{
    public class Person
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public required string FirstName { get; set; }

        [MaxLength(255)]
        public required string LastName { get; set; }

        [MaxLength(255)]
        public required string Email { get; set; }
    }
}
