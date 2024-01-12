using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Application.GenreOperations.Commands.CreateGenre;

// Command class for creating a new genre in the application layer.
public class CreateGenreCommand
{
    // Property: Model containing information for creating a new genre.
    public CreateGenreModel Model { get; set; }

    // Database context for interacting with the underlying data store.
    public readonly BookStoreDbContext _context;

    // AutoMapper instance for mapping between Model and Entity.
    public readonly IMapper _mapper;

    // Constructor: Initializes the command with the required dependencies.
    public CreateGenreCommand(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // Method: Handles the create genre command.
    public void Handle()
    {
        // Check if a genre with the same name already exists in the database.
        var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);

        // If a genre with the same name is found, raise an exception.
        if (genre is not null)
        {
            throw new InvalidOperationException("Genre already exists!");
        }

        // Add the new genre to the Genres DbSet in the database context.
        _context.Genres.Add(new Genre { Name = Model.Name });

        // Save changes to persist the new genre in the database.
        _context.SaveChanges();
    }
}

// Model class for creating a new genre.
public class CreateGenreModel
{
    // Property: Name of the genre.
    public string Name { get; set; }
}

