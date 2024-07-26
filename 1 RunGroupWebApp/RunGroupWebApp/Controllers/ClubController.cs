using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Controllers
{
    public class ClubController : Controller
    {
        public readonly ApplicationDBContext _context;
        public ClubController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //Load all data in Club table to Club View
            List<Club> clubs = _context.Clubs.ToList();
            return View(clubs);
            //return BadRequest();
        }

        //Data for single club detail page
        public IActionResult Detail(int id)
        {
            Club club = _context.Clubs.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            return View(club);
        }


    }
}
