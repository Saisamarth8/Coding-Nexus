namespace TutorialWebApp.Models.Domain
{
    public class TutorialComment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid TutorialPostId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
