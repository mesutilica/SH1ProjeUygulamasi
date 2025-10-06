using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Service.Abstract;

namespace SH1ProjeUygulamasi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController : ControllerBase
    {
        private readonly IService<Slider> _service;

        public SlidersController(IService<Slider> service)
        {
            _service = service;
        }

        // GET: api/<SlidersController>
        [HttpGet]
        public async Task<IEnumerable<Slider>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<SlidersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Slider>> GetAsync(int id)
        {
            var model = await _service.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        // POST api/<SlidersController>
        [HttpPost]
        public async Task PostAsync([FromBody] Slider value)
        {
            await _service.AddAsync(value);
            await _service.SaveChangesAsync();
        }

        // PUT api/<SlidersController>/5
        [HttpPut("{id}")]
        public async Task PutAsync(int id, [FromBody] Slider value)
        {
            _service.Update(value);
            await _service.SaveChangesAsync();
        }

        // DELETE api/<SlidersController>/5
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
