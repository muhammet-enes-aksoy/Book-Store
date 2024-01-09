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

[ApiController]
[Route("[controller]s")]
public class GenreController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GenreController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult GetGenres()
    {
        var query = new GetGenresQuery(_context, _mapper);
        var obj = query.Handle();
        return Ok(obj);
    }

    [HttpGet("id")]
    public ActionResult GetGenreDetail(int id)
    {
        var query = new GetGenreDetailQuery(_context, _mapper);
        query.GenreId = id;
        var validator = new GetGenreDetailQueryValidator();
        validator.ValidateAndThrow(query);
        var obj = query.Handle();
        return Ok(obj);
    }

    [HttpPost]
    public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
    {
        var command = new CreateGenreCommand(_context, _mapper)
        {
            Model = newGenre
        };

        var validator = new CreateGenreCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updateGenre)
    {
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

    [HttpDelete("id")]
    public IActionResult DeleteGenre(int id)
    {
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