using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommand
{
    public CreateGenreModel Model { get; set; }
    public readonly BookStoreDbContext _context;
    public readonly IMapper _mapper;

    public CreateGenreCommand(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);
        if (genre is not null)
        {
            throw new InvalidOperationException("Book type already exist!");
        }

        genre = new Genre();
        genre.Name = Model.Name;
        _context.Genres.Add(genre);
        _context.SaveChanges();
    }
}

public class CreateGenreModel
{
    public string Name { get; set; }
}