using TutorialWebApp.Models.Domain;

namespace TutorialWebApp.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<TutorialPost> TutorialPosts { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
