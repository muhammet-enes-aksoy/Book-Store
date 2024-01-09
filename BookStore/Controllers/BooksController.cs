using BookStore.Api.BookOperations.GetBooks;
using BookStore.Api.DbOperations;
using BookStore.BookOperations.CreateBook;
using FluentValidation;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BookStore.BookOperations.GetBookDetail;
using BookStore.Api.BookOperations.CreateBook;
using BookStore.Api.BookOperations.GetBookDetail;
using BookStore.Api.BookOperations.UpdateBook;
using BookStore.Api.BookOperations.DeleteBook;
namespace BookStore.Api.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public BookController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        var query = new GetBooksQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        GetBookDetailQuery.BookDetailViewModel result;
        try
        {
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
            return BadRequest(e.Message);
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookCommand.CreateBookModel newBook)
    {

        var command = new CreateBookCommand(_context, _mapper)
        {
            Model = newBook
        };
        var validator = new CreateBookCommandValidator(); //Fluent validation
        validator.ValidateAndThrow(command); // Do validate
        command.Handle();

        /*catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        }*/
        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookCommand.UpdateBookViewModel updatedBook)
    {
        try
        {
            var command = new UpdateBookCommand(_context)
            {
                BookId = id,
                Model = updatedBook
            };
            var validator = new UpdateBookCommandValidator(); //Fluent validation
            validator.ValidateAndThrow(command); // Do validate
            command.Handle();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }
    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook(int id)
    {
        try
        {
            var command = new DeleteBookCommand(_context)
            {
                BookId = id
            };
            var validator = new DeleteBookCommandValidator();//Fluent Validation
            validator.ValidateAndThrow(command); // Do validate
            command.Handle();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }
}
