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
            var query = (from p in _context.Persons.ToList()
                         join s in _context.PersonRelations.ToList() on p.PersonId equals s.PersonId into gj1
                         join s in _context.PersonRelations.ToList() on p.PersonId equals s.RelatedPersonId into gj2
                         let g = gj1.FirstOrDefault()?.RelatedPersonId ?? gj2.FirstOrDefault()?.PersonId
                         select new P
                         {
                             value = p.PersonId,
                             text = p.FullName + " " + p.Nickname + " " + p.DoB.ToString("yyyy-MM-dd"),
                             spouseValue = g,
                             spouseText = ""
                         }).ToList();

            return new JsonResult(query.Concat(new[] { new P { value = -1, text = "None", spouseValue = -1, spouseText = "None" } }));
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
                Dates = $"{p.DoB:yyyy-MM-dd} - {(p.DoD.HasValue ? p.DoD.Value.ToString("yyyy-MM-dd") : "")}",
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
            public string Dates { get; set; }
            public IEnumerable<Branch> Children { get; set; }
        }

        public class P
        {
            public int value { get; set; }
            public string text { get; set; }
            public int? spouseValue { get; set; }
            public string? spouseText { get; set; }
        }

    }
}
