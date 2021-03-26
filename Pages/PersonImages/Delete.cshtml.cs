using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SimplzFamilyTree.Data;

namespace SimplzFamilyTree.Pages.PersonImages
{
    public class DeleteModel : PageModel
    {
        private readonly SimplzFamilyTree.Data.ApplicationDbContext _context;

        public DeleteModel(SimplzFamilyTree.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PersonImage PersonImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PersonImage = await _context.PersonImages.FirstOrDefaultAsync(m => m.PersonImageId == id);

            if (PersonImage == null)
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

            PersonImage = await _context.PersonImages.FindAsync(id);

            if (PersonImage != null)
            {
                _context.PersonImages.Remove(PersonImage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
