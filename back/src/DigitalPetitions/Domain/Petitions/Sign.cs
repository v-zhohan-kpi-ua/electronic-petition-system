namespace DigitalPetitions.Domain.Petitions
{
    public class Sign
    {
        public int Id { get; set; }

        public int SigneeId { get; set; }

        public Signee? Signee { get; set; }

        public int PetitionId { get; set; }

        public Petition? Petition { get; set; }

        public DateTime SignedAt { get; set; } = DateTime.UtcNow;
    }
}
