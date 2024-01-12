using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor;

// Command class for updating an existing author in the application layer.
public class UpdateAuthorCommand
{
	// Property: Unique identifier for the author to be updated.
	public int AuthorId { get; set; }

	// Property: ViewModel containing updated information for the author.
	public UpdateAuthorViewModel Model { get; set; }

	// Database context for interacting with the underlying data store.
	private readonly BookStoreDbContext _dbContext;

	// AutoMapper instance for mapping between ViewModel and Entity.
	private readonly IMapper _mapper;

	// Constructor: Initializes the command with the required dependencies.
	public UpdateAuthorCommand(BookStoreDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	// Method: Handles the update author command.
	public void Handle()
	{
		// Retrieve the author to be updated from the database based on the provided AuthorId.
		var author = _dbContext.Authors.SingleOrDefault(a => a.Id == AuthorId);

		// Check if the author with the provided ID exists.
		if (author is null)
			throw new InvalidOperationException("ID is not correct.");

		// Map the updated data from the ViewModel to the existing Author entity using AutoMapper.
		_mapper.Map(Model, author);

		// Save changes to persist the updated author information in the database.
		_dbContext.SaveChanges();
	}

	// ViewModel class for updating an existing author.
	public class UpdateAuthorViewModel
	{
		// Property: Updated name of the author.
		public string Name { get; set; }

		// Property: Updated surname of the author.
		public string Surname { get; set; }

		// Property: Updated birthday of the author.
		public DateTime Birthday { get; set; }
	}
}

