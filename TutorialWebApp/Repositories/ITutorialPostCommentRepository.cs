using TutorialWebApp.Models.Domain;

namespace TutorialWebApp.Repositories
{
    public interface ITutorialPostCommentRepository
    {
        Task<TutorialComment> AddAsync(TutorialComment tutorialComment);

        Task<IEnumerable<TutorialComment>> GetCommentsByTutorialIdAsync(Guid tutorialPostId);
    }
}
