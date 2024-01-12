using BookStore.DbOperations;

namespace BookStore.Application.BookOperations.Commands.DeleteBook;

// Command class for deleting a book in the application layer.
public class DeleteBookCommand
{
    // Database context for interacting with the underlying data store.
    private readonly BookStoreDbContext _dbContext;

    // Property: Unique identifier for the book to be deleted.
    public int BookId { get; set; }

    // Constructor: Initializes the command with the required dependencies.
    public DeleteBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Method: Handles the delete book command.
    public void Handle()
    {
        // Retrieve the book to be deleted from the database based on the provided BookId.
        var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

        // Check if the book with the provided ID exists.
        if (book is null)
        {
            throw new InvalidOperationException("No books found to delete!");
        }

        // Remove the book from the Books DbSet in the database context.
        _dbContext.Books.Remove(book);

        // Save changes to persist the deletion in the database.
        _dbContext.SaveChanges();
    }
}

