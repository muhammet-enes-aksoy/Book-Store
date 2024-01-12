using Microsoft.EntityFrameworkCore;
using BookStore.Entities;

namespace BookStore.DbOperations;

// DbContextOptions<BookStoreDbContext> is injected to provide configuration options.
public class BookStoreDbContext : DbContext
{
    // Constructor: Initializes the DbContext with the provided options.
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {
    }

    // DbSet<Book>: Represents the collection of Book entities in the database.
    public DbSet<Book> Books { get; set; }

    // DbSet<Genre>: Represents the collection of Genre entities in the database.
    public DbSet<Genre> Genres { get; set; }

    // DbSet<Author>: Represents the collection of Author entities in the database.
    public DbSet<Author> Authors { get; set; }
}

