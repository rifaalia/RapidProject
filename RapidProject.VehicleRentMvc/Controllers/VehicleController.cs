using Microsoft.AspNetCore.Mvc;
using RapidProject.VehicleRentMvc.DAL.Repositories;
using RapidProject.VehicleRentMvc.DAL.Services;
using RapidProject.VehicleRentMvc.Models;
using RapidProject.VehicleRentMvc.ViewModels;
using System.Threading.Tasks;

namespace RapidProject.VehicleRentMvc.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleRepository _vehicleService;
        private readonly IVehicleTypeRepository _vehicleTypeService;
        private readonly IRentRepository _rentService;
        private readonly IUserRepository _userService;

        public VehicleController(IVehicleRepository vehicleService,
            IVehicleTypeRepository vehicleTypeService,
            IRentRepository rentService,
            IUserRepository userService)
        {
            _vehicleService = vehicleService;
            _vehicleTypeService = vehicleTypeService;
            _rentService = rentService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleService.GetAll();
            return View(vehicles);
        }

        [HttpGet("vehicle/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await _vehicleService.GetById(id);
            
            if (vehicle == null)
            {
                return NotFound();
            }

            var rentalViewModel = new RentalViewModel
            {
                VehicleId = vehicle.VehicleId,
                VehicleMake = vehicle.Make,
                VehicleModel = vehicle.Model
            };

            ViewBag.User = await _userService.GetAll();

            return View(rentalViewModel);
        }

        [HttpPost("/vehicle/{id}")]
        public async Task<IActionResult> Details(RentalViewModel rentalViewModel)
        {
            if (ModelState.IsValid)
            {
                if (rentalViewModel.RentalStartDate >= rentalViewModel.RentalEndDate)
                {
                    ModelState.AddModelError(string.Empty, "Rental start date must be before the end date.");
                    return View(rentalViewModel);
                }

                var rental = new Rental
                {
                    VehicleId = rentalViewModel.VehicleId,
                    UserId = rentalViewModel.UserId,
                    RentalDate = rentalViewModel.RentalStartDate,
                    ReturnDate = rentalViewModel.RentalEndDate
                };

                await _rentService.Add(rental);

                return RedirectToAction("Index", "Home");
            }

            return View(rentalViewModel);
        }

        [HttpGet("/Vehicle/Create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.VehicleTypes = await _vehicleTypeService.GetAll();
            return View();
        }

        [HttpPost("/Vehicle/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                await _vehicleService.Add(vehicle);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.VehicleTypes = await _vehicleTypeService.GetAll();
            return View(vehicle);
        }

        [HttpGet("/Vehicle/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var vehicle = await _vehicleService.GetById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewBag.VehicleTypes = await _vehicleTypeService.GetAll();
            return View(vehicle);
        }

        [HttpPost("/Vehicle/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _vehicleService.Update(vehicle);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.VehicleTypes = await _vehicleTypeService.GetAll();
            return View(vehicle);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _vehicleService.GetById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
