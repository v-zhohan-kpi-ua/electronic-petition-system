using System.Text.Json.Serialization;

namespace DigitalPetitions.Features.Petitions.Dto
{
    public class CreatorDto
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set;}

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Email { get; set;}
    }
}
