using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LwdzsController : ControllerBase
    {
        private readonly MyContext _context;

        public LwdzsController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Lwdzs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LwdzDTO>>> GetLwdzs()
        {
            return await _context.Lwdzs
                .Select(x =>LwdzToDTO(x)).
                ToListAsync();
        }

        // GET: api/Lwdzs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LwdzDTO>> GetLwdz(long id)
        {
            var lwdz = await _context.Lwdzs.FindAsync(id);

            if (lwdz == null)
            {
                return NotFound();
            }

            return LwdzToDTO(lwdz);
        }

        // PUT: api/Lwdzs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLwdz(long id, LwdzDTO lwdzDTO)
        {
            if (id != lwdzDTO.ID)
            {
                return BadRequest();
            }

            var lwdz = await _context.Lwdzs.FindAsync(id);
            if(lwdz == null)
            {
                return NotFound();
            }

            lwdz.Name = lwdzDTO.Name;
            lwdz.Pwd = lwdzDTO.Pwd;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) when(!LwdzExists(id))
            {
                return NotFound();
            }

            //_context.Entry(lwdzDTO).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!LwdzExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/Lwdzs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LwdzDTO>> PostLwdz(LwdzDTO lwdzDTO)
        {
            var lwdz = new Lwdz
            {
                Name = lwdzDTO.Name,
                Pwd = lwdzDTO.Pwd
            };
            _context.Lwdzs.Add(lwdz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLwdz", new { id = lwdz.ID }, LwdzToDTO(lwdz));
        }

        // DELETE: api/Lwdzs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLwdz(long id)
        {
            var lwdz = await _context.Lwdzs.FindAsync(id);
            if (lwdz == null)
            {
                return NotFound();
            }

            _context.Lwdzs.Remove(lwdz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LwdzExists(long id)
        {
            return _context.Lwdzs.Any(e => e.ID == id);
        }


        private static LwdzDTO LwdzToDTO(Lwdz lwdz)=>
           new LwdzDTO
            {
                ID = lwdz.ID,
                Name = lwdz.Name,
                Pwd = lwdz.Pwd
            };
        
    }
}
