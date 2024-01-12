using BookStore.DbOperations;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre;
// Command class for updating an existing genre in the application layer.
public class UpdateGenreCommand
{
    // Database context for interacting with the underlying data store.
    private readonly BookStoreDbContext _dbContext;

    // Property: Unique identifier for the genre to be updated.
    public int GenreId { get; set; }

    // Property: Model containing updated information for the genre.
    public UpdateGenreModel Model { get; set; }

    // Constructor: Initializes the command with the required dependencies.
    public UpdateGenreCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Method: Handles the update genre command.
    public void Handle()
    {
        // Retrieve the genre to be updated from the database based on the provided GenreId.
        var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);

        // Check if the genre with the provided ID exists.
        if (genre is null)
        {
            throw new InvalidOperationException("No genre found to update!");
        }

        // Check if there is already a genre with the same name (case-insensitive) other than the one being updated.
        if (_dbContext.Genres.Any(x =>
            string.Equals(x.Name.ToLower(), Model.Name.ToLower(), StringComparison.Ordinal) && x.Id != GenreId))
        {
            throw new InvalidOperationException("There is already a genre with the same name.");
        }

        // Update the genre's Name property if the updated model's Name is not null or empty; otherwise, use the existing Name.
        genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;

        // Update the genre's IsActive property based on the updated model.
        genre.IsActive = Model.IsActive;

        // Save changes to persist the updated genre information in the database.
        _dbContext.SaveChanges();
    }
}

// Model class for updating an existing genre.
public class UpdateGenreModel
{
    // Property: Updated name of the genre.
    public string Name { get; set; }

    // Property: Updated IsActive status of the genre (default is true).
    public bool IsActive { get; set; } = true;
}

