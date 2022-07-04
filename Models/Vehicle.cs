using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GDi.WebAPI.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleID { get; set; }
        public string VehicleName { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Model { get; set; }
        public int? ProductionYear { get; set; }

        public int VehicleTypeID { get; set; }
        [ForeignKey("VehicleTypeID")]
        public VehicleType VehicleType { get; set; }
    }
}
