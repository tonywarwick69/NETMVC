using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.Repository;

namespace RunGroupWebApp.Controllers
{
    public class RaceController : Controller
    {
        //public readonly ApplicationDBContext _context;
        public readonly IRaceRepository _raceRepository;
        public RaceController(IRaceRepository raceRepository)
        {
            //_context = context;
            _raceRepository = raceRepository;
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
        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
            {
                return View(race);
            }
            _raceRepository.Add(race);
            return RedirectToAction("Index");
        }
    }
}
