using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Service.Abstract;

namespace SH1ProjeUygulamasi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IService<ProductImage> _service;

        public ProductImagesController(IService<ProductImage> service)
        {
            _service = service;
        }

        // GET: api/<ProductImagesController>
        [HttpGet]
        public async Task<IEnumerable<ProductImage>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<ProductImagesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImage>> GetAsync(int id)
        {
            var model = await _service.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpGet("GetProductImagesByProductId/{id}")]
        public async Task<IEnumerable<ProductImage>> GetProductImagesByProductId(int id)
        {
            return await _service.GetAllAsync(p => p.ProductId == id);
        }

        // POST api/<ProductImagesController>
        [HttpPost]
        public async Task<ActionResult<ProductImage>> PostAsync([FromBody] ProductImage value)
        {
            await _service.AddAsync(value);
            await _service.SaveChangesAsync();

            return Ok(value);
        }

        // PUT api/<ProductImagesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductImage>> PutAsync(int id, [FromBody] ProductImage value)
        {
            _service.Update(value);
            await _service.SaveChangesAsync();
            return Ok(value);
        }

        // DELETE api/<ProductImagesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _service.Delete(model);
            await _service.SaveChangesAsync();
            return Ok();
        }
    }
}
