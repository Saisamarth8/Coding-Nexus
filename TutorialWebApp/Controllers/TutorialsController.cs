using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TutorialWebApp.Models.Domain;
using TutorialWebApp.Models.ViewModels;
using TutorialWebApp.Repositories;

namespace TutorialWebApp.Controllers
{
    public class TutorialsController : Controller
    {
        private readonly ITutorialPostRepository tutorialPostRepository;
        private readonly ITutorialPostLikeRepository tutorialPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITutorialPostCommentRepository tutorialPostCommentRepository;

        public TutorialsController(ITutorialPostRepository tutorialPostRepository, ITutorialPostLikeRepository tutorialPostLikeRepository, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ITutorialPostCommentRepository tutorialPostCommentRepository )
        {
            this.tutorialPostRepository = tutorialPostRepository;
            this.tutorialPostLikeRepository = tutorialPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.tutorialPostCommentRepository = tutorialPostCommentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
            var tutorialPost = await tutorialPostRepository.GetByUrlHandleAsync(urlHandle);
            var tutorialDetailsViewModel = new TutorialDetailsViewModel();

            if (tutorialPost != null)
            {
                var totalLikes = await tutorialPostLikeRepository.GetTotalLikes(tutorialPost.Id);

                if (signInManager.IsSignedIn(User))
                {
                    // Get like for this blog for this user
                    var likesForBlog = await tutorialPostLikeRepository.GetLikesForTutorial(tutorialPost.Id);

                    var userId = userManager.GetUserId(User);

                    if (userId != null)
                    {
                        var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }

                }
                // Get comments for blog post
                var tutorialCommentsDomainModel = await tutorialPostCommentRepository.GetCommentsByTutorialIdAsync(tutorialPost.Id);

                var tutorialCommentsForView = new List<TutorialCommentViewModel>();

                foreach (var tutorialCommentViewModel in tutorialCommentsDomainModel)
                {
                    tutorialCommentsForView.Add(new TutorialCommentViewModel
                    {
                        Description = tutorialCommentViewModel.Content,
                        DateAdded = tutorialCommentViewModel.DateAdded,
                        Username = (await userManager.FindByIdAsync(tutorialCommentViewModel.UserId.ToString())).UserName
                    });
                }

                tutorialDetailsViewModel = new TutorialDetailsViewModel
                {
                    Id = tutorialPost.Id,
                    Content = tutorialPost.Content,
                    PageTitle = tutorialPost.PageTitle,
                    Author = tutorialPost.Author,
                    FeaturedImageUrl = tutorialPost.FeaturedImageUrl,
                    Heading = tutorialPost.Heading,
                    PublishedDate = tutorialPost.PublishedDate,
                    ShortDescription = tutorialPost.ShortDescription,
                    UrlHandle = tutorialPost.UrlHandle,
                    Visible = tutorialPost.Visible,
                    Tags = tutorialPost.Tags,
                    TotalLikes = totalLikes,
                    Liked = liked,
                    Comments = tutorialCommentsForView
                };
            }
                return View(tutorialDetailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TutorialDetailsViewModel tutorialDetailsViewModel)
        {
            if (signInManager.IsSignedIn(User))
            {
                var domainModel = new TutorialComment
                {
                    TutorialPostId = tutorialDetailsViewModel.Id,
                    Content = tutorialDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
                };

                await tutorialPostCommentRepository.AddAsync(domainModel);
                return RedirectToAction("Index", "Tutorials",
                    new { urlHandle = tutorialDetailsViewModel.UrlHandle });
            }

            return View();
        }
    }
}





