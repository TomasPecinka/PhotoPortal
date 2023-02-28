using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using PhotoPortalWebApp.Interfaces;
using PhotoPortalWebApp.Models;
using PhotoPortalWebApp.Repositories;
using PhotoPortalWebApp.ViewModels;

namespace PhotoPortalWebApp.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICloudinaryPhotoService _cloudinaryPhotoService;

        public UserProfileController(IUserProfileRepository userProfileRepository, IHttpContextAccessor httpContextAccessor,
            ICloudinaryPhotoService photoService)
        {
            _userProfileRepository = userProfileRepository;
            _httpContextAccessor = httpContextAccessor;
            _cloudinaryPhotoService = photoService;
        }
        private void MapUserEdit(AppUser user, EditUserProfileViewModel editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.FullName = editVM.FullName;
            user.ProfileImageUrl = photoResult.Url.ToString();
        }

        // GET: UserProfile
        public async Task<IActionResult> Index()
        {
            var userPhotos = await _userProfileRepository.GetAllUserPhotos();
            var userClubs = await _userProfileRepository.GetAllUserClubs();
            var userProfileViewModel = new UserProfileViewModel()
            {
                Photos = userPhotos,
                Clubs = userClubs,
            };
            return View(userProfileViewModel);
        }

        // GET: UserProfile/Edit
        public async Task<IActionResult> Edit()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _userProfileRepository.GetUserById(currentUserId);
            if (user == null) return View("Error");
            var editUserViewModel = new EditUserProfileViewModel()
            {
                Id = currentUserId,
                FullName = user.FullName,
                ProfileImageUrl = user.ProfileImageUrl,
            };
            return View(editUserViewModel);
        }

        // POST: UserProfile/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserProfileViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("Edit", editVM);
            }

            var user = await _userProfileRepository.GetUserByIdNoTracking(editVM.Id);

            user.FullName = editVM.FullName;
            _userProfileRepository.Update(user);

            if (editVM.ProfileImage == null) 
            {
                return RedirectToAction("Index");
            }

            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _cloudinaryPhotoService.AddPhotoAsync(editVM.ProfileImage);
                MapUserEdit(user, editVM, photoResult);

                _userProfileRepository.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _cloudinaryPhotoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo.");
                    return View(editVM);
                }

                var photoResult = await _cloudinaryPhotoService.AddPhotoAsync(editVM.ProfileImage);
                MapUserEdit(user, editVM, photoResult);

                _userProfileRepository.Update(user);
                return RedirectToAction("Index");
            }
        }
    }
}
