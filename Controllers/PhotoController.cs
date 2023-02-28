using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoPortalWebApp.Data;
using PhotoPortalWebApp.Interfaces;
using PhotoPortalWebApp.Models;
using PhotoPortalWebApp.Repositories;
using PhotoPortalWebApp.ViewModels;

namespace PhotoPortalWebApp.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICloudinaryPhotoService _cloudinaryPhotoService;

        public PhotoController(IPhotoRepository photoRepository, IHttpContextAccessor httpContextAccessor, ICloudinaryPhotoService cloudinaryPhotoService)
        {
            _photoRepository = photoRepository;
            _cloudinaryPhotoService = cloudinaryPhotoService;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Photo
        public async Task<IActionResult> Index()
        {
            IEnumerable<Photo> photos = await _photoRepository.GetAllAsync();
            return View(photos);
        }

        // GET: Photo/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Photo photo = await _photoRepository.GetByIdAsync(id);

            if (photo == null) return NotFound();

            return View(photo);
        }

        // GET: Photo/Create
        [Authorize]
        public IActionResult Create()
        {
            if (_httpContextAccessor == null)
            {
                throw new ArgumentNullException(nameof(_httpContextAccessor));
            }

            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            //AppUser currentUser = await _userRepository.GetByIdAsync(currentUserId);
            var PhotoVM = new CreatePhotoViewModel { AppUserId = currentUserId};
            return View(PhotoVM);
        }

        // POST: Photo/Create
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreatePhotoViewModel photoVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _cloudinaryPhotoService.AddPhotoAsync(photoVM.Image);
                var photo = new Photo
                {
                    Title = photoVM.Title,
                    Description = photoVM.Description,
                    ImageUrl = result.Url.ToString(),
                    PhotoCategory = photoVM.PhotoCategory,
                    UploadDateTime = DateTime.Now,
                    AppUserId = photoVM.AppUserId,
                };
                await _photoRepository.AddAsync(photo);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(photoVM);
        }

        // GET: Photo/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var photo = await _photoRepository.GetByIdAsync(id);
            if (photo == null) return View("Error");
            var photoVM = new EditPhotoViewModel
            {
                Title = photo.Title,
                Description = photo.Description,
                ImageUrl = photo.ImageUrl,
                PhotoCategory = photo.PhotoCategory,
                AppUserId = photo.AppUserId,
            };
            return View(photoVM);
        }

        // POST: Photo/Edit/5
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditPhotoViewModel photoVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit photo.");
                return View("Edit", photoVM);
            }
            var userPhoto = await _photoRepository.GetByIdAsyncNoTracking(id);

            if (userPhoto != null)
            {
                try
                {
                    await _cloudinaryPhotoService.DeletePhotoAsync(userPhoto.ImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(photoVM);
                }
                var photoResult = await _cloudinaryPhotoService.AddPhotoAsync(photoVM.Image);
                var photo = new Photo
                {
                    Id = id,
                    Title = photoVM.Title,
                    Description = photoVM.Description,
                    ImageUrl = photoResult.Url.ToString(),
                    PhotoCategory = photoVM.PhotoCategory,
                    AppUserId = photoVM.AppUserId,
                };

                _photoRepository.Update(photo);

                return RedirectToAction("Index");
            }
            else
            {
                return View(photoVM);
            }
        }

        // GET: Photo/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            Photo photo = await _photoRepository.GetByIdAsync(id);
            if (photo == null) return View("Error");
            return View(photo);
        }

        // POST: Photo/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            Photo photo = await _photoRepository.GetByIdAsync(id);
            if (photo == null) return View("Error");

            _photoRepository.Delete(photo);
            return RedirectToAction("Index");
        }
    }
}
