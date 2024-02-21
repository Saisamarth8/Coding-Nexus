using Microsoft.AspNetCore.Identity;

namespace TutorialWebApp.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
