using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GDi.WebAPI.Models
{
    public class VehicleLocation
    {
        [Key]
        public int VehicleLocationID { get; set; }

        [Precision(18, 5)]
        public decimal Longitude { get; set; }
        
        [Precision(18, 5)]
        public decimal Latitude { get; set; }

        public int VehicleID { get; set; }
        [ForeignKey("VehicleID")]
        public Vehicle Vehicle { get; set; }
    }
}
