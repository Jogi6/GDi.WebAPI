using System.ComponentModel.DataAnnotations;

namespace GDi.WebAPI.Models
{
    public class VehicleType
    {
        [Key]
        public int VehicleTypeID { get; set; }
        public string TypeOfVehicle { get; set; }
    }
}
