using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EventPlanner.Data;
using EventPlanner.Models;

namespace EventPlanner.Viewmodels
{
    public class LoginModel
    {

        public string? Username { get; set; }
        public string? Password { get; set; }

    }
}
