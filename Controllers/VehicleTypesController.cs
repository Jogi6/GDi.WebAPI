using GDi.WebAPI.Data;
using GDi.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GDi.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleTypesController : Controller
    {
        private readonly GDiDbContext gdiDbContex;

        public VehicleTypesController(GDiDbContext gdiDbContex)
        {
            this.gdiDbContex = gdiDbContex;
        }
        //Get all VehiclesTypes
        [HttpGet]
        public async Task<IActionResult> GetAllVehiclesTypes()
        {
            var vehiclesTypes = await gdiDbContex.VehicleTypes.ToListAsync();
            return Ok(vehiclesTypes);
        }

        //Get single vehicle type
        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetVehicleType")]
        public async Task<IActionResult> GetVehicleType([FromRoute] int id)
        {
            var vehicleType = await gdiDbContex.VehicleTypes.FirstOrDefaultAsync(x => x.VehicleTypeID == id);
            if (vehicleType != null)
            {
                return Ok(vehicleType);
            }
            return NotFound("Vehicle type doesn't exist!");
        }

        //Add vehicle type
        [HttpPost]
        public async Task<IActionResult> AddVehicleType([FromBody] VehicleType vehicleType)
        {
            await gdiDbContex.VehicleTypes.AddAsync(vehicleType);
            await gdiDbContex.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVehicleType), new { id = vehicleType.VehicleTypeID }, vehicleType);
        }

        //Update a vehicle
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteVehicleType([FromRoute] int id)
        {
            if (id == 1)
            {
                return NotFound("Can't delete default vehicle type");
            }
            var existingVehicleType = await gdiDbContex.VehicleTypes.FirstOrDefaultAsync(x => x.VehicleTypeID == id);
            if (existingVehicleType != null)
            {
                List<Vehicle> vehicleWithTypeID = await gdiDbContex.Vehicles.Where(v => v.VehicleTypeID.Equals(id)).ToListAsync();
                foreach (var vehicle in vehicleWithTypeID)
                {
                    var existingVehicle = await gdiDbContex.Vehicles.FirstOrDefaultAsync(x => x.VehicleID == vehicle.VehicleID);
                    if (existingVehicle != null)
                    {
                        existingVehicle.VehicleTypeID = 1;
                        await gdiDbContex.SaveChangesAsync();
                    }
                }
                gdiDbContex.Remove(existingVehicleType);
                await gdiDbContex.SaveChangesAsync();
                return Ok(existingVehicleType);
            }

            return NotFound("Vehicle type doesn't exist!");
        }
    }
}
