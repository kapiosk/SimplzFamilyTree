using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimplzFamilyTree.Data;

namespace SimplzFamilyTree.Pages.Persons
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Person Person { get; set; }

        [BindProperty]
        public PersonRelation Father { get; set; }
        public string FatherName { get; set; }

        [BindProperty]
        public PersonRelation Mother { get; set; }
        public string MotherName { get; set; }

        [BindProperty]
        public PersonRelation Spouse { get; set; }
        public string SpouseName { get; set; }

        [BindProperty]
        public PersonEvent PersonEvent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person = await _context.Persons.FirstOrDefaultAsync(m => m.PersonId == id);

            var relation = from person in _context.Persons
                          where person.PersonId == id
                          join personRelation in _context.PersonRelations on person.PersonId equals personRelation.PersonId
                          join parent in _context.Persons on personRelation.RelatedPersonId equals parent.PersonId
                          select new { personRelation, parent };

            Father = await relation.Where(p => p.personRelation.Relation == Relation.ParentY)
                                  .Select(p => p.personRelation)
                                  .FirstOrDefaultAsync() ?? new PersonRelation { Relation = Relation.ParentY };

            FatherName = await relation.Where(p => p.personRelation.Relation == Relation.ParentY)
                                      .Select(p => p.parent.FullName)
                                      .FirstOrDefaultAsync() ?? "Select Father";

            Mother = await relation.Where(p => p.personRelation.Relation == Relation.ParentX)
                                  .Select(p => p.personRelation)
                                  .FirstOrDefaultAsync() ?? new PersonRelation { Relation = Relation.ParentX };

            MotherName = await relation.Where(p => p.personRelation.Relation == Relation.ParentX)
                                      .Select(p => p.parent.FullName)
                                      .FirstOrDefaultAsync() ?? "Select Mother";

            Spouse = await relation.Where(p => p.personRelation.Relation == Relation.Spouse)
                      .Select(p => p.personRelation)
                      .FirstOrDefaultAsync() ?? new PersonRelation { Relation = Relation.Spouse };

            SpouseName = await relation.Where(p => p.personRelation.Relation == Relation.Spouse)
                                      .Select(p => p.parent.FullName)
                                      .FirstOrDefaultAsync() ?? "Select Spouse";

            PersonEvent = await (from personEvent in _context.PersonEvents
                                 where personEvent.PersonId == id
                                 select personEvent).FirstOrDefaultAsync();

            if (Person == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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
                    _context.Attach(Person).State = EntityState.Modified;

                    foreach (var parent in new[] { Father, Mother, Spouse })
                    {
                        parent.PersonId = Person.PersonId;
                        if (parent.RelatedPersonId == -1)
                        {
                            if (parent.PersonRelationId > 0)
                                _context.Attach(parent).State = EntityState.Deleted;
                        }
                        else if (parent.RelatedPersonId > 0)
                        {
                            if (parent.PersonRelationId > 0)
                                _context.Attach(parent).State = EntityState.Modified;
                            else
                                _context.Attach(parent).State = EntityState.Added;
                        }
                    }

                    if (PersonEvent.PersonEventId > 0)
                    {
                        _context.Attach(PersonEvent).State = EntityState.Modified;
                    }
                    else if (!string.IsNullOrEmpty(PersonEvent.Description))
                    {
                        PersonEvent.PersonId = Person.PersonId;
                        _context.Attach(PersonEvent).State = EntityState.Added;
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
