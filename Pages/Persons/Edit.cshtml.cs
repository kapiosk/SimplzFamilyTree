using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SimplzFamilyTree.Data;
using static System.Net.WebRequestMethods;

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

        [BindProperty]
        public string Image { get; set; }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person = await _context.Persons.FirstOrDefaultAsync(m => m.PersonId == id);

            if (Person == null)
            {
                return NotFound();
            }

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

            PersonImage personImage = _context.PersonImages.FirstOrDefault(pi => pi.PersonId == Person.PersonId);

            if (personImage != null)
            {
                Image = Convert.ToBase64String(personImage.Image);
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

                    if (Upload?.FileName != null)
                    {
                        using var reader = new StreamReader(Upload.OpenReadStream());
                        using var ms = new MemoryStream();
                        await reader.BaseStream.CopyToAsync(ms);

                        PersonImage personImage = _context.PersonImages.FirstOrDefault(pi => pi.PersonId == Person.PersonId);

                        if (personImage is null)
                        {
                            personImage = new();
                            personImage.PersonId = Person.PersonId;
                            personImage.Name = "";
                            personImage.Image = ms.ToArray();
                            _context.Attach(personImage).State = EntityState.Added;
                        }
                        else
                        {
                            personImage.Image = ms.ToArray();
                            _context.Attach(personImage).State = EntityState.Modified;
                        }

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
