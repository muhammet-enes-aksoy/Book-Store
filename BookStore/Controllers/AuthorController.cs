using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.Application.AuthorOperations.Commands.GetAuthorDetail;
using BookStore.Application.AuthorOperations.Commands.GetAuthors;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

// ApiController attribute indicates that this class handles HTTP API requests.
[ApiController]

// Route attribute specifies the base route for the controller.
[Route("[controller]s")]
public class AuthorController : ControllerBase
{
	// Database context for interacting with the underlying data store.
	private readonly BookStoreDbContext _context;

	// AutoMapper instance for mapping between Entity and ViewModel.
	private readonly IMapper _mapper;

	// Constructor: Initializes the controller with required dependencies.
	public AuthorController(BookStoreDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	// Endpoint: Handles HTTP GET request to retrieve a list of authors.
	[HttpGet]
	public IActionResult GetAuthors()
	{
		// Retrieve and return a list of authors using the GetAuthorsQuery.
		var query = new GetAuthorsQuery(_context, _mapper);
		return Ok(query.Handle());
	}

	// Endpoint: Handles HTTP GET request to retrieve author details by ID.
	[HttpGet("{id:int}")]
	public IActionResult GetAuthorById(int id)
	{
		// Retrieve author details by ID using the GetAuthorDetailQuery.
		var query = new GetAuthorDetailQuery(_context, _mapper)
		{
			AuthorId = id
		};

		// Validate the query parameters using the GetAuthorDetailQueryValidator.
		var validator = new GetAuthorDetailQueryValidator();
		validator.ValidateAndThrow(query);

		// Return the result of handling the query.
		return Ok(query.Handle());
	}

	// Endpoint: Handles HTTP POST request to add a new author.
	[HttpPost]
	public IActionResult AddAuthor([FromBody] CreateAuthorCommand.CreateAuthorViewModel newAuthor)
	{
		// Add a new author using the CreateAuthorCommand.
		var command = new CreateAuthorCommand(_context, _mapper)
		{
			Model = newAuthor
		};

		// Validate the command parameters using the CreateAuthorCommandValidator.
		var validator = new CreateAuthorCommandValidator();
		validator.ValidateAndThrow(command);

		// Handle the command to add the new author.
		command.Handle();

		// Return a successful response.
		return Ok();
	}

	// Endpoint: Handles HTTP PUT request to update author details by ID.
	[HttpPut("{id:int}")]
	public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorCommand.UpdateAuthorViewModel updatedAuthor)
	{
		// Update author details using the UpdateAuthorCommand.
		var command = new UpdateAuthorCommand(_context, _mapper)
		{
			AuthorId = id,
			Model = updatedAuthor
		};

		// Validate the command parameters using the UpdateAuthorCommandValidator.
		var validator = new UpdateAuthorCommandValidator();
		validator.ValidateAndThrow(command);

		// Handle the command to update the author details.
		command.Handle();

		// Return a successful response.
		return Ok();
	}

	// Endpoint: Handles HTTP DELETE request to remove an author by ID.
	[HttpDelete("{id:int}")]
	public IActionResult RemoveAuthor(int id)
	{
		// Remove an author by ID using the DeleteAuthorCommand.
		var command = new DeleteAuthorCommand(_context)
		{
			AuthorId = id
		};

		// Handle the command to remove the author.
		command.Handle();

		// Return a successful response.
		return Ok();
	}
}

