using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Tabby_Docker.Models;

namespace Tabby_Docker.Pages.Bookmarks
{
    public class CreateModel : PageModel
    {
        private readonly Tabby_Docker.Data.Tabby_DockerContext _context;

        public CreateModel(Tabby_Docker.Data.Tabby_DockerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Bookmark Bookmark { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bookmark.Add(Bookmark);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
