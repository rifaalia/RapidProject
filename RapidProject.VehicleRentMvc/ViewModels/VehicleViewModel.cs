using RapidProject.VehicleRentMvc.DTOs;

namespace RapidProject.VehicleRentMvc.ViewModels
{
    public class VehicleViewModel
    {
        public IEnumerable<VehicleTypeDto> VehicleTypes { get; set; }
        public int VehicleTypeId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public decimal RentalPrice { get; set; }
        public int AvailibilityStatus { get; set; }

    }
}
