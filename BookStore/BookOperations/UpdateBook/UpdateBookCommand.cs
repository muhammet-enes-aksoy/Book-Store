using BookStore.DbOperations;

namespace BookStore.BookOperations.UpdateBook;
public class UpdateBookCommand
{
    private readonly BookStoreDbContext _dbContext;
    public int BookId { get; set; }
    public UpdateBookViewModel Model { get; set; }

    public UpdateBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
        if (book is null)
        {
            throw new InvalidOperationException("No books found to update!");
        }

        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        //If updatedBook's GenreID has changed, change it. If not changed, use default value
        book.Title = Model.Title != default ? Model.Title : book.Title;
        _dbContext.SaveChanges();
    }

    public class UpdateBookViewModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }

}