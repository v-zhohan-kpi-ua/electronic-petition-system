using System.Text.Json.Serialization;

namespace DigitalPetitions.Features.Admin.Dto
{
    public class ModerationResultDto
    {
        public int Id { get; set; }

        public int PetitionId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Reason { get; set; }

        public required ModerationStatus Status { get; set; }

        public int ModeratorId { get; set; }

        public required DateTime CreatedAt { get; set; }
    }
}
