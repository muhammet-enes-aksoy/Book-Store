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
            // Check if the genre with the same name already exists
            var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);

            if (genre is not null)
            {
                throw new InvalidOperationException("Genre already exists!");
            }

            // Add the new genre to the database
            _context.Genres.Add(new Genre { Name = Model.Name });
            _context.SaveChanges();
        }
}

public class CreateGenreModel
{
    public string Name { get; set; }
}