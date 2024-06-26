namespace RapidProject.VehicleRentMvc.DTOs
{
    public class VehicleDto
    {
        public int VehicleTypeId { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Year { get; set; }

        public decimal RentalPrice { get; set; }

        public int AvailabilityStatus { get; set; }

        public VehicleTypeDto VehicleTypeDto { get; set; }

    }
}
