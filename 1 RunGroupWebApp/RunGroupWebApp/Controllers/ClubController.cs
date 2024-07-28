using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.Repository;

namespace RunGroupWebApp.Controllers
{
    public class ClubController : Controller
    {
        //public readonly ApplicationDBContext _context;
        public readonly IClubRepository _clubRepository;
        public ClubController( IClubRepository clubRepository)
        {
            //_context = context;
            _clubRepository = clubRepository;
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
            return View();  
        }


    }
}
