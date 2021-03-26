using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimplzFamilyTree.Data;

namespace SimplzFamilyTree.Pages.PersonImages
{
    public class EditModel : PageModel
    {
        private readonly SimplzFamilyTree.Data.ApplicationDbContext _context;

        public EditModel(SimplzFamilyTree.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PersonImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonImageExists(PersonImage.PersonImageId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PersonImageExists(int id)
        {
            return _context.PersonImages.Any(e => e.PersonImageId == id);
        }
    }
}
