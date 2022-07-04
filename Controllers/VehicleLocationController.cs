using GDi.WebAPI.Data;
using GDi.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GDi.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleLocationController : Controller
    {
        private readonly GDiDbContext gdiDbContex;

        public VehicleLocationController(GDiDbContext gdiDbContex)
        {
            this.gdiDbContex = gdiDbContex;
        }

        //Get all VehiclesLocations
        [HttpGet]
        public async Task<IActionResult> GetAllVehiclesLocations()
        {
            var vehicleslocations = await gdiDbContex.VehicleLocations.ToListAsync();
            return Ok(vehicleslocations);
        }

        //Get single vehicle location
        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetVehicleLocation")]
        public async Task<IActionResult> GetVehicleLocation([FromRoute] int id)
        {
            var vehicleLocation = await gdiDbContex.VehicleLocations.FirstOrDefaultAsync(x => x.VehicleLocationID == id);
            if (vehicleLocation != null)
            {
                return Ok(vehicleLocation);
            }
            return NotFound("Location for this vehicle doesn't exist!");
        }

        //Add vehicle location
        [HttpPost]
        public async Task<IActionResult> AddVehicleLocation([FromBody] VehicleLocation vehicleLocation)
        {
            var vehicle = await gdiDbContex.Vehicles.FirstOrDefaultAsync(x => x.VehicleID == vehicleLocation.VehicleID);
            var vehicleType = await gdiDbContex.VehicleTypes.FirstOrDefaultAsync(x => x.VehicleTypeID == vehicle.VehicleTypeID);
            vehicleLocation.Vehicle = vehicle;
            vehicleLocation.Vehicle.VehicleType = vehicleType;
            await gdiDbContex.VehicleLocations.AddAsync(vehicleLocation);
            await gdiDbContex.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVehicleLocation), new { id = vehicleLocation.VehicleID }, vehicleLocation);
        }

        //Delete vehicle location
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteVehicleLocation([FromRoute] int id)
        {
            var existingVehicleLocation = await gdiDbContex.VehicleLocations.FirstOrDefaultAsync(x => x.VehicleLocationID == id);
            if (existingVehicleLocation != null)
            {
                gdiDbContex.Remove(existingVehicleLocation);
                await gdiDbContex.SaveChangesAsync();
                return Ok(existingVehicleLocation);
            }

            return NotFound("Vehicle location doesn't exist!");
        }

    }
}
