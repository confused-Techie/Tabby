using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tabby_Docker.Models;

namespace Tabby_Docker.Pages.Bookmarks
{
    public class IndexModel : PageModel
    {
        private readonly Tabby_Docker.Data.Tabby_DockerContext _context;

        public IndexModel(Tabby_Docker.Data.Tabby_DockerContext context)
        {
            _context = context;
        }

        public IList<Bookmark> Bookmark { get; set; }

        public async Task OnGetAsync()
        {
            Bookmark = await _context.Bookmark.ToListAsync();
        }
    }
}
