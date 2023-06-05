using DigitalPetitions.Domain.Petitions;
using System.Text.Json.Serialization;

namespace DigitalPetitions.Features.Petitions.Dto
{
    public class PetitionDto
    {
        public int Id { get; set; }
        
        public required string Title { get; set; }

        public required string Body { get; set; }

        public required string Status { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? StatusDeadline { get; set; }

        public required CreatorDto Creator { get; set; }

        public int SignsCount { get; set; }

        public int SignsRequiredToGetAnswer { get; set; } = Petition.MinimumSignsToGetAnswer;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Answer { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ModerationResult { get; set; }

        public required DateTime CreatedAt { get; set; }
    }
}
