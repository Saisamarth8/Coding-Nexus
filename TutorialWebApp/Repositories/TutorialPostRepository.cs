using Microsoft.EntityFrameworkCore;
using TutorialWebApp.Data;
using TutorialWebApp.Models.Domain;

namespace TutorialWebApp.Repositories
{
    public class TutorialPostRepository : ITutorialPostRepository
    {
        private readonly MyDbContext myDbContext;

        public TutorialPostRepository(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }
        public async Task<TutorialPost> AddAsync(TutorialPost tutorialPost)
        {
            await myDbContext.AddAsync(tutorialPost);
            await myDbContext.SaveChangesAsync();
            return tutorialPost;
        }

        public async Task<TutorialPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await myDbContext.TutorialPosts.FindAsync(id);
            if(existingBlog != null)
            {
                myDbContext.TutorialPosts.Remove(existingBlog);
                await myDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<TutorialPost>> GetAllAsync()
        {
            return await myDbContext.TutorialPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<TutorialPost?> GetAsync(Guid id)
        {
            return await myDbContext.TutorialPosts.Include(x=> x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TutorialPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await myDbContext.TutorialPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public Task<TutorialPost?> UpdateAsync(TutorialPost tutorialPost)
        {
            throw new NotImplementedException();
        }
    }
}
