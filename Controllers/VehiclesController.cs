using GDi.WebAPI.Data;
using GDi.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GDi.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : Controller
    {
        private readonly GDiDbContext gdiDbContex;

        public VehiclesController(GDiDbContext gdiDbContex)
        {
            this.gdiDbContex = gdiDbContex;
        }
        //Get all Vehicles
        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            var vehicles = await gdiDbContex.Vehicles.ToListAsync();
            foreach (var vehicle in vehicles)
            {
                vehicle.VehicleType = await gdiDbContex.VehicleTypes.FirstOrDefaultAsync(x => x.VehicleTypeID == vehicle.VehicleTypeID);
            }
            return Ok(vehicles);
        }

        //Get single vehicle
        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetVehicle")]
        public async Task<IActionResult> GetVehicle([FromRoute] int id)
        {
            var vehicle = await gdiDbContex.Vehicles.FirstOrDefaultAsync(x=>x.VehicleID == id);
            if (vehicle != null)
            {
                vehicle.VehicleType = await gdiDbContex.VehicleTypes.FirstOrDefaultAsync(x => x.VehicleTypeID == vehicle.VehicleTypeID);
                return Ok(vehicle);
            }
            return NotFound("Vehicle doesn't exist!");
        }

        //Add single vehicle
        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] Vehicle vehicle)
        {
            vehicle.VehicleType = await gdiDbContex.VehicleTypes.FirstOrDefaultAsync(x => x.VehicleTypeID == vehicle.VehicleTypeID);
            await gdiDbContex.Vehicles.AddAsync(vehicle);
            await gdiDbContex.SaveChangesAsync();

            // Creates 201 HttpResponse
            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.VehicleID }, vehicle);
        }

        //Update vehicle
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateVehicle([FromRoute] int id, [FromBody] Vehicle vehicle)
        {
            var existingVehicle = await gdiDbContex.Vehicles.FirstOrDefaultAsync(x => x.VehicleID == id);
            if (existingVehicle != null)
            {
                existingVehicle.VehicleTypeID = vehicle.VehicleTypeID;
                existingVehicle.VehicleName = vehicle.VehicleName;
                existingVehicle.RegistrationNumber = vehicle.RegistrationNumber;
                existingVehicle.Model = vehicle.Model;
                existingVehicle.ProductionYear = vehicle.ProductionYear;
                existingVehicle.VehicleType = await gdiDbContex.VehicleTypes.FirstOrDefaultAsync(x => x.VehicleTypeID == vehicle.VehicleTypeID);
                await gdiDbContex.SaveChangesAsync();
                return Ok(existingVehicle);
            }

            return NotFound("Vehicle doesn't exist!");
        }

        //Delete vehicle
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int id)
        {
            var existingVehicle = await gdiDbContex.Vehicles.FirstOrDefaultAsync(x => x.VehicleID == id);
            if (existingVehicle != null)
            {
                gdiDbContex.Remove(existingVehicle);
                await gdiDbContex.SaveChangesAsync();
                return Ok(existingVehicle);
            }

            return NotFound("Vehicle doesn't exist!");
        }
    }
}
