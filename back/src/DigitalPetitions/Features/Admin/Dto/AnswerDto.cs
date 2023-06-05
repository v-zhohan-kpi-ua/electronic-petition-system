namespace DigitalPetitions.Features.Admin.Dto
{
    public class AnswerDto
    {
        public int Id { get; set; }

        public required string Body { get; set; }

        public int PetitionId { get; set; }

        public int PublishedById { get; set; }

        public required DateTime CreatedAt { get; set; }
    }
}
