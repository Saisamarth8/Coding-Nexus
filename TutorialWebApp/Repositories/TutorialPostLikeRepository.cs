
using Microsoft.EntityFrameworkCore;
using TutorialWebApp.Data;
using TutorialWebApp.Models.Domain;

namespace TutorialWebApp.Repositories
{
    public class TutorialPostLikeRepository : ITutorialPostLikeRepository
    {
        private readonly MyDbContext myDbContext;

        public TutorialPostLikeRepository(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }
        public async Task<TutorialPostLike> AddLikeForTutorial(TutorialPostLike tutorialPostLike)
        {
            await myDbContext.TutorialPostLike.AddAsync(tutorialPostLike);
            await myDbContext.SaveChangesAsync();
            return tutorialPostLike;
        }

        public async Task<IEnumerable<TutorialPostLike>> GetLikesForTutorial(Guid tutorialPostId)
        {
            return await myDbContext.TutorialPostLike.Where(x => x.TutorialPostId == tutorialPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid tutorialPostId)
        {
            return await myDbContext.TutorialPostLike.CountAsync(x => x.TutorialPostId == tutorialPostId);
        }
    }
}
