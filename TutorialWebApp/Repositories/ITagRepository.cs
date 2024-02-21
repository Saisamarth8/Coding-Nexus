using TutorialWebApp.Models.Domain;

namespace TutorialWebApp.Repositories
{
        public interface ItagRepository
        {
            Task<IEnumerable<Tag>> GetAllAsync();

            Task<Tag?> GetAsync(Guid id);

            Task<Tag> AddAsync(Tag tag);

            Task<Tag?> UpdateAsync(Tag tag);

            Task<Tag?> DeleteAsync(Guid id);

        }
    }
