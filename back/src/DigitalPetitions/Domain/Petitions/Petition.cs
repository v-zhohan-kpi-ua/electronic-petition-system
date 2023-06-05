using DigitalPetitions.Domain.AdminNS;

namespace DigitalPetitions.Domain.Petitions
{
    public class Petition
    {
        public const int DeadlineToGetSignsInDays = 90;

        public const int MinimumSignsToGetAnswer = 3;

        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Body { get; set; }

        public Creator? Creator { get; set; }

        private PetitionStatus status { get; set; } = PetitionStatus.Created;

        public PetitionStatus Status {
            get { return status; }
            set {
                status = value;

                StatusChangedAt = DateTime.UtcNow;

                switch (value)
                {
                    case PetitionStatus.Signing:
                        StatusDeadline = DateTime.UtcNow
                            .AddDays(DeadlineToGetSignsInDays)
                            .EndOfDay()
                            .ConvertTimeToUtc();
                        break;
                    default:
                        StatusDeadline = null;
                        break;
                }
            }
        }

        public DateTime? StatusDeadline { get; set; }

        public DateTime StatusChangedAt { get; set; } = DateTime.UtcNow;

        public IList<Sign> Signs { get; set; } = new List<Sign>();

        public Answer? Answer { get; set; }

        public ModerationResult? ModerationResult { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

public enum PetitionStatus
{
    Created = 0,
    Declined = 1,
    Signing = 2,
    NotEnoughSigns = 3,
    WaitingForAnswer = 4,
    Answered = 5
}