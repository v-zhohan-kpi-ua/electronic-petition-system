using DigitalPetitions.Domain.AdminNS;

namespace DigitalPetitions.Domain.Petitions
{
    public class Answer
    {
        public int Id { get; set; }

        public required string Body { get; set; }

        public int PetitionId { get; set; }

        public Petition? Petition { get; set; }

        public int PublishedById { get; set; }

        public Admin? PublishedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
