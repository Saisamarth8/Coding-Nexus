using Microsoft.EntityFrameworkCore;
using TutorialWebApp.Data;
using TutorialWebApp.Models.Domain;

namespace TutorialWebApp.Repositories
{
    public class TutorialPostCommentRepository : ITutorialPostCommentRepository
    {
        private readonly MyDbContext myDbContext;

        public TutorialPostCommentRepository(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }
        public async Task<TutorialComment> AddAsync(TutorialComment tutorialComment)
        {
            await myDbContext.TutorialComment.AddAsync(tutorialComment);
            await myDbContext.SaveChangesAsync();
            return tutorialComment;
        }

        public async Task<IEnumerable<TutorialComment>> GetCommentsByTutorialIdAsync(Guid tutorialPostId)
        {
            return await myDbContext.TutorialComment.Where(x => x.TutorialPostId == tutorialPostId).ToListAsync();
        }
    }
}
