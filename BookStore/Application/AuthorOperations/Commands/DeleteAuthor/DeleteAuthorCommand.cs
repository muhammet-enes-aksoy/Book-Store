using BookStore.DbOperations;

namespace BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
// Command class for deleting an author in the application layer.
public class DeleteAuthorCommand
{
	// Property: Unique identifier for the author to be deleted.
	public int AuthorId { get; set; }

	// Database context for interacting with the underlying data store.
	private readonly BookStoreDbContext _dbContext;

	// Constructor: Initializes the command with the required dependencies.
	public DeleteAuthorCommand(BookStoreDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	// Method: Handles the delete author command.
	public void Handle()
	{
		// Retrieve the author to be deleted from the database based on the provided AuthorId.
		var author = _dbContext.Authors.SingleOrDefault(a => a.Id == AuthorId);

		// Retrieve any books associated with the author to be deleted.
		var authorBooks = _dbContext.Books.SingleOrDefault(a => a.AuthorId == AuthorId);

		// Check if the author with the provided ID exists.
		if (author is null)
			throw new InvalidOperationException("ID isn't found.");

		// Check if the author has any published books.
		if (authorBooks is not null)
			throw new InvalidOperationException(author.Name + " " + author.Surname + " has a published book. Please delete the book first.");

		// Remove the author from the Authors DbSet in the database context.
		_dbContext.Authors.Remove(author);

		// Save changes to persist the deletion in the database.
		_dbContext.SaveChanges();
	}
}

