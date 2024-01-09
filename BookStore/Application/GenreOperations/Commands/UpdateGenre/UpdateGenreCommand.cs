using BookStore.DbOperations;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommand
{
    private readonly BookStoreDbContext _dbContext;
    public int GenreId { get; set; }
    public UpdateGenreModel Model { get; set; }

    public UpdateGenreCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
        if (genre is null)
        {
            throw new InvalidOperationException("No book type found to update!");
        }

        if (_dbContext.Genres.Any(x =>
                string.Equals(x.Name.ToLower(), Model.Name.ToLower(), StringComparison.Ordinal) && x.Id != GenreId))
        {
            throw new InvalidOperationException("There is already a book of the same name.");
        }
        genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
        genre.IsActive = Model.IsActive;
        _dbContext.SaveChanges();
    }
}

public class UpdateGenreModel
{
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
}