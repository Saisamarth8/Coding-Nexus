using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TutorialWebApp.Models.Domain;
using TutorialWebApp.Models.ViewModels;
using TutorialWebApp.Repositories;

namespace TutorialWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTutorialPostsController : Controller
    {
        private readonly ItagRepository tagRepository;
        private readonly ITutorialPostRepository tutorialPostRepository;
        public AdminTutorialPostsController(ItagRepository tagRepository, ITutorialPostRepository tutorialPostRepository)
        {
            this.tagRepository = tagRepository;
            this.tutorialPostRepository = tutorialPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();
            var model = new AddTutorialPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTutorialPostRequest addTutorialPostRequest)
        {
            // Map view model to domain model
            var tutorialPost = new TutorialPost
            {
                Heading = addTutorialPostRequest.Heading,
                PageTitle = addTutorialPostRequest.PageTitle,
                Content = addTutorialPostRequest.Content,
                ShortDescription = addTutorialPostRequest.ShortDescription,
                FeaturedImageUrl = addTutorialPostRequest.FeaturedImageUrl,
                UrlHandle = addTutorialPostRequest.UrlHandle,
                PublishedDate = addTutorialPostRequest.PublishedDate,
                Author = addTutorialPostRequest.Author,
                Visible = addTutorialPostRequest.Visible,
            };

            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addTutorialPostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdAsGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            // Mapping tags back to domain model
            tutorialPost.Tags = selectedTags;


            await tutorialPostRepository.AddAsync(tutorialPost);

            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            // Call the repository 
            var tutorialPosts = await tutorialPostRepository.GetAllAsync();

            return View(tutorialPosts);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Retrieve the result from the repository 
            var tutorialPost = await tutorialPostRepository.GetAsync(id);
            var tagsDomainModel = await tagRepository.GetAllAsync();

            if (tutorialPost != null)
            {
                // map the domain model into the view model
                var model = new EditTutorialPostRequest
                {
                    Id = tutorialPost.Id,
                    Heading = tutorialPost.Heading,
                    PageTitle = tutorialPost.PageTitle,
                    Content = tutorialPost.Content,
                    Author = tutorialPost.Author,
                    FeaturedImageUrl = tutorialPost.FeaturedImageUrl,
                    UrlHandle = tutorialPost.UrlHandle,
                    ShortDescription = tutorialPost.ShortDescription,
                    PublishedDate = tutorialPost.PublishedDate,
                    Visible = tutorialPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = tutorialPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(model);

            }

            // pass data to view
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTutorialPostRequest editTutorialPostRequest)
        {
            // map view model back to domain model
            var tutorialPostDomainModel = new TutorialPost
            {
                Id = editTutorialPostRequest.Id,
                Heading = editTutorialPostRequest.Heading,
                PageTitle = editTutorialPostRequest.PageTitle,
                Content = editTutorialPostRequest.Content,
                Author = editTutorialPostRequest.Author,
                ShortDescription = editTutorialPostRequest.ShortDescription,
                FeaturedImageUrl = editTutorialPostRequest.FeaturedImageUrl,
                PublishedDate = editTutorialPostRequest.PublishedDate,
                UrlHandle = editTutorialPostRequest.UrlHandle,
                Visible = editTutorialPostRequest.Visible
            };

            // Map tags into domain model

            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editTutorialPostRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);

                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            tutorialPostDomainModel.Tags = selectedTags;

            // Submit information to repository to update
            var updatedBlog = await tutorialPostRepository.UpdateAsync(tutorialPostDomainModel);

            if (updatedBlog != null)
            {
                // Show success notification
                //TempData["SuccessMessage"] = "Tutorial post updated successfully!";
                return RedirectToAction("Edit");
            }

            // Show error notification
            //TempData["ErrorMessage"] = "Error updating the tutorial post. Please try again.";
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTutorialPostRequest editTutorialPostRequest)
        {
            var deletedBlogPost = await tutorialPostRepository.DeleteAsync(editTutorialPostRequest.Id);

            if (deletedBlogPost != null)
            {
                // Show success notification
               // TempData["SuccessMessage"] = "Tutorial post deleted successfully!";
                return RedirectToAction("List");
            }

           // TempData["ErrorMessage"] = "Error deleting the tutorial post. Please try again.";
            return RedirectToAction("Edit", new { id = editTutorialPostRequest.Id });

        }
    }
}
