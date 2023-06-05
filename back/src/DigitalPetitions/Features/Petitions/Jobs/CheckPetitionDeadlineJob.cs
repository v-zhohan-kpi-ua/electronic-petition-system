using DigitalPetitions.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace DigitalPetitions.Features.Petitions.Jobs
{
    public class CheckPetitionDeadlineJob : IJob
    {
        private readonly ApplicationDbContext _dbContext;

        public CheckPetitionDeadlineJob(ApplicationDbContext context) {
            _dbContext = context;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var petitionsToBeClosed = await _dbContext.Petitions
                .Where(p => p.Status == PetitionStatus.Signing)
                .Where(p => p.StatusDeadline <= DateTime.UtcNow)
                .ToListAsync();

            petitionsToBeClosed.ForEach(petition =>
            {
                petition.Status = PetitionStatus.NotEnoughSigns;
            });

            await _dbContext.SaveChangesAsync();
        }
    }
}
