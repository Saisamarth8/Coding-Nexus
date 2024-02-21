namespace TutorialWebApp.Models.ViewModels
{
    public class AddLikeRequest
    {
        public Guid TutorialPostId { get; set; }
        public Guid UserId { get; set; }
    }
}