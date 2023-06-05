using DigitalPetitions.Domain.Petitions;

namespace DigitalPetitions.Domain.AdminNS
{
    public class Admin : User
    {
        public Admin() : base()
        {
            Role = UserRole.Admin;
        }

        public IList<ModerationResult> ModerationResults { get; set; } = new List<ModerationResult>();

        public IList<Answer> PublishedAnswers { get; set; } = new List<Answer>();
    }
}
