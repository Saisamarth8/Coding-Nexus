using TutorialWebApp.Data;
using TutorialWebApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace TutorialWebApp.Repositories
{
    public class TagRepository : ItagRepository
    {
        private readonly MyDbContext myDbContext;

        public TagRepository(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await myDbContext.Tags.AddAsync(tag);
            await myDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await myDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                myDbContext.Tags.Remove(existingTag);
                await myDbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
           return  await myDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return myDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await myDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await myDbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }
    }
}
