using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Data;

namespace SH1ProjeUygulamasi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public BrandsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        // PUT: api/Brands/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
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

        // POST: api/Brands
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrand", new { id = brand.Id }, brand);
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}
