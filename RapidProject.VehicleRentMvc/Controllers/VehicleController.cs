using Microsoft.AspNetCore.Mvc;
using RapidProject.VehicleRentMvc.DAL.Repositories;
using RapidProject.VehicleRentMvc.DAL.Services;
using RapidProject.VehicleRentMvc.Models;
using System.Threading.Tasks;

namespace RapidProject.VehicleRentMvc.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleRepository _vehicleService;
        private readonly IVehicleTypeRepository _vehicleTypeService;

        public VehicleController(IVehicleRepository vehicleService, IVehicleTypeRepository vehicleTypeService)
        {
            _vehicleService = vehicleService;
            _vehicleTypeService = vehicleTypeService;
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
            return View(vehicle);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.VehicleTypes = await _vehicleTypeService.GetAll();
            return View();
        }

        [HttpPost]
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

        [HttpPost]
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
