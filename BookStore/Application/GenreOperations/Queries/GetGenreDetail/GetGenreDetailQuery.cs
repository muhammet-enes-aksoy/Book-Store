using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Application.GenreOperations.Queries.GetGenreDetail;
// Query class for retrieving detailed information about a genre in the application layer.
public class GetGenreDetailQuery
{
    // Property: Unique identifier for the genre whose details are to be retrieved.
    public int GenreId { get; set; }

    // Database context for interacting with the underlying data store.
    public readonly BookStoreDbContext _context;

    // AutoMapper instance for mapping between Entity and ViewModel.
    public readonly IMapper _mapper;

    // Constructor: Initializes the query with the required dependencies.
    public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // Method: Handles the get genre detail query and returns detailed information as a ViewModel.
    public GenreDetailViewModel Handle()
    {
        // Retrieve the active genre with the specified ID from the database.
        var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);

        // Check if the genre with the provided ID exists and is active.
        if (genre is null)
        {
            throw new InvalidOperationException("Genre not found or is inactive.");
        }

        // Map the Genre entity to the GenreDetailViewModel using AutoMapper.
        return _mapper.Map<GenreDetailViewModel>(genre);
    }
}

// ViewModel class for displaying detailed information about a genre.
public class GenreDetailViewModel
{
    // Property: Unique identifier for the genre.
    public int Id { get; set; }

    // Property: Name of the genre.
    public string Name { get; set; }
}

