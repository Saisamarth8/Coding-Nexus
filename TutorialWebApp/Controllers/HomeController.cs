using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TutorialWebApp.Models;
using TutorialWebApp.Models.ViewModels;
using TutorialWebApp.Repositories;

namespace TutorialWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITutorialPostRepository tutorialPostRepository;
        private readonly ItagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger, ITutorialPostRepository tutorialPostRepository, ItagRepository tagRepository)
        {
            _logger = logger;
            this.tutorialPostRepository = tutorialPostRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            var tutorialPosts = await tutorialPostRepository.GetAllAsync();
            var tags = await tagRepository.GetAllAsync();
            var model = new HomeViewModel
            {
                TutorialPosts = tutorialPosts,
                Tags = tags
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
