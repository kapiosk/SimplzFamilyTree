using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimplzFamilyTree.Data;

namespace SimplzFamilyTree.Pages.Persons
{
    public class CreateModel : PageModel
    {
        private readonly SimplzFamilyTree.Data.ApplicationDbContext _context;

        public CreateModel(SimplzFamilyTree.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Person Person { get; set; }

        [BindProperty]
        public PersonRelation Father { get; set; }

        [BindProperty]
        public PersonRelation Mother { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Persons.Add(Person);
                    await _context.SaveChangesAsync();

                    if (Father.RelatedPersonId > 0)
                    {
                        Father.Relation = Relation.ParentY;
                        Father.PersonId = Person.PersonId;
                        _context.PersonRelations.Add(Father);
                    }

                    if (Mother.RelatedPersonId > 0)
                    {
                        Mother.Relation = Relation.ParentX;
                        Mother.PersonId = Person.PersonId;
                        _context.PersonRelations.Add(Mother);
                    }

                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }



            return RedirectToPage("./Index");
        }
    }
}
