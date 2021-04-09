using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SimplzFamilyTree.Data;

namespace SimplzFamilyTree.Pages.PersonRelations
{
    public class DeleteModel : PageModel
    {
        private readonly SimplzFamilyTree.Data.ApplicationDbContext _context;

        public DeleteModel(SimplzFamilyTree.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PersonRelation PersonRelation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PersonRelation = await _context.PersonRelations.FirstOrDefaultAsync(m => m.PersonRelationId == id);

            if (PersonRelation == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PersonRelation = await _context.PersonRelations.FindAsync(id);

            if (PersonRelation != null)
            {
                _context.PersonRelations.Remove(PersonRelation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
