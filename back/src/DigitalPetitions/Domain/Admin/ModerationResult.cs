using DigitalPetitions.Domain.Petitions;

namespace DigitalPetitions.Domain.AdminNS
{
    public class ModerationResult
    {
        public int Id { get; set; }

        public int PetitionId { get; set; }

        public Petition? Petition { get; set; }

        public string? Reason { get; set; }

        public required ModerationStatus Status { get; set; }

        public int ModeratorId { get; set; }

        public Admin? Moderator { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

public enum ModerationStatus
{
    Declined = 0,
    Accepted = 1
}