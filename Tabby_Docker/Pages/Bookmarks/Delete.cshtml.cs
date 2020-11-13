using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tabby_Docker.Models;

namespace Tabby_Docker.Pages.Bookmarks
{
    public class DeleteModel : PageModel
    {
        private readonly Tabby_Docker.Data.Tabby_DockerContext _context;

        public DeleteModel(Tabby_Docker.Data.Tabby_DockerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bookmark Bookmark { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bookmark = await _context.Bookmark.FirstOrDefaultAsync(m => m.ID == id);

            if (Bookmark == null)
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

            Bookmark = await _context.Bookmark.FindAsync(id);

            if (Bookmark != null)
            {
                _context.Bookmark.Remove(Bookmark);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
