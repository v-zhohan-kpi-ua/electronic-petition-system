namespace DigitalPetitions.Domain.Petitions
{
    public class Creator : Person
    {
        public int PetitionId { get; set; }

        public Petition? Petition { get; set; }
    }
}
