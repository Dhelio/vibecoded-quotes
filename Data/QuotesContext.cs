using AntigravityQuotes.Models;
using Microsoft.EntityFrameworkCore;

namespace AntigravityQuotes.Data;

public class QuotesContext : DbContext
{
    public QuotesContext(DbContextOptions<QuotesContext> options)
        : base(options)
    {
    }

    public DbSet<Quote> Quotes { get; set; }
}
