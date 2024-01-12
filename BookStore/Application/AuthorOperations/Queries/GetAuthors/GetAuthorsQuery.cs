using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Application.AuthorOperations.Commands.GetAuthors;

// Query class for retrieving a list of authors in the application layer.
public class GetAuthorsQuery
{
	// Database context for interacting with the underlying data store.
	private readonly BookStoreDbContext _dbContext;

	// AutoMapper instance for mapping between Entity and ViewModel.
	private readonly IMapper _mapper;

	// Constructor: Initializes the query with the required dependencies.
	public GetAuthorsQuery(BookStoreDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	// Method: Handles the get authors query and returns a list of authors as ViewModels.
	public List<AuthorsViewModel> Handle()
	{
		// Retrieve the list of authors from the database, ordered by their IDs.
		var authorList = _dbContext.Authors.OrderBy(a => a.Id).ToList();

		// Map the list of authors to the list of AuthorsViewModel using AutoMapper.
		var viewModel = _mapper.Map<List<AuthorsViewModel>>(authorList);

		// Return the mapped ViewModel containing information about the authors.
		return viewModel;
	}

	// ViewModel class for displaying information about authors in a list.
	public class AuthorsViewModel
	{
		// Property: Name of the author.
		public string Name { get; set; }

		// Property: Surname of the author.
		public string Surname { get; set; }

		// Property: Birthday of the author (formatted as a string for simplicity).
		public string Birthday { get; set; }
	}
}
