using Microsoft.AspNetCore.Mvc;
using RapidProject.VehicleRentMvc.DAL.Repositories;

namespace RapidProject.VehicleRentMvc.Controllers
{
    public class RentalController : Controller
    {
        private readonly IRentRepository _rentService;

        public RentalController(IRentRepository rentRepository)
        {
            _rentService = rentRepository; 
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
