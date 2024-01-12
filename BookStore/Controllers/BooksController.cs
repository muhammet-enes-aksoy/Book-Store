using AutoMapper;
using BookStore.Application.BookOperations.Commands;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.Application.BookOperations.Queries;
using BookStore.Application.BookOperations.Queries.GetBookDetail;
using BookStore.Application.BookOperations.Queries.GetBooks;
using BookStore.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers;

// ApiController attribute indicates that this class handles HTTP API requests.
[ApiController]

// Route attribute specifies the base route for the controller.
[Route("[controller]s")]
public class BookController : ControllerBase
{
    // Database context for interacting with the underlying data store.
    private readonly BookStoreDbContext _context;

    // AutoMapper instance for mapping between Entity and ViewModel.
    private readonly IMapper _mapper;

    // Constructor: Initializes the controller with required dependencies.
    public BookController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // Endpoint: Handles HTTP GET request to retrieve a list of books.
    [HttpGet]
    public IActionResult GetBooks()
    {
        // Retrieve and return a list of books using the GetBooksQuery.
        var query = new GetBooksQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    // Endpoint: Handles HTTP GET request to retrieve book details by ID.
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        GetBookDetailQuery.BookDetailViewModel result;
        try
        {
            // Retrieve book details by ID using the GetBookDetailQuery.
            var query = new GetBookDetailQuery(_context, _mapper)
            {
                BookId = id
            };
            var validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
        }
        catch (Exception e)
        {
            // Return a BadRequest response with the error message.
            return BadRequest(e.Message);
        }

        // Return the result of handling the query.
        return Ok(result);
    }

    // Endpoint: Handles HTTP POST request to add a new book.
    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookCommand.CreateBookModel newBook)
    {
        try
        {
            // Add a new book using the CreateBookCommand.
            var command = new CreateBookCommand(_context, _mapper)
            {
                Model = newBook
            };
            var validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception e)
        {
            // Return a BadRequest response with the error message.
            return BadRequest(e.Message);
        }

        // Return a successful response.
        return Ok();
    }

    // Endpoint: Handles HTTP PUT request to update book details by ID.
    [HttpPut("{id:int}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookCommand.UpdateBookViewModel updatedBook)
    {
        try
        {
            // Update book details using the UpdateBookCommand.
            var command = new UpdateBookCommand(_context)
            {
                BookId = id,
                Model = updatedBook
            };
            var validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception e)
        {
            // Return a BadRequest response with the error message.
            return BadRequest(e.Message);
        }

        // Return a successful response.
        return Ok();
    }

    // Endpoint: Handles HTTP DELETE request to remove a book by ID.
    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook(int id)
    {
        try
        {
            // Remove a book by ID using the DeleteBookCommand.
            var command = new DeleteBookCommand(_context)
            {
                BookId = id
            };
            var validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception e)
        {
            // Return a BadRequest response with the error message.
            return BadRequest(e.Message);
        }

        // Return a successful response.
        return Ok();
    }
}

