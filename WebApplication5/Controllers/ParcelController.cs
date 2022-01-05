using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Database;
using WebApplication5.Database.Entities;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParcelController : ControllerBase
    {

        private readonly ApiDbContext _apiDbContext;

        public ParcelController(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        [HttpGet]
        public IEnumerable<Parcel> GetParcels()
        {
            return _apiDbContext.Parcels.AsEnumerable();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Parcel>> GetParcel(int id)
        {
            var parcel = await _apiDbContext.Parcels.FindAsync(id);

            if (parcel != null) return parcel;
            
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Parcel>> PostParcel(Parcel parcel)
        {
            parcel.RegisteredDate = DateTimeOffset.UtcNow;
            await _apiDbContext.Parcels.AddAsync(parcel);
            await _apiDbContext.SaveChangesAsync();

            return CreatedAtAction("PostParcel", new { id = parcel.Id }, parcel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutParcel(int id, Parcel parcel)
        {
            if (id != parcel.Id) return BadRequest();

            var existingParcel = await _apiDbContext.Parcels.FindAsync(id);
            if (existingParcel == null) return NotFound();
            _apiDbContext.Entry<Parcel>(existingParcel).State = EntityState.Detached;
            _apiDbContext.Entry<Parcel>(parcel).State = EntityState.Modified;

            await _apiDbContext.SaveChangesAsync();
     
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteParcel(int id)
        {
            var parcel = await _apiDbContext.Parcels.FindAsync(id);

            if (parcel == null) return NotFound();

            _apiDbContext.Remove(parcel);
            await _apiDbContext.SaveChangesAsync();

            return NoContent();

        }
    }
}
