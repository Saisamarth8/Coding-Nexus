using TutorialWebApp.Models.Domain;

namespace TutorialWebApp.Repositories
{
    public interface ITutorialPostRepository
    {
        Task<IEnumerable<TutorialPost>> GetAllAsync();

        Task<TutorialPost?> GetAsync(Guid id);

        Task<TutorialPost?> GetByUrlHandleAsync(string urlHandle);

        Task<TutorialPost> AddAsync(TutorialPost tutorialPost);

        Task<TutorialPost?> UpdateAsync(TutorialPost tutorialPost);

        Task<TutorialPost?> DeleteAsync(Guid id);
    }
}
