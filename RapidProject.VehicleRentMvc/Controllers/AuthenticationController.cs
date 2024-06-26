using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using RapidProject.VehicleRentMvc.Models;

namespace RapidProject.VehicleRentMvc.Controllers
{
    public class AuthenticationController : Controller
    {
     
        public IActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<ActionResult> Register(RegisterDto registerDto)
        //{
        //    return RedirectToAction("Index");
        //}
    }
}
