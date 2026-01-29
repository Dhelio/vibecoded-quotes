using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AntigravityQuotes.Data;
using AntigravityQuotes.Models;

namespace AntigravityQuotes.Pages;

public class IndexModel : PageModel
{
    private readonly QuotesContext _context;

    public IndexModel(QuotesContext context)
    {
        _context = context;
    }

    public IList<Quote> Quotes { get;set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Quotes != null)
        {
            Quotes = await _context.Quotes
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }
    }
}
