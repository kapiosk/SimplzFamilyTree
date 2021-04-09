using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplzFamilyTree.Data;

namespace SimplzFamilyTree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet]
        public JsonResult GetPersons()
        {
            return new JsonResult(_context.Persons.Select(p => new
            {
                value = p.PersonId,
                text = p.FullName + " " + p.Nickname + " " + p.DoB.ToString("yyyy-MM-dd")
            }).ToList().Concat(new[] { new { value = 0, text = "None" } }));
        }

        [HttpGet("{inp}")]
        public async Task<IActionResult> GetPerson(string inp)
        {
            var person = await _context.Persons
                                       .Where(p => p.FullName.ToLower().Contains(inp.ToLower()))
                                       .Select(p => new
                                       {
                                           value = p.PersonId,
                                           text = p.FullName + " " + p.Nickname + " " + p.DoB.ToString("yyyy-MM-dd")
                                       })
                                       .FirstOrDefaultAsync();

            if (person == null)
            {
                return NotFound();
            }

            return new JsonResult(person);
        }

        //    // PUT: api/People/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutPerson(int id, Person person)
        //    {
        //        if (id != person.PersonId)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(person).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PersonExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        //    // POST: api/People
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<Person>> PostPerson(Person person)
        //    {
        //        _context.Persons.Add(person);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetPerson", new { id = person.PersonId }, person);
        //    }

        //    // DELETE: api/People/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeletePerson(int id)
        //    {
        //        var person = await _context.Persons.FindAsync(id);
        //        if (person == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Persons.Remove(person);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool PersonExists(int id)
        //    {
        //        return _context.Persons.Any(e => e.PersonId == id);
        //    }
    }
}
