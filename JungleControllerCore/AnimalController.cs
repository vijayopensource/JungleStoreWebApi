using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JungleEntities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JungleControllers.Controllers
{
    [Produces("application/json")]
    [Route("api/Animal")]
    public class AnimalController : Controller
    {
        private readonly JungleContext _context;

        public AnimalController(JungleContext context)
        {
            _context = context;
        }

        // GET: api/Animal..
        [HttpGet]
        public IEnumerable<Animal> GetAnimals()
        {
            return _context.Animals;
        }

        // GET: api/Animal/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var animal = await _context.Animals.SingleOrDefaultAsync(m => m.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }

        // PUT: api/Animal/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal([FromRoute] int id, [FromBody] Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != animal.Id)
            {
                return BadRequest();
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
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

        // POST: api/Animal
        [HttpPost]
        public async Task<IActionResult> PostAnimal([FromBody] Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);
        }

        // DELETE: api/Animal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var animal = await _context.Animals.SingleOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return Ok(animal);
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.Id == id);
        }
    }
}