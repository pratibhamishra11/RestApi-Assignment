using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Data;
using RestApi.Models;
using System.Reflection.Metadata.Ecma335;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeveragesC : ControllerBase

    {
        private readonly ApplicationDbContext _context;
        public BeveragesC(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beverages>>> GetWeather()
        {
            return await _context.BeveragesL.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Beverages>> GetWeather(long id)
        {
            var weather = await _context.BeveragesL.FindAsync(id);

            if (weather == null)
            {
                return NotFound();
            }

            return weather;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeather(long id, Beverages bv)
        {
            if (id != bv.Id)
            {
                return BadRequest();
            }

            _context.Entry(bv).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeveragesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Beverages>> PostBeverages(Beverages beverages)
        {
            _context.BeveragesL.Add(beverages);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostBeverages), new { id = beverages.Id }, beverages);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeather(long id)
        {
            var weather = await _context.BeveragesL.FindAsync(id);
            if (weather == null)
            {
                return NotFound();
            }

            _context.BeveragesL.Remove(weather);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeveragesExists(long id)
        {
            return _context.BeveragesL.Any(equals => equals.Id == id);
        }
    }
}
