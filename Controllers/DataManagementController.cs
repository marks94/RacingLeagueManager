using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RacingLeagueManager.Controllers
{
    [Authorize]
    public class DataManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
