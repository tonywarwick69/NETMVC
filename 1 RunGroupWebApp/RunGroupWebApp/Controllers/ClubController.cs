using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.DTO;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.Repository;
using System.IO;

namespace RunGroupWebApp.Controllers
{
    public class ClubController : Controller
    {
        //public readonly ApplicationDBContext _context;
        public readonly IClubRepository _clubRepository;
        public readonly IPhotoService _photoService;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public ClubController(IClubRepository clubRepository, IPhotoService photoService, IHttpContextAccessor contextAccessor)
        {
            //_context = context;
            _clubRepository = clubRepository;
            _photoService = photoService;
            _httpContextAccessor = contextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            //Load all data in Club table to Club View
            IEnumerable<Club> clubs = await _clubRepository.GetAll();
            return View(clubs);
            //return BadRequest();
        }

        //Data for single club detail page
        public async Task<IActionResult> Detail(int id)
        {
            //Club club = _context.Clubs.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }

        //Create page
        public IActionResult Create()
        {
            //asp-for = label for and id of something
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createClubDTO = new CreateClubDTO
            {
                AppUserID = curUserId,
            };
            return View(createClubDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateClubDTO club)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(club);
            //}
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(club.Image);
                var savedClub = new Club
                {
                    Title = club.Title,
                    Description = club.Description,
                    AppUserId = club.AppUserID,
                    Address = new Address
                    {
                        Street = club.Address.Street,
                        City = club.Address.City,
                        State = club.Address.State,
                    },
                    Image = result.Url.ToString(),

                };
                _clubRepository.Add(savedClub);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(club);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);
            if (clubDetails == null) throw new Exception("Club " + id + " not found.");
            return View(clubDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var foundClub = await _clubRepository.GetByIdAsync(id);
            if (foundClub != null)
            {
                _clubRepository.Delete(foundClub);
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception("Club " + id + " not found.");
            }

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            if (club == null) return View("Error");
            var savedClub = new EditClubDTO
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                URL = club.Image,
                ClubCategory = club.ClubCategory,

            };
            return View(savedClub);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubDTO clubModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", clubModel);
            }
            var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);
            if (userClub != null)
            {
                try
                {
                    if(clubModel.Image != null) 
                        await _photoService.DeletePhotoAsync(userClub.Image);
                    
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(clubModel);
                }
                var photoResult = await _photoService.AddPhotoAsync(clubModel.Image);
                
                var club = new Club
                {
                    Id = id,
                    Title = clubModel.Title,
                    Description = clubModel.Description,
                    Image = photoResult.Url.ToString() != null ? photoResult.Url.ToString() : userClub.Image,
                    AddressId = clubModel.AddressId,
                    Address = clubModel.Address,
                };
                _clubRepository.Update(club);

                return RedirectToAction("Index");
            } else
            {
                return View(clubModel);
            }
      
        }

    }
}
