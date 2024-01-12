using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

// ApiController attribute indicates that this class handles HTTP API requests.
[ApiController]

// Route attribute specifies the base route for the controller.
[Route("[controller]s")]
public class GenreController : ControllerBase
{
    // Database context for interacting with the underlying data store.
    private readonly BookStoreDbContext _context;

    // AutoMapper instance for mapping between Entity and ViewModel.
    private readonly IMapper _mapper;

    // Constructor: Initializes the controller with required dependencies.
    public GenreController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // Endpoint: Handles HTTP GET request to retrieve a list of genres.
    [HttpGet]
    public ActionResult GetGenres()
    {
        // Retrieve and return a list of genres using the GetGenresQuery.
        var query = new GetGenresQuery(_context, _mapper);
        var obj = query.Handle();
        return Ok(obj);
    }

    // Endpoint: Handles HTTP GET request to retrieve genre details by ID.
    [HttpGet("{id}")]
    public ActionResult GetGenreDetail(int id)
    {
        // Retrieve genre details by ID using the GetGenreDetailQuery.
        var query = new GetGenreDetailQuery(_context, _mapper)
        {
            GenreId = id
        };
        var validator = new GetGenreDetailQueryValidator();
        validator.ValidateAndThrow(query);
        var obj = query.Handle();
        return Ok(obj);
    }

    // Endpoint: Handles HTTP POST request to add a new genre.
    [HttpPost]
    public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
    {
        // Add a new genre using the CreateGenreCommand.
        var command = new CreateGenreCommand(_context, _mapper)
        {
            Model = newGenre
        };

        var validator = new CreateGenreCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    // Endpoint: Handles HTTP PUT request to update genre details by ID.
    [HttpPut("{id}")]
    public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updateGenre)
    {
        // Update genre details using the UpdateGenreCommand.
        var command = new UpdateGenreCommand(_context)
        {
            Model = updateGenre,
            GenreId = id
        };
        var validator = new UpdateGenreCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    // Endpoint: Handles HTTP DELETE request to remove a genre by ID.
    [HttpDelete("{id}")]
    public IActionResult DeleteGenre(int id)
    {
        // Remove a genre by ID using the DeleteGenreCommand.
        var command = new DeleteGenreCommand(_context)
        {
            GenreId = id
        };

        var validator = new DeleteGenreCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }
}

