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
        public ClubController( IClubRepository clubRepository, IPhotoService photoService)
        {
            //_context = context;
            _clubRepository = clubRepository;
            _photoService = photoService;
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
            return View();  
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
            } else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(club);
        }
        public IActionResult Edit() { 
            return View();
        }
    }
}
