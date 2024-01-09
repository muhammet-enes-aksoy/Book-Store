using AutoMapper;
using BookStore.Api.BookOperations.CreateBook;
using BookStore.Api.BookOperations.DeleteBook;
using BookStore.Api.BookOperations.GetBookDetail;
using BookStore.Api.BookOperations.GetBooks;
using BookStore.Api.BookOperations.UpdateBook;
using BookStore.Api.DbOperations;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.GetBookDetail;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;
[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
    private readonly BookStoreDbContext _context;

    public BookController(BookStoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(_context);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public Book GetById(int id)
    {
        var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
        return book;
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookCommand.CreateBookModel newBook)
    {
        try
        {
            CreateBookCommand command = new CreateBookCommand(_context)
            {
                Model = newBook
            };
            command.Handle();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = _context.Books.SingleOrDefault(x => x.Id == id);
        if (book is null)
        {
            return BadRequest();
        }

        book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        //If updatedBook's GenreID has changed, change it. If not changed, use default value
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = _context.Books.SingleOrDefault(x => x.Id == id);
        if (book is null)
        {
            return BadRequest();
        }
        _context.Books.Remove(book);
        _context.SaveChanges();
        return Ok();
    }
}