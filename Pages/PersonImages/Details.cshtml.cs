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
    public class DetailsModel : PageModel
    {
        private readonly SimplzFamilyTree.Data.ApplicationDbContext _context;

        public DetailsModel(SimplzFamilyTree.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
