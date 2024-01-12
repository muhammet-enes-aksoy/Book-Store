using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor;
// Command class for creating a new author in the application layer.
public class CreateAuthorCommand
{
	// Property: ViewModel containing information for creating a new author.
	public CreateAuthorViewModel Model { get; set; }

	// Database context for interacting with the underlying data store.
	private readonly BookStoreDbContext _dbContext;

	// AutoMapper instance for mapping between ViewModel and Entity.
	private readonly IMapper _mapper;

	// Constructor: Initializes the command with the required dependencies.
	public CreateAuthorCommand(BookStoreDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	// Method: Handles the create author command.
	public void Handle()
	{
		// Check if an author with the same name already exists in the database.
		var author = _dbContext.Authors.SingleOrDefault(a => a.Name == Model.Name);

		// If an author with the same name is found, raise an exception.
		if (author is not null)
			throw new InvalidOperationException("Author is already added.");

		// Map the data from the ViewModel to the Author entity using AutoMapper.
		author = _mapper.Map<Author>(Model);

		// Add the new author to the Authors DbSet in the database context.
		_dbContext.Authors.Add(author);

		// Save changes to persist the new author in the database.
		_dbContext.SaveChanges();
	}

	// ViewModel class for creating a new author.
	public class CreateAuthorViewModel
	{
		// Property: Name of the author.
		public string Name { get; set; }

		// Property: Surname of the author.
		public string Surname { get; set; }

		// Property: Birthday of the author.
		public DateTime Birthday { get; set; }
	}
}

