using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TutorialWebApp.Models.Domain;
using TutorialWebApp.Models.ViewModels;
using TutorialWebApp.Repositories;

namespace TutorialWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorialPostLikeController : ControllerBase
    {
        private readonly ITutorialPostLikeRepository tutorialPostLikeRepository;

        public TutorialPostLikeController(ITutorialPostLikeRepository tutorialPostLikeRepository)
        {
            this.tutorialPostLikeRepository = tutorialPostLikeRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            var model = new TutorialPostLike
            {
                TutorialPostId = addLikeRequest.TutorialPostId,
                UserId = addLikeRequest.UserId
            };

            await tutorialPostLikeRepository.AddLikeForTutorial(model);

            return Ok();
        }


        [HttpGet]
        [Route("{tutorialPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForTutorial([FromRoute] Guid tutorialPostId)
        {
            var totalLikes = await tutorialPostLikeRepository.GetTotalLikes(tutorialPostId);

            return Ok(totalLikes);
        }
    }
}
