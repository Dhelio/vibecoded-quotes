using Microsoft.AspNetCore.Mvc;
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

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    public IList<Quote> Quotes { get;set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Quotes != null)
        {
            var quotes = _context.Quotes.AsQueryable();

            if (!string.IsNullOrEmpty(SearchString))
            {
                // Check if searching by tag with "tag:" prefix
                if (SearchString.StartsWith("tag:", StringComparison.OrdinalIgnoreCase))
                {
                    var tagSearch = SearchString.Substring(4).Trim();
                    if (!string.IsNullOrEmpty(tagSearch))
                    {
                        quotes = quotes.Where(s => s.Tags != null && s.Tags.Contains(tagSearch));
                    }
                }
                else
                {
                    quotes = quotes.Where(s => 
                        s.Text.Contains(SearchString) || 
                        s.Author.Contains(SearchString) ||
                        (s.Source != null && s.Source.Contains(SearchString)) ||
                        (s.Tags != null && s.Tags.Contains(SearchString)));
                }
            }

            Quotes = await quotes
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }
    }
}
