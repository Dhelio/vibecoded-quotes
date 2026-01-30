using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AntigravityQuotes.Data;
using AntigravityQuotes.Models;

namespace AntigravityQuotes.Pages;

[Authorize]
public class EditModel : PageModel
{
    private readonly QuotesContext _context;

    public EditModel(QuotesContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Quote Quote { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Quotes == null)
        {
            return NotFound();
        }

        var quote = await _context.Quotes.FirstOrDefaultAsync(m => m.Id == id);
        if (quote == null)
        {
            return NotFound();
        }
        Quote = quote;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var existingQuote = await _context.Quotes.FindAsync(Quote.Id);
        if (existingQuote == null)
        {
            return NotFound();
        }

        // Update fields individually
        existingQuote.Text = Quote.Text;
        existingQuote.Author = Quote.Author;
        existingQuote.Source = Quote.Source;
        existingQuote.Tags = Quote.Tags;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!QuoteExists(Quote.Id))
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

    private bool QuoteExists(int id)
    {
        return (_context.Quotes?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
