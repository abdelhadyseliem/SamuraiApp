using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamuraisController : ControllerBase
    {
        private readonly SamuraiContext _context;

        public SamuraisController(SamuraiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSamurais()
        {
            var samurais = await _context.Samurais.ToListAsync();

            if (samurais.Count == 0)
            {
                return NotFound();
            }

            return Ok(samurais);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSamuraiById(int? id)
        {
            if (id == null || _context.Samurais == null)
            {
                return NotFound();
            }

            var samurai = await _context.Samurais
                .FirstOrDefaultAsync(m => m.Id == id);
            if (samurai == null)
            {
                return NotFound();
            }

            return Ok(samurai);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSamurai(int id, Samurai samurai)
        {
            if (id != samurai.Id)
            {
                return BadRequest();
            }

            _context.Entry(samurai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (!SamuraiExists(id))
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
        public async Task<IActionResult> CreateSamurai(Samurai samurai)
        {
            _context.Add(samurai);

            await _context.SaveChangesAsync();

            return Ok(samurai);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSamurai(int? id)
        {
            if (id == null || _context.Samurais == null)
            {
                return NotFound();
            }

            var samurai = await _context.Samurais
                .FirstOrDefaultAsync(m => m.Id == id);

            if (samurai == null)
            {
                return NotFound();
            }

            _context.Samurais.Remove(samurai);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SamuraiExists(int id)
        {
            return (_context.Samurais?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
