namespace TutorialWebApp.Models.Domain
{
    public class TutorialPostLike
    {
        public Guid Id { get; set; }
        public Guid TutorialPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
