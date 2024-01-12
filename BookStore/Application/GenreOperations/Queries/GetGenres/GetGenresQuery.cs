using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Application.GenreOperations.Queries.GetGenres;
// Query class for retrieving a list of active genres in the application layer.
public class GetGenresQuery
{
    // Database context for interacting with the underlying data store.
    public readonly BookStoreDbContext _context;

    // AutoMapper instance for mapping between Entity and ViewModel.
    public readonly IMapper _mapper;

    // Constructor: Initializes the query with the required dependencies.
    public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // Method: Handles the get genres query and returns a list of active genres as ViewModels.
    public List<GenresViewModel> Handle()
    {
        // Retrieve active genres from the database, ordered by ID.
        var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);

        // Map the list of Genre entities to a list of GenresViewModel using AutoMapper.
        var returnObj = _mapper.Map<List<GenresViewModel>>(genres);

        // Return the list of active genres as ViewModels.
        return returnObj;
    }
}

// ViewModel class for displaying information about a genre in a list.
public class GenresViewModel
{
    // Property: Unique identifier for the genre.
    public int Id { get; set; }

    // Property: Name of the genre.
    public string Name { get; set; }
}

