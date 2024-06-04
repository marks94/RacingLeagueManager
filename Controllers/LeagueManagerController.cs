using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueManager.Data;
using RacingLeagueManager.Models;

namespace RacingLeagueManager.Controllers
{
    [Authorize]
    public class LeagueManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeagueManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var leagues = _context.Leagues.ToList();
            ViewBag.Leagues = leagues;

            return View();
        }

        public IActionResult CreateEditLeagueForm(int id)
        { 
            //check if new element gets created
            if (id == 0)
                return View("CreateEditLeague");

            //check if ID is in DB
            var leagueInDB = _context.Leagues.Find(id);
            if (leagueInDB == null)
                return NotFound();

            return View("CreateEditLeague", leagueInDB);
        }

        [HttpPost]
        public IActionResult CreateEditLeague(League league)
        {
            if (league.Id == 0)
                _context.Leagues.Add(league);
            else
                _context.Leagues.Update(league);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteLeague(int id)
        {
            var leagueInDB = _context.Leagues.Find(id);
            if (leagueInDB == null)
                return NotFound();

            _context.Leagues.Remove(leagueInDB);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
