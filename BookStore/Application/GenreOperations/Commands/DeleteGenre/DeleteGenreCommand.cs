using BookStore.DbOperations;

namespace BookStore.Application.GenreOperations.Commands.DeleteGenre;
// Command class for deleting a genre in the application layer.
public class DeleteGenreCommand
{
    // Database context for interacting with the underlying data store.
    private readonly BookStoreDbContext _dbContext;

    // Property: Unique identifier for the genre to be deleted.
    public int GenreId { get; set; }

    // Constructor: Initializes the command with the required dependencies.
    public DeleteGenreCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Method: Handles the delete genre command.
    public void Handle()
    {
        // Retrieve the genre to be deleted from the database based on the provided GenreId.
        var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);

        // Check if the genre with the provided ID exists.
        if (genre is null)
        {
            throw new InvalidOperationException("No genre found to delete!");
        }

        // Remove the genre from the Genres DbSet in the database context.
        _dbContext.Genres.Remove(genre);

        // Save changes to persist the deletion in the database.
        _dbContext.SaveChanges();
    }
}

