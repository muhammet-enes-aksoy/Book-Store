using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Application.AuthorOperations.Commands.GetAuthorDetail;
// Query class for retrieving detailed information about an author in the application layer.
public class GetAuthorDetailQuery
{
	// Property: Unique identifier for the author whose details are to be retrieved.
	public int AuthorId { get; set; }

	// Database context for interacting with the underlying data store.
	private readonly BookStoreDbContext _dbContext;

	// AutoMapper instance for mapping between Entity and ViewModel.
	private readonly IMapper _mapper;

	// Constructor: Initializes the query with the required dependencies.
	public GetAuthorDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	// Method: Handles the get author detail query and returns the detailed information as a ViewModel.
	public AuthorDetailViewModel Handle()
	{
		// Retrieve the author from the database based on the provided AuthorId.
		var author = _dbContext.Authors.SingleOrDefault(a => a.Id == AuthorId);

		// Check if the author with the provided ID exists.
		if (author is null)
			throw new InvalidOperationException("ID is not correct!");

		// Map the Author entity to the AuthorDetailViewModel using AutoMapper.
		AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);

		// Return the mapped ViewModel containing detailed information about the author.
		return vm;
	}

	// ViewModel class for displaying detailed information about an author.
	public class AuthorDetailViewModel
	{
		// Property: Name of the author.
		public string Name { get; set; }

		// Property: Surname of the author.
		public string Surname { get; set; }

		// Property: Birthday of the author (formatted as a string for simplicity).
		public string Birthday { get; set; }
	}
}
