using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RunGroupWebApp.Data;
using RunGroupWebApp.DTO;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.Repository;

namespace RunGroupWebApp.Controllers
{
    public class RaceController : Controller
    {
        //public readonly ApplicationDBContext _context;
        public readonly IRaceRepository _raceRepository;
        public readonly IPhotoService _photoService;
        public RaceController(IRaceRepository raceRepository, IPhotoService photoService)
        {
            //_context = context;
            _raceRepository = raceRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            //List<Race> races = _context.Races.ToList(); 
            IEnumerable<Race> races = await _raceRepository.GetAll();
            return View(races);
        }
        //Data for single race detail page
        public async Task<IActionResult> Detail(int id)
        {
            //Race race = _context.Races.Include(a => a.Address).FirstOrDefault(r => r.Id == id);
            Race race = await _raceRepository.GetByIdAsync(id);
            return View(race);
        }
        //Create page
        public IActionResult Create()
        {
            //asp-for = label for and id of something
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceDTO raceModel)
        {
            if (ModelState.IsValid) {
                var result = await _photoService.AddPhotoAsync(raceModel.Image);
                var savedRace = new Race
                {
                    Title = raceModel.Title,
                    Description = raceModel.Description,
                    Address = new Address
                    {
                        Street = raceModel.Address.Street,
                        City = raceModel.Address.City,
                        State = raceModel.Address.State,
                    },
                    Image = result.Url.ToString(),
                };
                _raceRepository.Add(savedRace);
                return RedirectToAction("Index");
            } else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(raceModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);
            if (race == null) return View("Error");
            var savedRace = new EditRaceDTO
            {
                Title = race.Title,
                Description = race.Description,
                AddressId = race.AddressId,
                Address = race.Address,
                URL = race.Image,
                RaceCategory = race.RaceCategory,
            };
            return View(savedRace);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceDTO raceModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit race");
                return View("Edit", raceModel);
            }
            var userRace = await _raceRepository.GetByIdAsyncNoTracking(id);
            if (userRace != null)
            {
                try
                {
                    if (raceModel.Image != null)
                        await _photoService.DeletePhotoAsync(userRace.Image);
                }
                catch (Exception ex) {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(raceModel);
                }
                var photoResult = await _photoService.AddPhotoAsync(raceModel.Image);
                var race = new Race
                {
                    Id = id,
                    Title = raceModel.Title,
                    Description = raceModel.Description,
                    AddressId = raceModel.AddressId,
                    Address = raceModel.Address,
                    Image = photoResult.Url.ToString() != null ? photoResult.Url.ToString() : userRace.Image,
                };
                _raceRepository.Update(race);
                return RedirectToAction("Index"); 
            } else
            {
                return View(raceModel);
            }

            
        }
    }
}
