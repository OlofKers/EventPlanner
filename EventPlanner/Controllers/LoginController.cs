using EventPlanner.Data;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Controllers
{
    public class LoginController : Controller
    {
        private readonly EventplannerContext _context;

        public LoginController(EventplannerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


    }
}