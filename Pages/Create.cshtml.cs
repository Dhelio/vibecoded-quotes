using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AntigravityQuotes.Data;
using AntigravityQuotes.Models;

using Microsoft.AspNetCore.Authorization;

namespace AntigravityQuotes.Pages;

[Authorize]
public class CreateModel : PageModel
{
    private readonly QuotesContext _context;

    public CreateModel(QuotesContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Quote Quote { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || _context.Quotes == null || Quote == null)
        {
            return Page();
        }

        Quote.CreatedAt = DateTime.UtcNow; // Ensure correct time
        _context.Quotes.Add(Quote);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
