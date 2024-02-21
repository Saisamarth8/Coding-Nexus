namespace TutorialWebApp.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public ICollection<TutorialPost> TutorialPosts { get; set; }
    }
}
