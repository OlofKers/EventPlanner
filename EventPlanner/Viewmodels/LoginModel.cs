using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EventPlanner.Data;
using EventPlanner.Models;

namespace EventPlanner.Viewmodels
{
    public class LoginModel : PageModel
    {
        private readonly EventplannerContext _context;

        public LoginModel(EventplannerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string? Username { get; set; }

        [BindProperty]
        public string? Password { get; set; }
        public string? ErrorMessage = "Invalid Username or Password";

        public IActionResult OnPost()
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == Username);

            if (user != null && user.UserPassword == Password)
            {
                return RedirectToPage("/index");
            }
            return Page();         

        }
    }
}
