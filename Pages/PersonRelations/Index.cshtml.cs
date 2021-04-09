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
    public class IndexModel : PageModel
    {
        private readonly SimplzFamilyTree.Data.ApplicationDbContext _context;

        public IndexModel(SimplzFamilyTree.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<PersonRelation> PersonRelation { get;set; }

        public async Task OnGetAsync()
        {
            PersonRelation = await _context.PersonRelations.ToListAsync();
        }
    }
}
