using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            }).ToList().Concat(new[] { new { value = -1, text = "None" } }));
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

        [HttpGet("tree/{id}")]
        public async Task<JsonResult> GetTree(int Id)
        {
            var p = await _context.Persons.FirstOrDefaultAsync(p => p.PersonId == Id);
            return new JsonResult(GetBranch(p, _context));
        }

        public static Branch GetBranch(Person p, ApplicationDbContext context)
        {
            return new Branch
            {
                Id = p.PersonId,
                Name = p.FullName,
                Children = from personRelation in context.PersonRelations
                           where personRelation.RelatedPersonId == p.PersonId
                           join child in context.Persons on personRelation.PersonId equals child.PersonId
                           select GetBranch(child, context)
            };
        }

        public class Branch
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public IEnumerable<Branch> Children { get; set; }
        }

    }
}
