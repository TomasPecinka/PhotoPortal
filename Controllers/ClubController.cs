using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoPortalWebApp.Interfaces;
using PhotoPortalWebApp.Models;
using PhotoPortalWebApp.ViewModels;

namespace PhotoPortalWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICloudinaryPhotoService _photoService;

        public ClubController(IClubRepository clubRepository, ICloudinaryPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _clubRepository = clubRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }

        // GET: Club
        public async Task<IActionResult> Index() 
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll();
            return View(clubs);
        }

        // GET: Club/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }

        // GET: Club/Create
        [Authorize]
        public IActionResult Create()
        {
            string? currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createClubViewModel = new CreateClubViewModel { AppUserId = currentUserId };
            return View(createClubViewModel);
        }
        
        // POST: Club/Create
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.ProfilePicture);
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    ProfilePictureUrl = result.Url.ToString(),
                    ClubCategory = clubVM.ClubCategory,
                    AppUserId = clubVM.AppUserId,
                    Address = new Address
                    {
                        City = clubVM.Address.City,
                        Street = clubVM.Address.Street,
                        PostalCode = clubVM.Address.PostalCode,
                        Country = clubVM.Address.Country,
                    }
                };
                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("CreateClubError", "Creation of club failed. Please try again.");
            }
            return View(clubVM);
        }


        // GET: Club/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            if (club == null) return View("Error");
            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                URL = club.ProfilePictureUrl,
                ClubCategory = club.ClubCategory
            };
            return View(clubVM);
        }

        
        // POST: Club/Edit/5
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", clubVM);
            }
            var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);

            if (userClub != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userClub.ProfilePictureUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(clubVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(clubVM.ProfilePicture);
                var club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    ProfilePictureUrl = photoResult.Url.ToString(),
                    AddressId = clubVM.AddressId,
                    Address = clubVM.Address,
                    ClubCategory = clubVM.ClubCategory
                };

                _clubRepository.Update(club);

                return RedirectToAction("Index");
            }
            else
            {
                return View(clubVM);
            }
        }

        // GET: Club/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }

        // POST: Club/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");

            _clubRepository.Delete(clubDetails);
            return RedirectToAction("Index");
        }
    }
}
