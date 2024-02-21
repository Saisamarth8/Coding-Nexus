using TutorialWebApp.Models.Domain;

namespace TutorialWebApp.Repositories
{
    public interface ITutorialPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid tutorialPostId);

        Task<IEnumerable<TutorialPostLike>> GetLikesForTutorial(Guid tutorialPostId);

        Task<TutorialPostLike> AddLikeForTutorial(TutorialPostLike tutorialPostLike);
    }
}
