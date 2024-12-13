using EventPlanner.Data;
using EventPlanner.Viewmodels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.Include(u => u.Role)
                    .SingleOrDefaultAsync(u => u.UserName == model.Username);

                if (user != null && model.Password == user.UserPassword)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role.Name),
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                    };

                    var claimsId = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsId);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(model);

        }
    }
}