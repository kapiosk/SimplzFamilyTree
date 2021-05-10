using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimplzFamilyTree.Pages
{
    public class TreeModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int PersonId { get; set; }

        [BindProperty]
        public string Name { get; set; }

        private readonly Data.ApplicationDbContext _context;

        public TreeModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var p = _context.Persons.FirstOrDefault(p=>p.PersonId==PersonId);
            Name = p?.FullName;
            return Page();
        }
    }
}
